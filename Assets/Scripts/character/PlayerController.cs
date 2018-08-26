using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Enum_RobotKind
    {
        DrinkRobot = 0 ,       //販賣機器人
        GarbageRobot = 1,      //垃圾機器人
        SweepRobot = 2,        //掃地機器人
        
    }

    public bool m_IsAllowMove
    {
        get { return m_AllowMove; }
        set { m_AllowMove = value; }
    }
    private bool m_AllowMove;

    public Enum_RobotKind theRobotKind;               //當前機器人的種類
    public GameData.LevelData theLevelData;           //當前關卡資料

    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private GameData.PlayerData thePlayerData;         //玩家資料
    private void Start()
    {
        if (thePlayerData == null || m_Rigidbody2D == null)
        {
            Debug.Log("find error.");
            return;
        }

        m_AllowMove = true;                       //允許玩家移動
        thePlayerData.m_fSchedule = thePlayerData.m_fMaxSchedule;
        theLevelData.m_fFriendValue = theLevelData.m_fMaxFriendValue;

        AudioManager.Instance.Fn_PlayBgm(AudioManager.Instance.theAudioData.Clip_GameBgm);
        StartCoroutine(Fn_SetHelpValue());
    }
    private void LateUpdate()
    {
        if (GameSystem.Instance.m_IsGameExecute && m_AllowMove)
        {
            Fn_Moveing();
        }
    }
    private void Fn_Moveing()
    {
        Vector2 MovePosition = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (MovePosition.sqrMagnitude > 0.001f)
        {
            Vector3 direction = MovePosition.normalized;
            m_Rigidbody2D.velocity = direction * thePlayerData.m_fMoveSpeed;

            this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x , thePlayerData.m_fMinXRange , thePlayerData.m_fMaxXRange) 
                , Mathf.Clamp(this.transform.position.y , thePlayerData.m_fMinYRange, thePlayerData.m_fMaxYRange) , this.transform.position.z);
        }
        else
        {
            m_Rigidbody2D.velocity = Vector3.zero;
        }
    }
    private IEnumerator Fn_SetHelpValue()
    {
        while (true)
        {
            UIExecute.Instance.Img_CurrectFriend.fillAmount = GameSystem.Instance.Fn_GetInverseLerp(0, theLevelData.m_fMaxFriendValue, theLevelData.m_fFriendValue);
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator Fn_SetSlipMoveing()             //滑倒移動
    {
        Vector2 MovePosition = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        while (!m_AllowMove)
        {
            Debug.Log("slip move.");
            m_Rigidbody2D.velocity = MovePosition * 1f;
            yield return new WaitForEndOfFrame();
        }
    }


    // return 
    public GameData.PlayerData Fn_ReturnPlayerControllerData()
    {
        return thePlayerData;
    }
}
