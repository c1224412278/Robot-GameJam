using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance
    {
        get { return _Instance; }
        set { _Instance = value; }
    }
    private static GameSystem _Instance;
    private void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public float Fn_GetInverseLerp(float min , float max , float currect)
    {
        return Mathf.InverseLerp(min , max , currect);
    }
}
