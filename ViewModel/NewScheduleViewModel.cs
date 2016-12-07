using Scheduler.BusinessLogic;
using Scheduler.Helpers;
using Scheduler.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Scheduler.ViewModel
{
    public class NewScheduleViewModel : ViewModelBase, IPageViewModel
    {
        #region Private Fields
        private string _title;
        private DateTime _startDate = DateTime.Now;
        private DateTime? _endDate = DateTime.Now;
        private string _discription;
        private Catagory _selectedCatagory = new Catagory();
        private ObservableCollection<Catagory> _catagories = new ObservableCollection<Catagory>();
        private ObservableCollection<Schedule> _selectedDateSchedule;
        private DateTime _selectedDate = DateTime.Now.Date;
        private string _isFinished = string.Empty;
       

        private ICommand _scheduleByDateCommand;
        private ICommand _saveScheduleCommand;

        private ScheduleBusinessLogic businessLogic = new ScheduleBusinessLogic();
        #endregion
       
        #region NewScheduleViewModel Constractor
        public NewScheduleViewModel()
        {
            _catagories = businessLogic.GetAllCatagories();
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return "Compose"; }
        }

        public string ImagePath
        {
            get { return "Resources/AddNew.png"; }
        }

        public int GridRow
        {
            get { return 1; }
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
            get { return _startDate; }
            set
            {
                if (value != _startDate)
                    _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (value != _endDate)
                    _endDate = value;
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

        public ObservableCollection<Catagory> Catagories
        {
            get { return _catagories; }
            set
            {
                _catagories = value;
                OnPropertyChanged("Catagories");
            }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (value != _selectedDate)
                {
                    _selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }

        public Catagory SelectedCatagory
        {
            get { return _selectedCatagory; }
            set
            {
                _selectedCatagory = value;
                OnPropertyChanged("SelectedCatagory");
            }
        }

        public ObservableCollection<Schedule> SelectedDateSchedule
        {
            get { return _selectedDateSchedule; }
            set
            {
                if (value != _selectedDateSchedule)
                {
                    _selectedDateSchedule = value;
                    OnPropertyChanged("SelectedDateSchedule");
                }
            }
        }


        #endregion

        #region Commands to save schedule and get Schedule by date
        public ICommand ScheduleByDateCommand
        {
            get
            {
                if (_scheduleByDateCommand == null)
                {
                    _scheduleByDateCommand = new RelayCommand
                        (
                        s => GetSchedulesByDate()
                        );
                }

                return _scheduleByDateCommand;
            }
        }

        public ICommand SaveScheduleCommand
        {
            get
            {
                if (_saveScheduleCommand == null)
                {
                    _saveScheduleCommand = new RelayCommand
                    (
                     param => SaveSchedule()
                    );
                }
                return _saveScheduleCommand;
            }
        }

        #endregion

        #region private methods and Validation rules  
        /// <summary>
        /// If validation succeeded, saves the schedule and notifies the user  
        /// </summary>
        private void SaveSchedule()
        {
            if (Validate())
            {
                Schedule schedule = new Schedule();
                schedule.Title = Title;
                schedule.StartDate = StartDate;
                schedule.EndDate = EndDate;
                schedule.CatagoryName = SelectedCatagory.CatagoryName;
                schedule.Discription = Discription;
                schedule.IsFinished = IsFinished;

                businessLogic.Save(schedule);
                MessageToDisplay = schedule.Title + " has been saved!";
                IsMessageToDisplayVisible = true;
            }

        }
        /// <summary>
        /// Gets schedules by the selected date and sets them to SelectedDateSchedule Property
        /// </summary>
        private void GetSchedulesByDate()
        {
            SelectedDateSchedule = new ObservableCollection<Schedule>(businessLogic.ScheduleByDate(SelectedDate));
        }
        /// <summary>
        /// Validation rules for user input
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            IsMessageToDisplayVisible = false;
            MessageToDisplay = string.Empty;

            if (StartDate > EndDate)
            {
                MessageToDisplay = "Start Date must be less than end date!";
                IsMessageToDisplayVisible = true;
            }

            if (StartDate == null || string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Discription) || SelectedCatagory.CatagoryName == null)
            {
                MessageToDisplay = "Please fill in the missing fields!";
                IsMessageToDisplayVisible = true;
            }

            return !IsMessageToDisplayVisible;
        }
        #endregion

    }
}
