using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public class UIManager
{
    public Tweener Fn_SetImageAlpha(Image img , float alpha , float time)
    {
        return img.DOColor(new Color(img.color.r , img.color.g , img.color.b , alpha) , time);
    }
    public Tweener Fn_SetTextAlpha(Text text , float alpha, float time)
    {
        return text.DOColor(new Color(text.color.r, text.color.g, text.color.b, alpha), time);
    }
}
