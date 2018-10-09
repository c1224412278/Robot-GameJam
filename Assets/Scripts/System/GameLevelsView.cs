using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Level
{
    public Vector3 center;
    public float cameraSize;
}

public class GameLevelsView : MonoBehaviour {

    public static GameLevelsView Instance;
    public List<Level> m_levels;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeCameraView(int currLevel)
    {
        Camera.main.transform.position = m_levels[currLevel].center;
        Camera.main.orthographicSize = m_levels[currLevel].cameraSize;
    }


    public static void ChangeCameraView(Level level)
    {
        Camera.main.transform.position = level.center;
        Camera.main.orthographicSize = level.cameraSize;
    }

    public static void SetCameraView(Level level)
    {
        level.center = Camera.main.transform.position;
        level.cameraSize = Camera.main.orthographicSize;
    }

    public static void SelectMainCamera()
    {
        Selection.objects = new GameObject[] { Camera.main.gameObject};
    }
}
