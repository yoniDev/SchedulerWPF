using Scheduler.BusinessLogic;
using Scheduler.Helpers;
using Scheduler.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Scheduler.ViewModel
{
    public class ScheduleViewModel : ViewModelBase, IPageViewModel
    {

        #region Private Fields
        private int _id = 0;
        private string _title = string.Empty;
        private DateTime _startDate = DateTime.Now;
        private DateTime? _endDate = DateTime.Now;
        private string _discription = string.Empty;
        private Catagory _catagory;
        private string _catagoryName = string.Empty;
        public string _isFinished = string.Empty;
        private Schedule _currentSchedule;
        private bool _isSelected = false;

        private ICommand _editScheduleCommand;
        private ICommand _deleteScheduleCommand;
        private ObservableCollection<Schedule> _schedules;
        private ScheduleBusinessLogic businessLogic = new ScheduleBusinessLogic();
        #endregion

        #region ScheduleViewModel Constractor
        public ScheduleViewModel()
        {
            try
            {
                Schedules = new ObservableCollection<Schedule>(businessLogic.GetAllSchedules().OrderByDescending(s => s.StartDate));
               // Catagories = new ObservableCollection<Catagory>(businessLogic.GetAllCatagories());
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBoxResult result = MessageBox.Show("Please Check your Server Connection and Try again!", "Connection Error", MessageBoxButton.OK);
                Application.Current.Shutdown();

            }

        }

        #endregion

        #region Properties
        
        public ObservableCollection<Schedule> Schedules
        {
            get { return _schedules; }
            set
            {
                if (value != _schedules)
                    _schedules = value;
                OnPropertyChanged("Schedules");
            }
        }
       
        public string Name
        {
            get { return "My Schedules"; }
        }

        public string ImagePath
        {
            get { return "Resources/Schedules.png"; }
        }

        public int GridRow
        {
            get { return 2; }
        }

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

        public Schedule CurrentSchedule
        {
            get { return _currentSchedule; }
            set
            {
                if (value != _currentSchedule)
                {
                    _currentSchedule = value;
                    OnPropertyChanged("CurrentSchedule");
                    if (CurrentSchedule != null)
                    {
                        if (CurrentSchedule.IsFinished.Equals("Completed"))
                            IsSelected = true;
                        else
                            IsSelected = false;
                    }
                   
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                    _isSelected = value;
                OnPropertyChanged("IsSelected");
                IsTaskDone();

            }
        }

        #endregion

        #region Commands
        public ICommand EditScheduleCommand
        {
            get
            {
                if (_editScheduleCommand == null)
                {
                    _editScheduleCommand = new RelayCommand
                                    (s => EditSelectedSchedule());
                }
                return _editScheduleCommand;
            }
        }

        public ICommand DeleteScheduleCommand
        {
            get
            {
                if (_deleteScheduleCommand == null)
                {
                    _deleteScheduleCommand = new RelayCommand
                                        (s => DeleteSelectedSchedule());
                }

                return _deleteScheduleCommand;
            }
        }

        #endregion

        #region Preivate methods for editing, deleting and validating Schedules
        /// <summary>
        /// Checks if Validation succeeded, applies the update and notifies the user about the changes made
        /// </summary>
        private void EditSelectedSchedule()
        {
            if (Validate())
            {
                businessLogic.Edit(CurrentSchedule);
                MessageToDisplay = CurrentSchedule.Title + " has been updated!";
                IsMessageToDisplayVisible = true;
            }

        }

        /// <summary>
        /// Checks if a schedule has been selected, 
        /// asks the user for confirmation, 
        /// if yes deletes the selected Schedule
        /// </summary>
        private void DeleteSelectedSchedule()
        {
            if (CurrentSchedule == null)
            {
                MessageToDisplay = "Please select a Schedule from the list!";
                IsMessageToDisplayVisible = true;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Do you want to permanently delete this schedule?!", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    businessLogic.Delete(CurrentSchedule);
                    MessageToDisplay = CurrentSchedule.Title + " has been deleted!";
                    IsMessageToDisplayVisible = true;
                    Schedules = new ObservableCollection<Schedule>(businessLogic.GetAllSchedules().OrderByDescending(s => s.StartDate));
                   
                }
            }
        }

        /// <summary>
        /// Validation rules and error messages accordingly
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            IsMessageToDisplayVisible = false;
            MessageToDisplay = string.Empty;

            if (CurrentSchedule == null)
            {
                MessageToDisplay = "Please select a Schedule from the list!";
                IsMessageToDisplayVisible = true;
            }
            else
            {
                if (CurrentSchedule.StartDate == null ||
                string.IsNullOrEmpty(CurrentSchedule.Title) ||
                string.IsNullOrEmpty(CurrentSchedule.Discription) ||
                string.IsNullOrEmpty(CurrentSchedule.CatagoryName))
                {
                    MessageToDisplay = "Please fill in the missing fields!";
                    IsMessageToDisplayVisible = true;
                }

                if (CurrentSchedule.StartDate > CurrentSchedule.EndDate)
                {
                    MessageToDisplay = "Start Date must be less than end date!";
                    IsMessageToDisplayVisible = true;
                }
            }
            

            return !IsMessageToDisplayVisible;
        }

        private void IsTaskDone()
        {
            if (CurrentSchedule != null)
            {
                if (IsSelected == true)
                {
                    CurrentSchedule.IsFinished = "Completed";
                    businessLogic.Edit(CurrentSchedule);
                }
                else if (IsSelected == false)
                {
                    CurrentSchedule.IsFinished = string.Empty;
                    businessLogic.Edit(CurrentSchedule);
                }
            }
        }
        #endregion

    }
}

