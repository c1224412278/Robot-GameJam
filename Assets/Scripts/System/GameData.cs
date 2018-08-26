using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    [System.Serializable]
    public class PlayerData
    {
        public float m_fMoveSpeed;          //玩家移動速度
        [HideInInspector] public float m_fSchedule;           //玩家進度值
        public float m_fMaxSchedule;        //玩家最大進度值
        public float m_fMaxXRange;          //X軸最大移動範圍
        public float m_fMinXRange;          //X軸最小移動範圍
        public float m_fMaxYRange;          //Y軸最大移動範圍
        public float m_fMinYRange;          //Y軸最小移動範圍
        public Sprite[] Spr_Robots;
    }

    [System.Serializable]
    public class LevelData
    {
        [HideInInspector] public float m_fFriendValue;           //當前剩餘好友度
        public float m_fMaxFriendValue;     //最大好友度
    }

    [System.Serializable]
    public class NpcData
    {
        public Sprite[] Spr_HelpKinds;
        public Sprite Fn_GetHelpLogo(int number)      //取得當前需要幫助的圖示
        {
            return Spr_HelpKinds[number];
        }
    }
}
