using System;
using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;

namespace ProctorCreekGreenwayApp
{
    public class PCGLocalDatabase
    {
        readonly SQLiteAsyncConnection database; /* The SQLite database connection */

        /**
         * Constructor creates a database connection that is kept open while app runs
         */
        public PCGLocalDatabase(string dbPath) 
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
        }

        /**
         * Methods below are SQLite database queries.
         * Methods called in SettingsView
         */

        /* Gets user info */
        public Task<User> GetUser()
        {
            return database.Table<User>().FirstOrDefaultAsync();
        }

        /* Adds user to database */
        public Task<int> AddUser()
        {
            // Add user to db and set everything to true
            User newUser = new User
            {
                Notifications = true,
                MusicNotifs = true,
                HistoryNotifs = true,
                FoodNotifs = true,
                ArtNotifs = true,
                NatureNotifs = true,
                ArchitectureNotifs = true
            }; 
            return database.InsertAsync(new User());
        }

         /* Updates wether the user wants to recieve notifications */
        public Task<int> UpdateNotifications(User user, bool onOrOff)
        {
            user.Notifications = onOrOff;
            return database.UpdateAsync(user);
        }


        /**
         * Updates wether the user wants to recieve music notifications
         */
        public Task<int> UpdateMusicNotifs(User user, bool onOrOff)
        {

            user.MusicNotifs = onOrOff;
            return database.UpdateAsync(user);
            
        }

        /**
         * Updates wether the user wants to recieve history notifications
         */
        public Task<int> UpdateHistoryNotifs(User user, bool onOrOff)
        {
            user.HistoryNotifs = onOrOff;
            return database.UpdateAsync(user);
        }

        /**
         * Updates wether the user wants to recieve food notifications
         */
        public Task<int> UpdateFoodNotifs(User user, bool onOrOff)
        {
            user.FoodNotifs = onOrOff;
            return database.UpdateAsync(user);
        }

        /**
         * Updates wether the user wants to recieve art notifications
         */
        public Task<int> UpdateArtNotifs(User user, bool onOrOff)
        {
            user.ArtNotifs = onOrOff;
            return database.UpdateAsync(user);
        }

        /**
         * Updates wether the user wants to recieve nature notifications
         */
        public Task<int> UpdateNatureNotifs(User user, bool onOrOff)
        {
            user.NatureNotifs = onOrOff;
            return database.UpdateAsync(user);

        }

        /**
         * Updates wether the user wants to recieve architecture notifications
         */
        public Task<int> UpdateArchitectureNotifs(User user, bool onOrOff)
        {
            user.ArchitectureNotifs = onOrOff;
            return database.UpdateAsync(user);
        }
    }
}
