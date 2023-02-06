using System;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [Serializable]
    public struct PlayerDataConfigData
    {
        public PlayerData playerData;
        public Player prefab;
    }
    
    [CreateAssetMenu(fileName = "PlayerDataConfig", menuName = "Notteam/Notteam.GangShowDown/Create Player Data Config", order = 0)]
    public class PlayerDataConfig : ScriptableObject
    {
        [SerializeField] private PlayerDataConfigData data;

        public PlayerDataConfigData Data => data;
    }
}