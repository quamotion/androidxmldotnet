using System.Windows;
using AndroidXmlDemo.ViewModels;

namespace AndroidXmlDemo.Views
{
    /// <summary>
    /// Lets the user browse the string pool.
    /// </summary>
    public partial class StringPoolView : Window
    {
        private readonly StringPoolViewModel _viewModel;

        public StringPoolView(StringPoolViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}