namespace Scheduler.BusinessLogic
{
    using Model;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class ScheduleDbContext : DbContext
    {
        // Your context has been configured to use a 'DbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Scheduler.BusinessLogic.DbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DbContext' 
        // connection string in the application configuration file.

        public ScheduleDbContext()
            : base("name = ScheduleDbContext")
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<ScheduleDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ScheduleDbContext>());
        }

        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Catagory> Catagories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

}