using Scheduler.Helpers;

namespace Scheduler.ViewModel
{
    public class HomePageViewModel : ViewModelBase, IPageViewModel
    {
        #region public properties
        public string Name
        {
            get { return "Scheduler"; }
        }

        public string ImagePath
        {
            get { return "Resources/Status.png"; }
        }

        public int GridRow
        {
            get { return 0; }
        }
        #endregion

    }
}
