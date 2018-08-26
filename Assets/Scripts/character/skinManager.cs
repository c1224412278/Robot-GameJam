using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class skinManager : MonoBehaviour
{
    private PlayerController PlayerScript;
    [SerializeField] private Button[] Btn_UpdateKind;
    private void Start()
    {
        PlayerScript = this.GetComponent<PlayerController>();           //抓取玩家角色控制腳本

        if (PlayerScript.theRobotKind == PlayerController.Enum_RobotKind.DrinkRobot)
        {
            this.GetComponent<SpriteRenderer>().sprite = PlayerScript.Fn_ReturnPlayerControllerData().Spr_Robots[2];
            Btn_UpdateKind[0].interactable = false;
        }
        else if (PlayerScript.theRobotKind == PlayerController.Enum_RobotKind.GarbageRobot)
        {
            this.GetComponent<SpriteRenderer>().sprite = PlayerScript.Fn_ReturnPlayerControllerData().Spr_Robots[1];
            Btn_UpdateKind[1].interactable = false;
        }
        else if (PlayerScript.theRobotKind == PlayerController.Enum_RobotKind.SweepRobot)
        {
            this.GetComponent<SpriteRenderer>().sprite = PlayerScript.Fn_ReturnPlayerControllerData().Spr_Robots[0];
            Btn_UpdateKind[0].interactable = false;
        }
    }
    public void Fn_UpdateToDrinkRobot()
    {
        if (PlayerScript.theRobotKind != PlayerController.Enum_RobotKind.DrinkRobot)
        {
            PlayerScript.gameObject.GetComponent<SpriteRenderer>().sprite = PlayerScript.Fn_ReturnPlayerControllerData().Spr_Robots[2];
            PlayerScript.theRobotKind = PlayerController.Enum_RobotKind.DrinkRobot;
            //變為販賣機器人
        }
    }
    public void Fn_UpdateToGarbageRobot()
    {
        if (PlayerScript.theRobotKind != PlayerController.Enum_RobotKind.GarbageRobot)
        {
            PlayerScript.gameObject.GetComponent<SpriteRenderer>().sprite = PlayerScript.Fn_ReturnPlayerControllerData().Spr_Robots[1];
            PlayerScript.theRobotKind = PlayerController.Enum_RobotKind.GarbageRobot;
            //變為垃圾機器人
        }
    }
    public void Fn_UpdateToSweepRobot()
    {
        if (PlayerScript.theRobotKind != PlayerController.Enum_RobotKind.SweepRobot)
        {
            PlayerScript.gameObject.GetComponent<SpriteRenderer>().sprite = PlayerScript.Fn_ReturnPlayerControllerData().Spr_Robots[0];
            PlayerScript.theRobotKind = PlayerController.Enum_RobotKind.SweepRobot;
            //變為掃地機器人
        }
    }
}
