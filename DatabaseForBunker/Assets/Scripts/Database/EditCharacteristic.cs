using System.Data.SQLite;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditCharacteristic : MonoBehaviour
{
    private GameObject editPage;

    string tableName;

    private void Start()
    {
        editPage = GetComponent<Transform>().parent.parent.Find("AddAndEdit").gameObject;
    }

    /// <summary>
    /// Открыть окно редактирования характеристики
    /// </summary>
    public void OpenEditPage()
    {
        editPage.SetActive(true);
        editPage.transform.Find("BackButton").gameObject.GetComponent<Button>().onClick.AddListener(CloseEditPage);
        editPage.transform.GetChild(1).Find("SaveButton").gameObject.GetComponent<Button>().onClick.AddListener(EditCharClick);

        Card card = GetComponent<Card>();

        editPage.transform.GetChild(1).Find("NameInput").GetComponent<TMP_InputField>().text = card.name;
        editPage.transform.GetChild(1).Find("DescriptionInput").GetComponent<TMP_InputField>().text = card.description;
        var panel = editPage.transform.GetChild(1).Find("Model").gameObject.GetComponent<ImageOrModelPanel>();
        panel.fileName = card.modelPath;
        StartCoroutine(panel.FileSetToPanel());
        editPage.transform.GetChild(1).Find("SelectImgButton").GetComponentInChildren<TMP_Text>().text = card.modelPath == "" ? "Выбрать картинку" : card.modelPath;
    }

    /// <summary>
    /// Сохранить изменение характеристики
    /// </summary>
    public void EditCharClick()
    {
        try
        {
            DatabaseEdit();
        }
        catch { }

        CloseEditPage();
        LoadData loadData = editPage.GetComponent<Transform>().parent.parent.GetComponent<LoadData>();
        loadData.CharsChange(tableName);
    }

    /// <summary>
    /// Изменение характеристики в БД
    /// </summary>
    private void DatabaseEdit()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            tableName = gameObject.transform.parent.parent.parent.GetComponent<LoadData>().tableName;

            string name = editPage.transform.GetChild(1).Find("NameInput").GetComponent<TMP_InputField>().text;
            string description = editPage.transform.GetChild(1).Find("DescriptionInput").GetComponent<TMP_InputField>().text;
            string modelPath = editPage.transform.GetChild(1).Find("SelectImgButton").GetComponentInChildren<TMP_Text>().text;
            if (modelPath == "Выбрать картинку")
                modelPath = "";

            Card card = GetComponent<Card>();

            string query = $"update {tableName} set Name = '{name}', Description = '{description}', ModelPath = '{modelPath}' where ID = {card.id}";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Закрыть окно изменения
    /// </summary>
    public void CloseEditPage()
    {
        editPage.SetActive(false);
    }
}
