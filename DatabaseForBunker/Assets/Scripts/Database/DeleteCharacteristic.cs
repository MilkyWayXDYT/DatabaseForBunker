using System.Data.SQLite;
using UnityEngine;

public class DeleteCharacteristic : MonoBehaviour
{
    /// <summary>
    /// Вызов удаления карточки
    /// </summary>
    public void DeleteCardClick()
    {
        Card card = GetComponent<Card>();
        int cardId = card.id;
        LoadData loadData = card.transform.parent.parent.parent.GetComponent<LoadData>();
        string tableName = loadData.tableName;
        DeleteInDatabase(cardId, tableName);
        loadData.CharsChange(tableName);
    }

    /// <summary>
    /// Удаление карточки в БД
    /// </summary>
    /// <param name="id">Id удаляемой записи</param>
    /// <param name="tableName">Название таблицы</param>
    private void DeleteInDatabase(int id, string tableName)
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = $"delete from {tableName} where ID = {id}";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
