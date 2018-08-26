using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpots : MonoBehaviour {

    public static NPCSpots Instance;

    [Header("觀光景點")]
    public GameObject[] m_spots;

    [Header("進出景點")]
    public GameObject[] m_exitSpots;

    private void Awake()
    {
        Instance = this;
    }

    public Vector2 GetNextSpot(int index)
    {
        return m_spots[index].transform.position;
    }

    public Vector2 GetRandomSpot()
    {
        return m_spots[Random.Range(0, m_spots.Length)].transform.position;
    }

    public Vector2 GetRandomExitSpot()
    {
        return m_exitSpots[Random.Range(0, m_spots.Length)].transform.position;
    }

}
