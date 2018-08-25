using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpots : MonoBehaviour {

    public static NPCSpots Instance;
    public GameObject[] m_spots;

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

}
