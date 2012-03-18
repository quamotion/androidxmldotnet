using System.Collections.ObjectModel;
using AndroidXml.Res;
using AndroidXmlDemo.Models;

namespace AndroidXmlDemo.ViewModels
{
    public class StringPoolViewModel : ObservableObject<StringPoolViewModel>
    {
        #region Construction

        public StringPoolViewModel(ResStringPool stringPool)
        {
            InitializeItems(stringPool);
        }

        private void InitializeItems(ResStringPool stringPool)
        {
            for (int i = 0; i < stringPool.StringData.Count; i++)
            {
                var item = new StringPoolItem
                {
                    Index = i,
                    Text = stringPool.StringData[i],
                };
                foreach (ResStringPool_span style in stringPool.GetStyles((uint) i))
                {
                    item.Styles.Add(new StringPoolStyleItem
                    {
                        FirstChar = (int) style.FirstChar,
                        LastChar = (int) style.LastChar,
                        Name = stringPool.GetString(style.Name)
                    });
                }
                _items.Add(item);
            }
        }

        #endregion // Constrution

        #region Properties

        #region Items

        private ObservableCollection<StringPoolItem> _items = new ObservableCollection<StringPoolItem>();

        public ObservableCollection<StringPoolItem> Items
        {
            get { return _items; }
            set
            {
                if (_items == value) return;
                _items = value;
                RaisePropertyChanged(o => o.Items);
            }
        }

        #endregion // Items

        #region SelectedItem

        private StringPoolItem _selectedItem;

        public StringPoolItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value) return;
                _selectedItem = value;
                RaisePropertyChanged(o => o.SelectedItem);
            }
        }

        #endregion // SelectedItem

        #endregion // Properties
    }
}