
using Scheduler.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduler.Model
{
    public class Schedule : ObservableObject
    {
        #region Fields

        private int _id = 0;
        private string _title = string.Empty;
        private DateTime _startDate = DateTime.Now;
        private DateTime? _endDate = DateTime.Now;
        private string _discription = string.Empty;
        private Catagory _catagory;
        private string _catagoryName = string.Empty;
        private string _isFinished = string.Empty;




        #endregion

        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                    _id = value;
                OnPropertyChanged("Id");
            }

        }
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                    _title = value;
                OnPropertyChanged("Title");
            }
        }
        public DateTime StartDate
        {
            get { return _startDate.Date; }
            set
            {
                if (value != _startDate)
                    _startDate = value.Date;
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime? EndDate
        {
            get { return _endDate.Value.Date; }
            set
            {
                if (value != _endDate)
                    _endDate = value.Value.Date;
                OnPropertyChanged("EndDate");
            }
        }

        public string Discription
        {
            get { return _discription; }
            set
            {
                if (value != _discription)
                    _discription = value;
                OnPropertyChanged("Discription");
            }
        }
        [ForeignKey("CatagoryName")]
        public virtual Catagory Catagory
        {
            get { return _catagory; }
            set
            {
                if (value != _catagory)
                    _catagory = value;
                OnPropertyChanged("Catagory");
            }
        }
        public string CatagoryName
        {
            get { return _catagoryName; }
            set
            {
                if (value != _catagoryName)
                    _catagoryName = value;
                OnPropertyChanged("CatagoryName");
            }

        }

        public string IsFinished
        {
            get { return _isFinished; }
            set
            {
                if (value != _isFinished)
                    _isFinished = value;
                OnPropertyChanged("IsFinished");
            }
        }

        #endregion

    }
}
