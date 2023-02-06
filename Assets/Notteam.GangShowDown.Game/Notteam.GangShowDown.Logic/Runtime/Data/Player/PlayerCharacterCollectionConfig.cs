using System;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [Serializable]
    public struct PlayerCharacterConfigData
    {
        public PlayerData playerData;
        public Player prefab;
    }
    
    [CreateAssetMenu(fileName = "PlayerCharacterCollectionConfig", menuName = "Notteam/Notteam.GangShowDown/Create Player Character Collection Config", order = 0)]
    public class PlayerCharacterCollectionConfig : ScriptableObject
    {
        [SerializeField] private PlayerDataConfig[] characters;

        public PlayerDataConfig[] Characters => characters;
    }
}
