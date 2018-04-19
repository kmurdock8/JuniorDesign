using Xamarin.Forms;
using System;


namespace ProctorCreekGreenwayApp
{
    public partial class App : Application
    {
        public static RestService DBManager {get; private set;} 

        static PCGLocalDatabase database;
        
        public App()
        {
            InitializeComponent();

            // Initialize database manager
            DBManager = new RestService();

            MainPage = new MainPage();

        }

        public static PCGLocalDatabase Database 
        {
            get 
            {
                if (database == null) 
                {
                    // Database not instantiated yet
                    database = new PCGLocalDatabase(DependencyService.Get<IDBFileHelper>().GetLocalFilePath("PCGAppSQLite.db3"));
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
