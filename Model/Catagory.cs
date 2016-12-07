using Scheduler.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Model
{
    public class Catagory : ObservableObject
    {

        private string _catagoryName;

        [Key]
        public string CatagoryName
        {
            get { return _catagoryName; }
            set
            {
                if (_catagoryName != value)
                {
                    _catagoryName = value;
                    OnPropertyChanged("CatagoryName");
                }
            }
        }

        public override string ToString()
        {
            return CatagoryName;
        }
    }
}
