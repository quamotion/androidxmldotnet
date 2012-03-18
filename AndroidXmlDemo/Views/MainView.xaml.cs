using System.Windows;
using AndroidXml;
using AndroidXml.Res;
using AndroidXmlDemo.Commands;
using AndroidXmlDemo.ViewModels;
using Microsoft.Win32;

namespace AndroidXmlDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly OpenFileDialog _openFileDialog = new OpenFileDialog
        {
            CheckFileExists = true,
            Filter = "Android Binary XML (*.xml)|*.xml|All Files (*.*)|*.*",
        };

        private readonly MainViewModel _viewModel;

        public MainView(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();

            viewModel.BrowseCommand.Executed += BrowseCommand_Executed;
            viewModel.ShowStringPoolCommand.Executed += ShowStringPool_Executed;
            viewModel.Error += ViewModel_Error;
        }

        private void ViewModel_Error(object sender, ArgumentEventArgs<string> e)
        {
            MessageBox.Show(this, e.Argument, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowStringPool_Executed(object sender, ArgumentEventArgs<ResStringPool> e)
        {
            var androidXmlReader = _viewModel.Reader as AndroidXmlReader;
            var viewModel = new StringPoolViewModel(androidXmlReader == null ? null : androidXmlReader.StringPool);
            var window = new StringPoolView(viewModel)
            {
                Owner = this
            };
            window.Show();
        }

        private void BrowseCommand_Executed(object sender, ArgumentEventArgs<string> e)
        {
            _openFileDialog.FileName = e.Argument;
            if (_openFileDialog.ShowDialog() != true) return;
            _viewModel.BrowseCommandCompleted(_openFileDialog.FileName);
        }
    }
}