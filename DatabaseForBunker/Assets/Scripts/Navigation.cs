using UnityEngine;

public class Navigation : MonoBehaviour
{
    [SerializeField]
    GameObject openedPage;
    [SerializeField]
    bool closePage;

    /// <summary>
    /// Перемещение между окнами
    /// </summary>
    public void OpenPage()
    {
        if (openedPage != null) 
            openedPage.SetActive(true);

        if (closePage)
        {
            GameObject closedPage = transform.parent.GetComponent<Transform>().gameObject;
            closedPage.SetActive(false);
        }
    }

    /// <summary>
    /// Выход из приложения
    /// </summary>
    public void ExitButton()
    {
        Application.Quit();
    }
}
