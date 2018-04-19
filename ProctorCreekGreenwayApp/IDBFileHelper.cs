using System;
namespace ProctorCreekGreenwayApp
{
    /**
     * Interface to retrieve the location of the SQLite Database for each OS
     */

    public interface IDBFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
