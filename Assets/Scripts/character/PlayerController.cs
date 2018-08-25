using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UIExecute UIExecuteScript;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private GameData.PlayerData thePlayerData;
    [SerializeField] private GameData.LevelData theLevelData;
    private void Start()
    {
        if (thePlayerData == null || m_Rigidbody2D == null || UIExecuteScript == null)
        {
            Debug.Log("find error.");
            return;
        }

        thePlayerData.m_fSchedule = thePlayerData.m_fMaxSchedule;
        theLevelData.m_fLastTime = theLevelData.m_fMaxExecuteTime;
    }
    private void Update()
    {
        Fn_GetLastTime(1f);
    }
    private void LateUpdate()
    {
        Fn_Moveing();
    }
    private void Fn_Moveing()
    {
        Vector2 MovePosition = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (MovePosition.sqrMagnitude > 0.001f)
        {
            Vector3 direction = MovePosition.normalized;
            m_Rigidbody2D.velocity = direction * thePlayerData.m_fMoveSpeed;

            this.transform.localPosition = new Vector3(Mathf.Clamp(this.transform.localPosition.x , thePlayerData.m_fMinXRange , thePlayerData.m_fMaxXRange) 
                , Mathf.Clamp(this.transform.localPosition.x, thePlayerData.m_fMinYRange, thePlayerData.m_fMaxYRange) , this.transform.localPosition.z);
        }
        else
        {
            m_Rigidbody2D.velocity = Vector3.zero;
        }
    }
    private void Fn_GetLastTime(float speed)
    {
        if (theLevelData.m_fLastTime > 0f)
        {
            theLevelData.m_fLastTime -= speed * Time.deltaTime;
            UIExecuteScript.Img_CurrectTime.fillAmount = GameSystem.Instance.Fn_GetInverseLerp(0, theLevelData.m_fMaxExecuteTime, theLevelData.m_fLastTime);
        }
    }
}
