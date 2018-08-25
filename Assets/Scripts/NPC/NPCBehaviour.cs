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

    public Image m_helpImage;
    public Canvas m_DialogCanvas;
    private Rigidbody2D m_rigidbody;

	void Start () {

        m_rigidbody = GetComponent<Rigidbody2D>();

        m_moveSpeed = Random.Range(0.5f, 1.5f);
        m_moveCount = Random.Range(3, 5);
        m_waitTimel = Random.Range(5, 10);
        m_needHpleTime = Random.Range(5f, 15f);

        StartCoroutine(Move());
        StartCoroutine(NeedHelp());
	}
	
	void Update () {
		
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
            while (m_needHelpBool) yield return null;

            yield return new WaitForSeconds(m_needHpleTime);
            EnableHelpImage(null);
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
        //m_helpImage.sprite = request;
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
        /*if (collision.gameObject.name == "Spot")
        {
            m_moveComplete = true;
        }*/
    }

}
