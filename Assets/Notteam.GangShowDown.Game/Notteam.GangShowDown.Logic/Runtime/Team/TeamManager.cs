using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [Serializable]
    public struct TeamData
    {
        public SpawnType spawnType;
        public List<Player> players;
    }
    
    [DefaultExecutionOrder(1)]
    public class TeamManager : MonoBehaviour
    {
        private List<TeamData> _teams = new();

        public List<TeamData> Teams => _teams;

        private void Awake()
        {
            var playerTeamData = new TeamData
            {
                spawnType = SpawnType.Player,
                players = new List<Player>()
            };
            
            var enemyTeamData = new TeamData
            {
                spawnType = SpawnType.Enemy,
                players = new List<Player>()
            };
                        
            _teams.AddRange(new[]{ playerTeamData, enemyTeamData });
            
            var players = FindObjectsOfType<Player>().ToList();
            
            players.Sort();
            
            var playerTeamIndex = _teams.FindIndex(e => e.spawnType == SpawnType.Player);
            var enemyTeamIndex = _teams.FindIndex(e => e.spawnType == SpawnType.Enemy);
            
            foreach (var player in players)
            {
                if (player.Type == SpawnType.Player)
                    _teams[playerTeamIndex].players.Add(player);
                else if (player.Type == SpawnType.Enemy)
                    _teams[enemyTeamIndex].players.Add(player);
            }
        }
    }
}
