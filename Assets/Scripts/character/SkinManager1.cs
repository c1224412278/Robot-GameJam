using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RobotType
{
    TrashRobot,
    DrinksRobot,
    SweepingRobot
}

[System.Serializable]
public class Robot
{
    public string name;
    public RobotType type;
    public GameObject objcet;
}

public class SkinManager1 : MonoBehaviour {

    public List<Robot> m_robots;

    public GameObject GetRobotObjcet(RobotType robotType)
    {
        for(int i=0; i< m_robots.Count; i++)
        {
            if (m_robots[i].type == robotType)
                return m_robots[i].objcet;
        }
        return null;
    }

    public Rigidbody2D GetRobotRigidbody(RobotType robotType)
    {
        for (int i = 0; i < m_robots.Count; i++)
        {
            if (m_robots[i].type == robotType)
                return m_robots[i].objcet.GetComponent<Rigidbody2D>();
        }
        return null;
    }

}
