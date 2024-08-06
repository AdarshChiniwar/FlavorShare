using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FlavorShare.Droid;
using FlavorShare.interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQliteDroid))]
namespace FlavorShare.Droid
{
    public class SQliteDroid : Isqlite
    {
        public static string FullPath;
        public static string DbDirectoryPath;
        public SQLiteConnection GetConnection()
        {
            var dbase = "flavourshare.sql";
            DbDirectoryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            FullPath = Path.Combine(DbDirectoryPath, dbase);
            var connection = new SQLiteConnection(FullPath);
            return connection;
        }
    }
}