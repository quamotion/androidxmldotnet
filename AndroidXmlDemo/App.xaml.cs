using System.Windows;
using AndroidXmlDemo.ViewModels;
using AndroidXmlDemo.Views;

namespace AndroidXmlDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var viewModel = new MainViewModel();
            MainWindow = new MainView(viewModel);
            MainWindow.Show();
        }
    }
}