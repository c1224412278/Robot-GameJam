using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float m_speed;
    public Bounds m_moveBound;

    private Vector2 m_moveRange;
    private Rigidbody2D m_botRigidbody;
    private GameSystem m_gameSystem;
    private SkinManager1 m_skinManager;

    private Vector2 m_movement;

    private void Start () {
        m_gameSystem = GameSystem.Instance;
        m_skinManager = GetComponent<SkinManager1>();
        m_botRigidbody = m_skinManager.GetRobotRigidbody(RobotType.TrashRobot);

        StartCoroutine(Move());
    }

    public void SetPlayerMoveBound(int curLevel)
    {
        Level level = GameLevelsView.Instance.m_levels[curLevel];
        m_moveBound.center = level.center;
        // 因為畫面是4:3在浮動，所以需要去計算寬的數值
        m_moveBound.extents = new Vector3((level.cameraSize / 3f) * 4f, level.cameraSize, 0);
    }

    private IEnumerator Move()
    {
        StartCoroutine(SelectRobot());

        while (m_gameSystem.m_IsGameExecute)
        {
            m_movement.x = Input.GetAxis("Horizontal") * m_speed;
            m_movement.y = Input.GetAxis("Vertical") * m_speed;

            m_botRigidbody.velocity = m_movement;
            m_botRigidbody.transform.position = new Vector2(
                Mathf.Clamp(m_botRigidbody.position.x, m_moveBound.min.x, m_moveBound.max.x),
                Mathf.Clamp(m_botRigidbody.position.y, m_moveBound.min.y, m_moveBound.max.y));

            yield return SelectRobot();
        }

        yield return null;
    }

    private IEnumerator SelectRobot()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            // Trash Bot
            m_botRigidbody.velocity = Vector2.zero;
            m_botRigidbody = m_skinManager.GetRobotRigidbody(RobotType.TrashRobot);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            // Drinks Bot
            m_botRigidbody.velocity = Vector2.zero;
            m_botRigidbody = m_skinManager.GetRobotRigidbody(RobotType.DrinksRobot);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            // Sweeping Bot
            m_botRigidbody.velocity = Vector2.zero;
            m_botRigidbody = m_skinManager.GetRobotRigidbody(RobotType.SweepingRobot);
        }

        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_moveBound.center, m_moveBound.size);
    }

}
