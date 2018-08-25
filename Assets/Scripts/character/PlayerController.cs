using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private GameData.PlayerData thePlayerData;
    private void Start()
    {
        
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
        }
        else
        {
            m_Rigidbody2D.velocity = Vector3.zero;
        }
    }
}
