using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCType
{
    public float speed;
    public float scale;
}

public class NPCClone : MonoBehaviour {

    public GameObject m_NPCobj;
    public float m_cloneTime;
    public NPCType[] m_type;

    private IEnumerator AddNewNPC()
    {
        while (GameSystem.Instance.m_IsGameExecute)
        {
            yield return new WaitForSeconds(m_cloneTime);

            if (GameSystem.Instance.Fn_CanAddNewNPC())
            {
                int RandomType = Random.Range(0, m_type.Length);
                GameObject npc = Instantiate(m_NPCobj, NPCSpots.Instance.GetRandomExitSpot(), Quaternion.identity);
                npc.transform.GetChild(0).localScale = Vector3.one * m_type[RandomType].scale;
                npc.GetComponent<NPCBehaviour>().m_moveSpeed = m_type[RandomType].speed;
            }  
        }
    }

 

	void Start () {

        if (m_NPCobj == null) return;
        StartCoroutine(AddNewNPC());

	}

}
