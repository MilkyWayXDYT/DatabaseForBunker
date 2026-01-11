using JetBrains.Annotations;
using System.Collections;
using System.IO;
using System.Security.Policy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageOrModelPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject imageError;
    [SerializeField]
    private TMP_Text fileNameButton;
    public string fileName;

    /// <summary>
    /// Установка картинки в панели при открытии окна изменения или подробной информации
    /// </summary>
    /// <returns></returns>
    public IEnumerator FileSetToPanel()
    {
        string defaultPath = Application.persistentDataPath;
        if (!File.Exists(defaultPath + "/" + fileName) && fileName != "")
        {
            imageError.SetActive(true);
        }
        if (fileName != "")
        {
            string url = "file://" + defaultPath + "/" + fileName;
            string extention = fileName.Split(new char[] { '.' })[1];
            using (WWW www = new WWW(url))
            {
                yield return www;

                float texW = www.texture.width;
                float texH = www.texture.height;
                Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, texW, texH), new Vector2());

                var imageObj = GetComponentsInChildren<Transform>()[1];
                float width = 517;
                float height = 348;

                float i;
                if (texH - height > texW - width)
                    i = texH / height;
                else
                    i = texW / width;
                imageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(texW / i, texH / i);
                imageObj.GetComponent<Image>().sprite = sprite;
                imageObj.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            ResetImage();
        }
        
    }


    /// <summary>
    /// Сброс выбранной картинки
    /// </summary>
    public void ResetImage()
    {
        if (transform.parent.parent.name == "AddAndEdit")
            fileNameButton.text = "Выбрать картинку";
        var imageObj = GetComponentsInChildren<Transform>()[1];
        imageObj.GetComponent<Image>().sprite = null;
        imageObj.GetComponent<Image>().color = Color.white;
    }
}
