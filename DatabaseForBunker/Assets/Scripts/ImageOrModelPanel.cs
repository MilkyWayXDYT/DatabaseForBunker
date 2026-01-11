using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageOrModelPanel : MonoBehaviour
{
    public string fileName;

    public IEnumerator FileSetToPanel()
    {
        string defaultPath = Application.persistentDataPath;
        string url = "file://" + defaultPath + "/" + fileName;
        string extention = fileName.Split(new char[] { '.' })[1];
        using (WWW www = new WWW(url))
        {
            yield return www;

            float texW = www.texture.width;
            float texH = www.texture.height;
            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, texW, texH), new Vector2());

            var imageObj = GetComponentsInChildren<Transform>()[1];
            float width = imageObj.GetComponent<RectTransform>().sizeDelta.x;
            float height = imageObj.GetComponent<RectTransform>().sizeDelta.y;

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
}
