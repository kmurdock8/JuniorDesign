using System;
using SQLite;

namespace ProctorCreekGreenwayApp
{
    public class User
    {
        /**
         * Format of a table in the SQLite local database
         * Each method represents a column in the table
         */

        /* Creates a unique ID for each user */
        [PrimaryKey, AutoIncrement, NotNull]
        public int ID { get; set; }

        /* Whether or not user has notifications on or off */
        public bool Notifications { get; set; }

        /* True/false for notifications about music */
        public bool MusicNotifs { get; set; }

        /* True/false for notifications about history */
        public bool HistoryNotifs { get; set; }

        /* True/false for notifications about food */
        public bool FoodNotifs { get; set; }

        /* True/false for notifications about art */
        public bool ArtNotifs { get; set; }

        /* True/false for notifications about nature */
        public bool NatureNotifs { get; set; }

        /* True/false for notifications about architecture */
        public bool ArchitectureNotifs { get; set; }
    }
}
