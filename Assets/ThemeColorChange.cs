using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeColorChange : MonoBehaviour
{
    public Color lightColor = Color.white;
    public Color darkColor = new Color(70, 70, 70, 255);

    protected bool isLight = false;
    
    public virtual void ChangeColor()
    {
        throw new System.NotImplementedException();
    }
    
    public virtual void ChangeColor(bool setLight)
    {
        throw new System.NotImplementedException();
    }
    
    public virtual void CheckColor()
    {
        throw new System.NotImplementedException();
    }
}
