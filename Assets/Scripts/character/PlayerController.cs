﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum Enum_RobotKind
    {
        GarbageRobot ,      //垃圾機器人
        SweepRobot ,        //掃地機器人
        DrinkRobot          //販賣機器人
    }
    [SerializeField] private UIExecute UIExecuteScript;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Enum_RobotKind theRobotKind;               //當前機器人的種類
    [SerializeField] private GameData.PlayerData thePlayerData;         //玩家資料
    [SerializeField] private GameData.LevelData theLevelData;           //當前關卡資料
    private void Start()
    {
        if (thePlayerData == null || m_Rigidbody2D == null || UIExecuteScript == null)
        {
            Debug.Log("find error.");
            return;
        }

        thePlayerData.m_fSchedule = thePlayerData.m_fMaxSchedule;
        theLevelData.m_fLastTime = theLevelData.m_fMaxExecuteTime;

        theRobotKind = Enum_RobotKind.DrinkRobot;
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

            this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x , thePlayerData.m_fMinXRange , thePlayerData.m_fMaxXRange) 
                , Mathf.Clamp(this.transform.position.y , thePlayerData.m_fMinYRange, thePlayerData.m_fMaxYRange) , this.transform.position.z);
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
