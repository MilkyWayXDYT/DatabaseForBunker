using SFB;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SelectFile : MonoBehaviour
{
    private ImageOrModelPanel panel;

    private string imageOrModelPath;
    private string fileName;

    /// <summary>
    /// Выбор картинки для характеристики
    /// </summary>
    public void SelectImageOrModel()
    {
        try
        {
            var extentions = new[]
            {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg"),
            };
            imageOrModelPath = StandaloneFileBrowser.OpenFilePanel("Выберите картинку", "", extentions, false)[0];
        }
        catch
        {
            return;
        }

        panel = this.GetComponent<Transform>().parent.Find("Model").GetComponent<ImageOrModelPanel>();
        Regex regex = new Regex(@"[^\\]+$");
        MatchCollection matches = regex.Matches(imageOrModelPath);
        fileName = matches[0].ToString();
        panel.fileName = fileName;
        this.GetComponentInChildren<TMP_Text>().text = fileName;
        MoveFile();

        StartCoroutine(panel.FileSetToPanel());
    }

    /// <summary>
    /// Перемещение файла из первоначального места в C://Users/User/AppData/LocalLow/DefaultCompany/DatabaseForBunker
    /// </summary>
    public void MoveFile()
    {
        string newPath = Path.Combine(Application.persistentDataPath, fileName);
        FileInfo fileInfo = new FileInfo(imageOrModelPath);
        if (fileInfo.Exists)
        {
            fileInfo.CopyTo(newPath, true);
        }
    }
}
