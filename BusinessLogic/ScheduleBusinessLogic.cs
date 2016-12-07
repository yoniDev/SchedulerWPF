using Scheduler.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Scheduler.BusinessLogic
{
    /// <summary>
    /// ScheduleBusinessLogic class consumes the Repository class to handel and manipulate Datas 
    /// throught this App, Summary about each method can be found from Repository Class
    /// </summary>
    public class ScheduleBusinessLogic
    {
        #region Private Fields
        /// <summary>
        /// Instances of Repositories with respective Models filled in with an instance of DbContext
        /// </summary>
        private Repository<Schedule> dbSchedule = new Repository<Schedule>(new ScheduleDbContext());
        private Repository<Catagory> dbCatagory = new Repository<Catagory>(new ScheduleDbContext());
        #endregion

        #region Business Logics for the Application

        public Schedule GetById(int? id)
        {
            Schedule schedule = dbSchedule.GetById(id);
            if (schedule == null)
            {
                //Error 
            }
            return schedule;
        }

        public void Edit(Schedule schedule)
        {
            dbSchedule.Edit(schedule);
            GetAllSchedules();
        }

        public void Delete(Schedule schedule)
        {
            dbSchedule.Delete(schedule);
            GetAllSchedules();
        }

        public ObservableCollection<Schedule> GetAllSchedules()
        {
            var schedules = new ObservableCollection<Schedule>(dbSchedule.GetAll());
            return schedules;
        }

        public ObservableCollection<Catagory> GetAllCatagories()
        {
            var catagories = new ObservableCollection<Catagory>(dbCatagory.GetAll());
            return catagories;
        }

        public void Save(Schedule schedule)
        {
            dbSchedule.Save(schedule);
            dbSchedule.GetAll();
        }

        public IQueryable<Schedule> ScheduleByDate(DateTime startDate)
        {
            return dbSchedule.SearchFor(s => DbFunctions.TruncateTime(s.StartDate) == DbFunctions.TruncateTime(startDate));
        }

        #endregion
    }
}
