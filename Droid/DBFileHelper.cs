using System;
using System.IO;
using Xamarin.Forms;
using ProctorCreekGreenwayApp.Droid;

[assembly: Dependency(typeof(DBFileHelper))]
namespace ProctorCreekGreenwayApp.Droid
{
    public class DBFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
