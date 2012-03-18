using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using AndroidXml;
using AndroidXml.Res;
using AndroidXmlDemo.Commands;
using AndroidXmlDemo.Models;

namespace AndroidXmlDemo.ViewModels
{
    public class MainViewModel : ObservableObject<MainViewModel>
    {
        public MainViewModel()
        {
            _loadCommand = new DelegateCommand(LoadCommand_Execute, () => !string.IsNullOrEmpty(_filename));

            ShowStringPoolCommand = new EventCommand<ResStringPool>(o => _reader is AndroidXmlReader);
            BrowseCommand = new EventCommand<string>();
        }

        #region Error event

        public event EventHandler<ArgumentEventArgs<string>> Error;

        public void OnError(string message)
        {
            EventHandler<ArgumentEventArgs<string>> handler = Error;
            if (handler != null)
            {
                handler(this, new ArgumentEventArgs<string> {Argument = message});
            }
        }

        #endregion

        #region Properties

        #region Filename

        private string _filename;

        public string Filename
        {
            get { return _filename; }
            set
            {
                if (value == _filename) return;
                _filename = value;
                RaisePropertyChanged(o => o.Filename);
                _loadCommand.RaiseCanExecuteChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        #endregion // Filename

        #region FormattedDocument

        private FlowDocument _formattedDocument;

        public FlowDocument FormattedDocument
        {
            get { return _formattedDocument; }
            set
            {
                if (value == _formattedDocument) return;
                _formattedDocument = value;
                RaisePropertyChanged(o => o.FormattedDocument);
            }
        }

        #endregion // FormattedDocument

        #region XmlReader

        private XmlReader _reader;

        public XmlReader Reader
        {
            get { return _reader; }
            set
            {
                if (_reader == value) return;
                _reader = value;
                RaisePropertyChanged(o => o.Reader);
            }
        }

        #endregion // XmlReader

        #endregion // Properties

        #region Commands

        #region BrowseCommand

        public EventCommand<string> BrowseCommand { get; private set; }

        public void BrowseCommandCompleted(string filename)
        {
            Filename = filename;
        }

        #endregion // BRowseCommand

        #region LoadCommand

        private readonly Brush _attrNameColor = Brushes.Red;
        private readonly Brush _attrQuotesColor = Brushes.Black;
        private readonly Brush _attrValueColor = Brushes.Blue;
        private readonly Brush _cdataSectionColor = Brushes.Gray;
        private readonly Brush _commentColor = Brushes.Green;
        private readonly Brush _delimiterColor = Brushes.Blue;
        private readonly Brush _nameColor = new SolidColorBrush(Color.FromRgb(0xA3, 0x15, 0x15));
        private readonly Brush _textColor = Brushes.Black;
        private DelegateCommand _loadCommand;

        public DelegateCommand LoadCommand
        {
            get { return _loadCommand; }
            set
            {
                if (value == _loadCommand) return;
                _loadCommand = value;
                RaisePropertyChanged(o => o.LoadCommand);
            }
        }

        private void LoadCommand_Execute()
        {
            try
            {
                byte[] data = File.ReadAllBytes(Filename);
                if (data.Length == 0)
                {
                    throw new IOException("Empty file");
                }
                var stream = new MemoryStream(data);
                if (data[0] == '<' || char.IsWhiteSpace((char) data[0]))
                {
                    // Normal XML file
                    _reader = new XmlTextReader(stream)
                    {
                        WhitespaceHandling = WhitespaceHandling.None
                    };
                }
                else
                {
                    // Android binary XML
                    _reader = new AndroidXmlReader(stream);
                }
                ShowResult(_reader);
            }
            catch (Exception ex)
            {
                OnError(string.Format("{0} ({1})", ex.Message, ex.GetType().Name));
                Console.WriteLine(ex);
            }
        }

        private void ShowResult(XmlReader reader)
        {
            var paragraph = new Paragraph
            {
                TextAlignment = TextAlignment.Left,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 14.0,
                Foreground = _textColor
            };
            var section = new Section(paragraph);
            var doc = new FlowDocument(section);

            int indent = 0;

            var namespaceStack = new Stack<List<NamespaceInfo>>();
            var unknownNamespaces = new List<NamespaceInfo>();
            var knownNamespaces = new List<NamespaceInfo>();

            knownNamespaces.Add(new NamespaceInfo {Prefix = "", Uri = ""});

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                    {
                        var line = new Span();
                        line.Inlines.Add(new string(' ', indent*2));
                        line.Inlines.Add(new Run("<") {Foreground = _delimiterColor});
                        if (!string.IsNullOrEmpty(reader.Prefix))
                        {
                            line.Inlines.Add(new Run(reader.Prefix) {Foreground = _nameColor});
                            line.Inlines.Add(new Run(":") {Foreground = _delimiterColor});
                        }
                        line.Inlines.Add(new Run(reader.LocalName) {Foreground = _nameColor});

                        namespaceStack.Push(knownNamespaces);
                        knownNamespaces = knownNamespaces.ToList();
                        if (!knownNamespaces.Any(ni => ni.Prefix == reader.Prefix && ni.Uri == reader.NamespaceURI))
                        {
                            var info = new NamespaceInfo {Prefix = reader.Prefix, Uri = reader.NamespaceURI};
                            knownNamespaces.RemoveAll(ni => ni.Prefix == info.Prefix);
                            knownNamespaces.Add(info);
                            unknownNamespaces.Add(info);
                        }

                        bool first = true;
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            if (!knownNamespaces.Any(ni => ni.Prefix == reader.Prefix && ni.Uri == reader.NamespaceURI))
                            {
                                var info = new NamespaceInfo {Prefix = reader.Prefix, Uri = reader.NamespaceURI};
                                knownNamespaces.RemoveAll(ni => ni.Prefix == info.Prefix);
                                knownNamespaces.Add(info);
                                unknownNamespaces.Add(info);
                            }

                            if (first)
                            {
                                line.Inlines.Add(" ");
                            }
                            else
                            {
                                paragraph.Inlines.Add(line);
                                paragraph.Inlines.Add(new LineBreak());
                                line = new Span();
                                line.Inlines.Add(new string(' ', indent*2 + 4));
                            }
                            first = false;

                            reader.MoveToAttribute(i);

                            if (!string.IsNullOrEmpty(reader.Prefix))
                            {
                                line.Inlines.Add(new Run(reader.Prefix) {Foreground = _attrNameColor});
                                line.Inlines.Add(new Run(":") {Foreground = _delimiterColor});
                            }
                            line.Inlines.Add(new Run(reader.LocalName) {Foreground = _attrNameColor});
                            line.Inlines.Add(new Run("=") {Foreground = _delimiterColor});
                            line.Inlines.Add(new Run("\"") {Foreground = _attrQuotesColor});

                            FormatEntities(line.Inlines, reader.Value, _attrValueColor);

                            line.Inlines.Add(new Run("\"") {Foreground = _attrQuotesColor});
                        }

                        reader.MoveToElement();

                        foreach (NamespaceInfo info in unknownNamespaces)
                        {
                            if (first)
                            {
                                line.Inlines.Add(" ");
                            }
                            else
                            {
                                paragraph.Inlines.Add(line);
                                paragraph.Inlines.Add(new LineBreak());
                                line = new Span();
                                line.Inlines.Add(new string(' ', indent*2 + 4));
                            }
                            first = false;

                            line.Inlines.Add(new Run("xmlns") {Foreground = _attrNameColor});
                            if (!string.IsNullOrEmpty(info.Prefix))
                            {
                                line.Inlines.Add(new Run(":") {Foreground = _delimiterColor});
                                line.Inlines.Add(new Run(info.Prefix) {Foreground = _attrNameColor});
                            }
                            line.Inlines.Add(new Run("=") {Foreground = _delimiterColor});
                            line.Inlines.Add(new Run("\"") {Foreground = _attrQuotesColor});

                            FormatEntities(line.Inlines, info.Uri, _attrValueColor);

                            line.Inlines.Add(new Run("\"") {Foreground = _attrQuotesColor});
                        }
                        unknownNamespaces.Clear();

                        if (reader.IsEmptyElement)
                        {
                            line.Inlines.Add(new Run("/>") {Foreground = _delimiterColor});
                            knownNamespaces = namespaceStack.Pop();
                        }
                        else
                        {
                            line.Inlines.Add(new Run(">") {Foreground = _delimiterColor});
                            indent++;
                        }

                        paragraph.Inlines.Add(line);

                        break;
                    }
                    case XmlNodeType.EndElement:
                    {
                        indent--;

                        var line = new Span();
                        line.Inlines.Add(new string(' ', indent*2));

                        line.Inlines.Add(new Run("</") {Foreground = _delimiterColor});
                        if (!string.IsNullOrEmpty(reader.Prefix))
                        {
                            line.Inlines.Add(new Run(reader.Prefix) {Foreground = _nameColor});
                            line.Inlines.Add(new Run(":") {Foreground = _delimiterColor});
                        }
                        line.Inlines.Add(new Run(reader.LocalName) {Foreground = _nameColor});
                        line.Inlines.Add(new Run(">") {Foreground = _delimiterColor});

                        paragraph.Inlines.Add(line);

                        knownNamespaces = namespaceStack.Pop();

                        break;
                    }
                    case XmlNodeType.CDATA:
                    {
                        var line = new Span();
                        line.Inlines.Add(new string(' ', indent*2));

                        line.Inlines.Add(new Run("<![CDATA[") {Foreground = _cdataSectionColor});

                        string value = reader.Value;
                        bool first = true;
                        foreach (string piece in value.Split(new[] {"]]>"}, StringSplitOptions.None))
                        {
                            if (!first)
                            {
                                line.Inlines.Add("]]");
                                line.Inlines.Add(new Run("]]><![CDATA[") {Foreground = _cdataSectionColor});
                                line.Inlines.Add(">");
                            }
                            first = false;
                            line.Inlines.Add(piece);
                        }
                        line.Inlines.Add(new Run("]]>") {Foreground = _cdataSectionColor});

                        paragraph.Inlines.Add(line);

                        break;
                    }
                    case XmlNodeType.Text:
                    {
                        var line = new Span();
                        line.Inlines.Add(new string(' ', indent*2));

                        FormatEntities(line.Inlines, reader.Value, _textColor);

                        paragraph.Inlines.Add(line);

                        break;
                    }
                    case XmlNodeType.Comment:
                    {
                        var line = new Span();
                        line.Inlines.Add(new string(' ', indent*2));

                        string value = reader.Value;
                        value = value.Replace("--", "- -");
                        value = value.Replace("--", "- -"); // twice to get all
                        line.Inlines.Add(new Run("<!--" + value + "-->") {Foreground = _commentColor});

                        paragraph.Inlines.Add(line);

                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Warning: Unexpected node type: {0}", reader.NodeType);
                        continue;
                    }
                }

                paragraph.Inlines.Add(new LineBreak());
            }

            FormattedDocument = doc;
        }

        private void FormatEntities(InlineCollection inlines, string value, Brush textColor)
        {
            int lastIndex = 0;
            int index = value.IndexOfAny(new[] {'&', '<', '>', '"'});
            while (index >= 0)
            {
                inlines.Add(new Run(value.Substring(lastIndex, index - lastIndex)) {Foreground = textColor});
                switch (value[index])
                {
                    case '&':
                        inlines.Add(new Run("&amp;") {Foreground = _nameColor});
                        break;
                    case '<':
                        inlines.Add(new Run("&lt;") {Foreground = _nameColor});
                        break;
                    case '>':
                        inlines.Add(new Run("&gt;") {Foreground = _nameColor});
                        break;
                    case '"':
                        inlines.Add(new Run("&quot;") {Foreground = _nameColor});
                        break;
                }
                lastIndex = index + 1;
                index = value.IndexOfAny(new[] {'&', '<', '>', '"'}, lastIndex);
            }
            inlines.Add(new Run(value.Substring(lastIndex)) {Foreground = textColor});
        }

        private class NamespaceInfo
        {
            public string Prefix { get; set; }
            public string Uri { get; set; }
        }

        #endregion // LoadCommand

        #region ShowStringPoolCommand

        public EventCommand<ResStringPool> ShowStringPoolCommand { get; private set; }

        #endregion // ShowStringPoolCommand

        #endregion // Commands
    }
}