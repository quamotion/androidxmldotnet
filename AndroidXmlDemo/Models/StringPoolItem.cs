using System.Collections.ObjectModel;

namespace AndroidXmlDemo.Models
{
    public class StringPoolItem : ObservableObject<StringPoolItem>
    {
        #region Index property

        private int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                if (_index == value) return;
                _index = value;
                RaisePropertyChanged(o => o.Index);
            }
        }

        #endregion // Index property

        #region Text property

        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value) return;
                _text = value;
                RaisePropertyChanged(o => o.Text);
            }
        }

        #endregion // Text property

        #region Styles property

        private ObservableCollection<StringPoolStyleItem> _styles = new ObservableCollection<StringPoolStyleItem>();

        public ObservableCollection<StringPoolStyleItem> Styles
        {
            get { return _styles; }
            set
            {
                if (_styles == value) return;
                _styles = value;
                RaisePropertyChanged(o => o.Styles);
            }
        }

        #endregion // Styles property
    }
}