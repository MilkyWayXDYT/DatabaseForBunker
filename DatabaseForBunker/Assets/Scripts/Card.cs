using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private GameObject descriptionObj;

    public bool isSelected = false;

    public int id;
    public string name;
    public string description;
    public string modelPath;
    public int deckId;
    public int isLocal;

    /// <summary>
    /// ѕри нажатии на карту, считаетс€, что она выбрана, если до нее была выбрана друга€, то с той выбор сбрасываетс€
    /// </summary>
    public void SelectCard()
    {
        if (isSelected)
        {
            DeselectCard();
            return;
        }

        var cardsParent = GetComponent<Transform>().parent;
        for (int c = 0; c < cardsParent.childCount; c++)
        {
            if (cardsParent.GetChild(c).GetComponent<Card>())
                cardsParent.GetChild(c).GetComponent<Card>().DeselectCard();
        }

        GetComponent<Image>().color = new Color(0.68f, 0.41f, 0);
        var elements = GetComponentsInChildren<Transform>(true);
        foreach (var el in elements)
        {
            if (el.name == "Buttons")
                el.gameObject.SetActive(true);
            if (el.name == "Name")
                el.gameObject.SetActive(false);
        }

        isSelected = true;
    }

    /// <summary>
    /// —брос выбора на карточке
    /// </summary>
    public void DeselectCard()
    {
        GetComponent<Image>().color = new Color(0.85f, 0.52f, 0.0f);
        isSelected = false;
        var elements = GetComponentsInChildren<Transform>(true);
        foreach (var el in elements)
        {
            if (el.name == "Buttons")
                el.gameObject.SetActive(false);
            if (el.name == "Name")
                el.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ќткрытие окна с подробной информацией о карточке
    /// </summary>
    public void DescriptionOpen()
    {
        descriptionObj = GetComponent<Transform>().gameObject.transform.parent.parent.Find("Description").gameObject;
        descriptionObj.gameObject.SetActive(true);
        var elements = descriptionObj.GetComponentsInChildren<Transform>(true);
        foreach (var el in elements)
        {
            if (el.name == "Name")
                el.gameObject.GetComponent<TMP_Text>().text = name;
            if (el.name == "DescriptionText")
                el.gameObject.GetComponent<TMP_Text>().text = description;
            // todo добавить еще загрузку модели или картинки
        }
    }
}
