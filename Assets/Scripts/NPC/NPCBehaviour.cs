using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBehaviour : MonoBehaviour {

    public Vector3 m_target;
    public float m_moveSpeed;
    public int m_moveCount;
    public float m_waitTimel;
    private bool m_moveComplete;

    public float m_angryValue;
    public float m_addAngryTime;

    public float m_needHpleTime;
    public bool m_needHelpBool;

    /*private bool m_IsExecuteingHelp;        //判斷是否正在幫助人
    private bool m_helpedComplete;          //判斷是否完成幫助
    private float m_fHelpingTime;           //幫助別人所需要的時間*/

    public Image m_helpImage;
    public Canvas m_DialogCanvas;
    private Sprite Spr_Help;                //需求 Sprtie
    private Sprite Spr_Loading;             //Load Sprite
    private Rigidbody2D m_rigidbody;

	void Start () {

        m_rigidbody = GetComponent<Rigidbody2D>();
        Spr_Loading = Resources.Load<Sprite>("Sprite/Load/w1");

        m_moveSpeed = Random.Range(0.5f, 1.5f);
        m_moveCount = Random.Range(3, 5);
        m_waitTimel = Random.Range(5, 10);
        m_needHpleTime = Random.Range(5f, 15f);

        StartCoroutine(Move());
        StartCoroutine(NeedHelp());
    }
	
    private IEnumerator AddAngryValue()
    {
        while(m_moveCount > 0)
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
        while (m_moveCount > 0)
        {
            SetNextPos();
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
        yield return null;
    }

    private IEnumerator NeedHelp()
    {
        while (m_moveCount > 0)
        {
            while (m_needHelpBool)
            {
                yield return null;
            }

            yield return new WaitForSeconds(m_needHpleTime);
            EnableHelpImage(Spr_Help);
        }
        yield return null;
    }

    private void SetNextPos()
    {
        m_target = NPCSpots.Instance.GetRandomSpot();
        m_moveComplete = false;
    }

    private void EnableHelpImage(Sprite request)
    {
        m_helpImage.sprite = request;
        m_DialogCanvas.enabled = true;
        m_needHelpBool = true;
    }

    public void DisableHelpImage()
    {
        m_DialogCanvas.enabled = false;
        m_needHelpBool = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (!m_helpedComplete)          //當需求尚未得到滿足時
        {
            if (collision.gameObject.tag == "Player" && m_needHelpBool)
            {
                if (!m_IsExecuteingHelp)
                {
                    EnableHelpImage(Spr_Loading);

                    m_moveComplete = true;
                    m_IsExecuteingHelp = true;              //判斷正在幫助 npc
                    StartCoroutine(Fn_RotationLoad());
                }
            }
        }*/
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (!m_helpedComplete)          //當需求尚未得到滿足時
        {
            if (collision.gameObject.tag == "Player" && m_needHelpBool)
            {
                if (m_IsExecuteingHelp)
                {
                    m_IsExecuteingHelp = false;              //判斷取消了幫助 npc
                }
                EnableHelpImage(Spr_Help);
            }
        }*/
    }
    /*private IEnumerator Fn_RotationLoad()           //旋轉 Load 圖示
    {
        while (m_IsExecuteingHelp)
        {
            m_helpImage.transform.Rotate(Vector3.forward * 75f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        //幫助取消了
        m_helpImage.transform.eulerAngles = Vector3.zero;
    }*/
}
