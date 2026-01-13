using System.Data.SQLite;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddCharacteristic : MonoBehaviour
{
    private GameObject addPage;
    private Authorization authPage;

    string tableName;

    private void Start()
    {
        addPage = GetComponent<Transform>().parent.parent.Find("AddAndEdit").gameObject;
        authPage = GetComponent<Transform>().parent.parent.parent.parent.parent.Find("AuthPage").transform.GetComponent<Authorization>();
    }

    /// <summary>
    /// Открытие окна добавления карточки
    /// </summary>
    public void OpenAddPage()
    {
        addPage.SetActive(true);
        addPage.transform.Find("BackButton").gameObject.GetComponent<Button>().onClick.AddListener(CloseAddPage);
        addPage.transform.GetChild(1).Find("SaveButton").gameObject.GetComponent<Button>().onClick.AddListener(AddCharClick);
    }

    /// <summary>
    /// Обработка нажатия кнопки сохранения карточки
    /// </summary>
    public void AddCharClick()
    {
        try
        {
            DatabaseAdd();
        }
        catch 
        {
            Debug.LogError("Ошибка добавления записи в базу данных");
        }

        CloseAddPage();
        LoadData loadData = addPage.GetComponent<Transform>().parent.parent.GetComponent<LoadData>();
        loadData.CharsChange(tableName);
    }

    /// <summary>
    /// Добавление данных новой карточки в БД
    /// </summary>
    private void DatabaseAdd()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            tableName = gameObject.transform.parent.parent.parent.GetComponent<LoadData>().tableName;

            string name = addPage.transform.GetChild(1).Find("NameInput").GetComponent<TMP_InputField>().text;
            string description = addPage.transform.GetChild(1).Find("DescriptionInput").GetComponent<TMP_InputField>().text;
            string modelPath = addPage.transform.GetChild(1).Find("SelectImgButton").GetComponentInChildren<TMP_Text>().text;
            if (modelPath == "Выбрать картинку")
                modelPath = "";
            int deckId = gameObject.transform.parent.parent.parent.GetComponent<LoadData>().deckId;

            string query = $"insert into {tableName} (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('{name}', '{description}', '{modelPath}', {deckId}, 1, {authPage.userID})";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Закрытие окна добавления новых карточек
    /// </summary>
    public void CloseAddPage()
    {
        addPage.SetActive(false);
    }
}
