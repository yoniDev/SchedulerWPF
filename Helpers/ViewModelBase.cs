
namespace Scheduler.Helpers
{
    public class ViewModelBase : ObservableObject
    {
        private string _messageToDisplay = string.Empty;
        private bool _isMessageToDisplayVisible = false;


        public string MessageToDisplay
        {
            get { return _messageToDisplay; }
            set
            {
                if (value != _messageToDisplay)
                {
                    _messageToDisplay = value;
                    OnPropertyChanged("MessageToDisplay");
                }
            }
        }

        public bool IsMessageToDisplayVisible
        {
            get { return _isMessageToDisplayVisible; }
            set
            {
                if (value != _isMessageToDisplayVisible)
                {
                    _isMessageToDisplayVisible = value;
                    OnPropertyChanged("IsMessageToDisplayVisible");
                }
            }
        }
    }
}
