using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    [System.Serializable]
    public class PlayerData
    {
        public float m_fMoveSpeed;          //玩家移動速度
        public float m_fMaxXRange;          //X軸最大移動範圍
        public float m_fMinXRange;          //X軸最小移動範圍
        public float m_fMaxYRange;          //Y軸最大移動範圍
        public float m_fMinYRange;          //Y軸最小移動範圍
    }

    [System.Serializable]
    public class LevelData
    {
        public float m_fLastTime;           //遊戲剩餘時間
    }
}
