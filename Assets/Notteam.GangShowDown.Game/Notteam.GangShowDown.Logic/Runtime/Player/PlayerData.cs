using System;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [Serializable]
    public struct PlayerData
    {
        public string name;
        [Space]
        public Sprite avatar;
        public Sprite modifer;
        [Space]
        public Color modiferColor;
        [Space]
        public int health;
        public int defence;
        [Space]
        public bool isMain;
        [Space]
        public bool isPoisoned;
        public bool isDefended;
        
        public PlayerData(PlayerData data)
        {
            name = data.name;
            avatar = data.avatar;
            
            modifer = data.modifer;
            modiferColor = data.modiferColor;
            
            health = data.health;
            defence = data.defence;
            
            isMain = data.isMain;
            
            isPoisoned = data.isPoisoned;
            isDefended = data.isDefended;
        }
    }
}