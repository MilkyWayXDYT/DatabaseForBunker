using System.Data.SQLite;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Authorization : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameInput;
    [SerializeField]
    private TMP_InputField passwordInput;
    [SerializeField]
    private TMP_Text buttonText;
    [SerializeField]
    private Button regButton;
    [SerializeField]
    private GameObject messagePage;
    [SerializeField]
    private GameObject authPage;

    public bool isAdmin = false;

    public bool isAuth = false;
    private bool isHide = true;

    /// <summary>
    /// Проверка на авторизацию
    /// </summary>
    public void CheckAuth()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = "select Login, Password, Role from Users where Authorization = 1";

            var command = new SQLiteCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                isAuth = reader.HasRows;

                if (isAuth) // выход
                {
                    while (reader.Read())
                    {
                        buttonText.text = "Выйти";
                        nameInput.text = reader.GetString(0);
                        passwordInput.text = reader.GetString(1);
                        nameInput.enabled = !nameInput.enabled;
                        passwordInput.enabled = !passwordInput.enabled;
                        regButton.enabled = !regButton.enabled;
                        isAdmin = reader.GetString(2) == "Admin";
                    }
                }
                else // вход
                {
                    buttonText.text = "Войти";
                } 
            }
        }
    }

    /// <summary>
    /// Авторизация
    /// </summary>
    public void Auth()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = $"select Role from Users where Login = '{nameInput.text}' and Password = '{passwordInput.text}'";
            var command = new SQLiteCommand(query, connection);

            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        isAdmin = reader.GetString(0) == "Admin";
                    }
                }
                else
                {
                    nameInput.GetComponent<Image>().color = Color.pink;
                    passwordInput.GetComponent<Image>().color = Color.pink;
                    messagePage.SetActive(true);
                    messagePage.GetComponentInChildren<Transform>().Find("HeaderText").GetComponentInChildren<TMP_Text>().text = "Проверьте введенные данные и повторите снова";
                    return;
                }
            }
            isAuth = !isAuth;

            if (isAuth) // выход
            {
                buttonText.text = "Выйти";
                query = $"update Users set Authorization = 1 where Login = '{nameInput.text}' and Password = '{passwordInput.text}'";
                isAdmin = false;
            }
            else // вход
            {
                buttonText.text = "Войти";
                query = $"update Users set Authorization = 0 where Login = '{nameInput.text}' and Password = '{passwordInput.text}'";
            }

            command.CommandText = query;
            command.ExecuteNonQuery();

            nameInput.enabled = !nameInput.enabled;
            passwordInput.enabled = !passwordInput.enabled;
            regButton.enabled = !regButton.enabled;
        }

        messagePage.SetActive(true);
        messagePage.GetComponentInChildren<Transform>().Find("HeaderText").GetComponentInChildren<TMP_Text>().text = "Операция успешно выполнена";
    }

    public void Registration()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = $"select Login from Users where Login = '{nameInput.text}'";
            var command = new SQLiteCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    messagePage.SetActive(true);
                    messagePage.GetComponentInChildren<Transform>().Find("HeaderText").GetComponentInChildren<TMP_Text>().text = "Такой пользователь уже существует";
                    return;
                }
            }

            query = $"insert into Users (Login, Password, Role, Authorization) values ('{nameInput.text}', '{passwordInput.text}', 'User', 0);";
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        GetComponentInChildren<Transform>().Find("BackButton").GetComponent<Button>().onClick.Invoke();
        authPage.GetComponentInChildren<Transform>().Find("LoginInput").GetComponent<TMP_InputField>().text = nameInput.text;
        authPage.GetComponentInChildren<Transform>().Find("PasswordInput").GetComponent<TMP_InputField>().text = passwordInput.text;
        authPage.GetComponentInChildren<Transform>().Find("AuthButton").GetComponent<Button>().onClick.Invoke();
    }

    // добавить новый столбик (создатель карточки) и добавить в вывод на странице подробнее. Проверить везде где есть получение данных на захват и этого столбика

    /// <summary>
    /// Скрытие пароля
    /// </summary>
    public void HidePassword()
    {
        isHide = !isHide;
        if (isHide)
            passwordInput.contentType = TMP_InputField.ContentType.Password;
        else
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
        passwordInput.Select();
    }
}
