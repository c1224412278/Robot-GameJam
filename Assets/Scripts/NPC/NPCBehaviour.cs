using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Enum_NeedServiceKind       //當前需要被服務的總類
{
    None = -1,         //無
    Drink = 0 ,        //喝
    Sweep = 1,         //掃
    Garbage = 2,       //垃圾
}
public class NPCBehaviour : MonoBehaviour
{
    private Enum_NeedServiceKind theNeedServiceKind;

    public Vector3 m_target;
    public float m_moveSpeed;
    public int m_moveCount;
    public float m_waitTimel;
    private bool m_moveComplete;

    public float m_angryValue;
    public float m_addAngryTime;

    public float m_needHpleTime;
    public bool m_needHelpBool;

    private float m_fLoadTime;              //等待幫助的時間
    
    private float m_fHelpingTime;           //幫助別人所需要的時間
    private float m_fMaxHelpingTime;        //幫助最大等待時間

    private bool m_helpedComplete;          //判斷是否完成幫助
    private bool m_IsExecuteingHelp;        //判斷是否正在幫助人

    public Image m_helpImage;
    public Canvas m_DialogCanvas;

    private Sprite Spr_Help;                //需求 Sprtie
    private Sprite Spr_Loading;             //Load Sprite
    private Rigidbody2D m_rigidbody;

    private PlayerController PlayerControllerScript;
    [SerializeField] private GameData.NpcData theNpcData;
	void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        Spr_Loading = Resources.Load<Sprite>("Sprite/Load/w1");

        m_fMaxHelpingTime = 1.5f;                               //設定最大幫助時間
        m_moveCount = Random.Range(3, 5);
        m_waitTimel = Random.Range(5, 10);
        m_needHpleTime = Random.Range(5f, 15f);

        theNeedServiceKind = Enum_NeedServiceKind.None;         //當前沒有需要被服務的需求

        PlayerControllerScript = FindObjectOfType<PlayerController>();          //抓取角色控制腳本

        StartCoroutine(Move());
        StartCoroutine(NeedHelp());
    }
    private void Update()
    {
        if ((int)theNeedServiceKind == (int)PlayerControllerScript.theRobotKind)
        {
            this.GetComponent<Collider2D>().enabled = true;          //開啟碰撞器
        }
        else
            this.GetComponent<Collider2D>().enabled = false;         //關閉碰撞器


        if (m_needHelpBool && !m_IsExecuteingHelp)                   //正在等待玩家幫助中
        {
            m_fLoadTime -= Time.deltaTime;
            if (m_fLoadTime <= 0f)
            {
                Debug.Log("幫助失敗 .. !!");
                PlayerControllerScript.theLevelData.m_fFriendValue -= 1;
                DisableHelpImage();
            }
        }


        if (m_IsExecuteingHelp)
        {
            if (m_fHelpingTime > 0)
            {
                m_fHelpingTime -= Time.deltaTime;
            }
            else
            {
                DisableHelpImage();

                m_IsExecuteingHelp = false;
                m_helpedComplete = false;                            //幫助完成
            }
        }
    }

    private IEnumerator AddAngryValue()
    {
        yield return GameSystem.Instance.m_IsGameExecute;

        while (m_moveCount > 0)
        {
            if (m_needHelpBool)
            {
                yield return new WaitForSeconds(m_needHpleTime);
                m_angryValue += 1;
            }
            yield return null;
        }
    }

    private IEnumerator Move()
    {
        yield return GameSystem.Instance.m_IsGameExecute;

        while (m_moveCount > 0)
        {
            if (m_moveCount <= 1) SetExitPos();
            else SetNextPos();

            while (m_moveComplete == false)
            {
                while (m_needHelpBool == true)
                {
                    m_rigidbody.velocity = Vector2.zero;
                    yield return null;
                }

                m_rigidbody.velocity = (m_target - transform.position).normalized * m_moveSpeed;

                if((transform.position - m_target).sqrMagnitude <= 0.1f)
                {
                    m_moveComplete = true;
                }
                yield return null;
                
            }

            m_rigidbody.velocity = Vector2.zero;
            m_moveCount--;
            yield return new WaitForSeconds(m_waitTimel);
           
        }

        GameSystem.Instance.Fn_NPCExit();
        Destroy(gameObject);

        yield return null;
    }

    private IEnumerator NeedHelp()
    {
        yield return GameSystem.Instance.m_IsGameExecute;

        while (m_moveCount > 0)
        {
            while (m_needHelpBool)
            {
                //顯示圖示後，協程暫停
                yield return null;
            }

            yield return new WaitForSeconds(m_needHpleTime);

            int number = Random.Range(0 , theNpcData.Spr_HelpKinds.Length);
            Spr_Help = theNpcData.Fn_GetHelpLogo(number);            //取得幫助的圖示
            if (number == 0)
            {
                theNeedServiceKind = Enum_NeedServiceKind.Drink;
            }
            else if (number == 1)
            {
                theNeedServiceKind = Enum_NeedServiceKind.Garbage;
            }
            else if (number == 2)
            {
                theNeedServiceKind = Enum_NeedServiceKind.Sweep;
            }


            if (Spr_Help != null)               //當要顯示的需求圖示不等於 null 時
            {
                EnableHelpImage(Spr_Help);
            }
        }
        yield return null;
    }

    private void SetNextPos()
    {
        m_target = NPCSpots.Instance.GetRandomSpot();
        m_moveComplete = false;
    }

    private void SetExitPos()
    {
        m_target = NPCSpots.Instance.GetRandomExitSpot();
        m_moveComplete = false;
    }

    private void EnableHelpImage(Sprite request)
    {
        m_helpImage.sprite = request;
        m_DialogCanvas.enabled = true;
        m_fLoadTime = 3f;
        m_needHelpBool = true;
    }

    public void DisableHelpImage()
    {
        m_DialogCanvas.enabled = false;
        m_needHelpBool = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_helpedComplete)          //當需求尚未得到滿足時
        {
            if (collision.gameObject.tag == "Player" && m_needHelpBool)
            {
                if (!m_IsExecuteingHelp)
                {
                    m_helpImage.sprite = Spr_Loading;
                    m_DialogCanvas.enabled = true;

                    m_moveComplete = true;

                    m_fHelpingTime = m_fMaxHelpingTime;     //給予幫助所需等待時間
                    m_IsExecuteingHelp = true;              //判斷正在幫助 npc

                    StartCoroutine(Fn_RotationLoad());      //Loading Sprite Rotation
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_IsExecuteingHelp)          //當角色正在幫助別人時，離開
        {
            if (collision.gameObject.tag == "Player")
            {
                if (m_IsExecuteingHelp)
                {
                    DisableHelpImage();
                    m_IsExecuteingHelp = false;              //判斷取消了幫助 npc
                }
            }
        }
    }
    private IEnumerator Fn_RotationLoad()           //旋轉 Load 圖示
    {
        while (m_IsExecuteingHelp)
        {
            m_helpImage.transform.Rotate(Vector3.forward * 75f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        //幫助取消了
        m_helpImage.transform.eulerAngles = Vector3.zero;
    }
}
