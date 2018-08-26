using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_floor : MonoBehaviour
{
    private bool m_IsSlip = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !m_IsSlip)          //當碰到玩家時
        {
            m_IsSlip = true;

            PlayerController PlayerScript = collision.GetComponent<PlayerController>();
            PlayerScript.m_IsAllowMove = false;     //禁止玩家進行移動
            

            StartCoroutine(PlayerScript.Fn_SetSlipMoveing());
            StartCoroutine(Fn_SetRestoreMove(PlayerScript));
            StartCoroutine(Fn_SetCharacterRotation(PlayerScript.gameObject));

            this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    private IEnumerator Fn_SetRestoreMove(PlayerController script)         //玩家恢復移動
    {
        yield return new WaitForSeconds(1.5f);
        m_IsSlip = false;
        script.m_IsAllowMove = true;
        script.gameObject.transform.eulerAngles = Vector3.zero;
    }
    private IEnumerator Fn_SetCharacterRotation(GameObject character)
    {
        while (m_IsSlip)
        {
            character.transform.Rotate(Vector3.up * 10f);
            yield return new WaitForEndOfFrame();
        }
    }
}
