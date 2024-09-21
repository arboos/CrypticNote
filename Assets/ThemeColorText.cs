using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThemeColorText : ThemeColorChange
{
    public override void ChangeColor()
    {
        GetComponent<TextMeshProUGUI>().color = isLight ? darkColor : lightColor;
        isLight = !isLight;
    }

    public override void CheckColor()
    {
        
    }
    
    public override void ChangeColor(bool setLight)
    {
        GetComponent<TextMeshProUGUI>().color = setLight ? lightColor : darkColor;
        isLight = setLight;
    }
}
