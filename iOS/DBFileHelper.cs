using System;
using System.IO;
using Xamarin.Forms;
using ProctorCreekGreenwayApp.iOS;

[assembly: Dependency(typeof(DBFileHelper))]
namespace ProctorCreekGreenwayApp.iOS
{
    public class DBFileHelper : IDBFileHelper
    {
        public string GetLocalFilePath(string filename) {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
               Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);

        }
    }
}
