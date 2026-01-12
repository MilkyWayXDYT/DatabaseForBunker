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
                    return;
                }
            }

            if (isAuth) // выход
            {
                buttonText.text = "Войти";
                query = $"update Users set Authorization = 0 where Login = '{nameInput.text}' and Password = '{passwordInput.text}'";
                isAdmin = false;
            }
            else // вход
            {
                buttonText.text = "Выйти";
                query = $"update Users set Authorization = 1 where Login = '{nameInput.text}' and Password = '{passwordInput.text}'";
            }

            command.CommandText = query;
            command.ExecuteNonQuery();

            isAuth = !isAuth;
            nameInput.enabled = !nameInput.enabled;
            passwordInput.enabled = !passwordInput.enabled;
        }
    }

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
