using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextScaler : MonoBehaviour
{
    private Image image;
    //[SerializeField] public LayoutElement textMP;

    [SerializeField] private TextMeshProUGUI textPreferedSize;

    private float width;
    void Start()
    {
        image = GetComponent<Image>();
        //textMP = textMP.GetComponent<LayoutElement>();
        textPreferedSize = textPreferedSize.GetComponent<TextMeshProUGUI>();
        width = image.rectTransform.sizeDelta.x;
    }
    
    
    public void SetTextPreferedSize(){
        //image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, textMP.preferredHeight);
        image.rectTransform.sizeDelta = new Vector2(width, textPreferedSize.preferredHeight);
        print("SetTextPreferedSize() by:" + gameObject.name + " ,prefered size is: " + textPreferedSize.preferredHeight);
    }
}
