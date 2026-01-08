using UnityEngine;
using System.Data.SQLite;
using System.IO;

public class DBHelper
{
    /// <summary>
    /// Подключение к БД
    /// </summary>
    /// <returns></returns>
    public static SQLiteConnection GetConnection()
    {
        string dataPath = Path.Combine(Application.persistentDataPath, "bunkerDB.db");
        string connectionString = $"Data Source={dataPath}";
        SQLiteConnection connection = new SQLiteConnection(connectionString);

        return connection; 
    }
}
