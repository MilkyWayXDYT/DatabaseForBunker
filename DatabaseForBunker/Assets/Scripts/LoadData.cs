using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Data.SQLite;
using TMPro;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    [SerializeField]
    private GameObject charactPrefab;
    [SerializeField]
    private GameObject addCharactPrefab;
    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private Toggle isLocalToggle;

    public string tableName;
    public int deckId;
    private List<Card> chars;

    private void OnEnable()
    {
        for (int c = 0; c < content.transform.childCount; c++)
        {
            Destroy(content.transform.GetChild(c).gameObject);
        }
    }

    /// <summary>
    /// Функция для перезаполнения скорллера с карточками характеристик
    /// </summary>
    /// <param name="tableName"> Название таблицы для БД.</param>
    public void CharsChange(string tableName = "")
    {
        OnEnable();
        charactPrefab.GetComponent<RectTransform>().anchorMax.Set(0, 0.5f);
        charactPrefab.GetComponent<RectTransform>().anchorMin.Set(0, 0.5f);
        if (tableName != "")
            this.tableName = tableName;
        GetData();
        if (chars.Count != 0)
            deckId = chars[0].deckId;

        float contentSize = (chars.Count + 4 - (chars.Count % 4)) * 363 / 4;
        content.sizeDelta = new Vector2(content.sizeDelta.x, contentSize);

        int i = 0;
        int j = 0;
        Vector2 cardTransform;
        GameObject creatingCard;
        foreach (var card in chars)
        {
            cardTransform = new Vector2(i * 300 + 164, contentSize / 2 - (196 + (j * 360)));
            creatingCard = Instantiate(charactPrefab, cardTransform, Quaternion.identity);
            var prefName = creatingCard.GetComponentInChildren<TMP_Text>();
            prefName.text = card.name;
            var cardInf = creatingCard.GetComponent<Card>();
            cardInf.id = card.id;
            cardInf.name = card.name;
            cardInf.description = card.description;
            cardInf.modelPath = card.modelPath;
            cardInf.deckId = card.deckId;
            cardInf.isLocal = card.isLocal;
            creatingCard.transform.SetParent(content.transform, false);

            i++;
            if (i == 4)
            {
                i = 0;
                j++;
            }
        }
        cardTransform = new Vector2(i * 300 + 164, contentSize / 2 - (196 + (j * 360)));
        creatingCard = Instantiate(addCharactPrefab, cardTransform, Quaternion.identity);
        creatingCard.transform.SetParent(content.transform, false);
    }

    /// <summary>
    /// Получение данных по выбранной колоде
    /// </summary>
    private void GetData()
    {
        chars = new List<Card>();
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query;
            if (isLocalToggle.isOn)
                query = $"select * from {tableName} where isLocal = 1";
            else 
                query = $"select * from {tableName}";

                var command = new SQLiteCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows) {
                    while (reader.Read())
                    {
                        Card deck = new Card();
                        deck.id = reader.GetInt32(0);
                        deck.name = reader.GetValue(1).ToString();
                        if (tableName != "Age")
                        {
                            deck.description = reader.GetString(2);
                            deck.modelPath = reader.GetString(3);
                            deck.deckId = int.Parse(reader.GetValue(4).ToString());
                            deck.isLocal = int.Parse(reader.GetValue(5).ToString());
                        }
                        else
                        {
                            deck.deckId = int.Parse(reader.GetValue(2).ToString());
                            deck.isLocal = int.Parse(reader.GetValue(3).ToString());
                        }

                        chars.Add(deck);
                    }
                }
            }
        }
    }
}
