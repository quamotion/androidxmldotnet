namespace AndroidXmlDemo.Models
{
    public class StringPoolStyleItem : ObservableObject<StringPoolStyleItem>
    {
        #region FirstChar property

        private int _firstChar;

        public int FirstChar
        {
            get { return _firstChar; }
            set
            {
                if (_firstChar == value) return;
                _firstChar = value;
                RaisePropertyChanged(o => o.FirstChar);
            }
        }

        #endregion // FirstChar property

        #region LastChar property

        private int _lastChar;

        public int LastChar
        {
            get { return _lastChar; }
            set
            {
                if (_lastChar == value) return;
                _lastChar = value;
                RaisePropertyChanged(o => o.LastChar);
            }
        }

        #endregion // LastChar property

        #region Name property

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                RaisePropertyChanged(o => o.Name);
            }
        }

        #endregion // Name property
    }
}
