using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance
    {
        get { return _Instance; }
        set { _Instance = value; }
    }
    private static GameSystem _Instance;

    public bool m_IsGameExecute;
    public int m_LV = 1;                        // 關卡
    public float m_UpdateLVTime = 5;            // 升級關卡時間   

    public int m_npcMaxAmount = 15;             // 最大數量   
    private int m_npcAmount = 1;
    private int m_npcCurrentAmoumt;             // NPC當前數量

    public int m_npcMaxMoveCount = 5;           // 移動場景數量
    [SerializeField]
    private int m_npcMoveCount = 1;              

    public float m_evaluationValue = 100;       // 評價值

    private void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(this.gameObject);
        //SceneManager.LoadScene("Scenes/menu");
    }
    public float Fn_GetInverseLerp(float min , float max , float currect)
    {
        return Mathf.InverseLerp(min , max , currect);
    }

    public bool Fn_CanAddNewNPC()
    {
        if(m_npcAmount > m_npcCurrentAmoumt)
        {
            m_npcCurrentAmoumt++;
            return true;
        }
        return false;
    }

    public void Fn_NPCExit()
    {
        m_npcCurrentAmoumt -= 1;
    }

    private void Start()
    {
        StartCoroutine(UpdateGameState());
    }

    private IEnumerator UpdateGameState()
    {
        m_IsGameExecute = true;
        StartCoroutine(CheckEvaluation());

        while (m_evaluationValue > 0 && m_IsGameExecute)
        {
            yield return new WaitForSeconds(m_UpdateLVTime);

            m_LV += 1;
            if (m_npcMaxAmount > m_npcAmount + 1)
                m_npcAmount += 1;                   // 每升級關卡就加入一位NPC

            if(m_npcMaxMoveCount > 1 + (m_LV / 3))
                m_npcMoveCount = 1 + (m_LV / 3);    // 每升級三次關卡就增加NPC移動地點的次數

        }
    }

    private IEnumerator CheckEvaluation()
    {
        while (m_evaluationValue > 0) yield return null;
        m_IsGameExecute = false;
    }
}
