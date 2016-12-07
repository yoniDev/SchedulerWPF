using Scheduler.Model;
using Scheduler.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scheduler.View
{
    /// <summary>
    /// Interaction logic for NewScheduleView.xaml
    /// </summary>
    public partial class NewScheduleView : UserControl
    {
        public NewScheduleView()
        {
            InitializeComponent();
            this.DataContext = new NewScheduleViewModel();
            Dpicker.Loaded += delegate
            {
                var textBox1 = (TextBox)Dpicker.Template.FindName("PART_TextBox", Dpicker);
                textBox1.Background = Dpicker.Background;
                textBox1.Foreground = Dpicker.Foreground;

                var textBox2 = (TextBox)Dpicker2.Template.FindName("PART_TextBox", Dpicker2);
                textBox2.Background = Dpicker2.Background;
                textBox2.Foreground = Dpicker2.Foreground;
            };

           
        
        }
    }
}
