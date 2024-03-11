using System.Windows;
using Pass_generator_wpf.ViewModels;
namespace Pass_generator_wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

    }
}
