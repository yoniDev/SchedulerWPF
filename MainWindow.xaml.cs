using Scheduler.View;
using Scheduler.ViewModel;
using System.Windows;

namespace Scheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainViewModel = new MainWindowViewModel();
            DataContext = mainViewModel;
        }

    }
}

