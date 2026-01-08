using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SQLite;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEditor;
using static UnityEngine.Rendering.DebugUI;

public class SharingData : MonoBehaviour
{
    public LoadData reloadPanels;
    private string[] tableName = { "Threat", "Profession", "Fact", "Age", "Disaster", "Hobby", "Phobia", "Health", "Bunker", "Luggage" };

    public void ExportData(string jsonPath = "C:\\Users\\User\\Desktop\\Characteristics.json")
    {
        jsonPath = EditorUtility.SaveFilePanel("Выберите папку для экспорта", "", "Characteristics", "json");
        var exportData = new JsonStruct();
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            foreach (var table in tableName)
            {
                string query = $"select * from {table} where isLocal = 1";
                var command = new SQLiteCommand(query, connection);

                var tableData = new List<Dictionary<string, object>>();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            
                            for (int i = 1; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }

                            tableData.Add(row);
                        }
                    }
                }

                exportData.table[table] = tableData;
            }
        }

        string json = JsonConvert.SerializeObject(exportData.table, Formatting.Indented);
        File.WriteAllText(jsonPath, json);
    }
    

    public void ImportData(string jsonPath = "C:\\Users\\User\\Desktop\\Characteristics.json")
    {
        jsonPath = EditorUtility.OpenFilePanel("Выберите файл для импорта", "", "json");

        string jsonStrings = File.ReadAllText(jsonPath);

        var importData = JToken.Parse(jsonStrings);

        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();
            foreach (var table in tableName)
            {
                if (importData[table].ToString() == "[]")
                    continue;
                else
                {
                    var obj = JArray.Parse(importData[table].ToString());
                    for (int i = 0; i < obj.Count; i++)
                    {
                        var row = new Dictionary<string, object>();
                        foreach (JProperty property in obj[i])
                        {
                            row[property.Name] = property.Value;
                        }
                        string columns = string.Join(", ", row.Keys);
                        string values = string.Join("', '", row.Values);

                        string query = $"insert into {table} ({columns}) values ('{values}')";
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        if (reloadPanels.GetComponent<Transform>().Find("Viewport").Find("Content").childCount != 0)
            reloadPanels.CharsChange(reloadPanels.tableName);
    }
}
