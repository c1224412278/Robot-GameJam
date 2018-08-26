using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public class UIExecute : MonoBehaviour
{
    #region 單例模式
    public static UIExecute Instance
    {
        get { return _Instance; }
        set { _Instance = value; }
    }
    private static UIExecute _Instance;
    #endregion

    public Image Img_CurrectFriend;
    private void Awake()
    {
        _Instance = this;
    }
    private void Start()
    {
        
    }
}
