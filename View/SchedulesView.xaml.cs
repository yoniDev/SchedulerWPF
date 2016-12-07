using Scheduler.Model;
using Scheduler.ViewModel;
using System.Windows.Controls;

namespace Scheduler.View
{
    /// <summary>
    /// Interaction logic for SchedulesView.xaml
    /// </summary>
    public partial class SchedulesView : UserControl
    {
        
        ScheduleViewModel scheduleViewModel = new ScheduleViewModel();
        public SchedulesView()
        {
           InitializeComponent();

           this.DataContext = scheduleViewModel;
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
