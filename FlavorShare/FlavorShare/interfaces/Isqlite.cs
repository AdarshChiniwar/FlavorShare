using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlavorShare.interfaces
{
    public interface Isqlite
    {
        SQLiteConnection GetConnection();
    }
}
