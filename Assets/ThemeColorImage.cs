using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeColorImage : ThemeColorChange
{
    public override void ChangeColor()
    {
        GetComponent<Image>().color = isLight ? darkColor : lightColor;
        isLight = !isLight;
    }
    
    public override void ChangeColor(bool setLight)
    {
        GetComponent<Image>().color = setLight ? lightColor : darkColor;
        isLight = setLight;
    }
}
