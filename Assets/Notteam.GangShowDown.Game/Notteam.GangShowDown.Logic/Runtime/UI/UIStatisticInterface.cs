using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [DefaultExecutionOrder(2)]
    public class UIStatisticInterface : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private UICanvasGroup uiCanvasGroup;
        
        private TeamManager _teamManager;
        private GameManager _gameManager;

        private void StoppedGame()
        {
            var information = string.Empty;

            var listLostTeams = new List<SpawnType>();
            
            foreach (var team in _teamManager.Teams)
            {
                var currentTeam = false;
                
                foreach (var player in team.players)
                {
                    if (player.Data.health == 0)
                    {
                        currentTeam = true;
                        
                        break;
                    }
                }

                if (currentTeam)
                {
                    listLostTeams.Add(team.spawnType);
                }
            }

            if (listLostTeams.Count == 1)
            {
                if (listLostTeams[0] == SpawnType.Player)
                    information = "Player's team lost";
                else if (listLostTeams[0] == SpawnType.Enemy)
                    information = "Player's team won";
            }
            else if (listLostTeams.Count > 1)
            {
                information = "Nobody won";
            }
            
            text.text = information;
            
            uiCanvasGroup.SetVisible(true);
        }
        
        private void Awake()
        {
            _teamManager = FindObjectOfType<TeamManager>();
            _gameManager = FindObjectOfType<GameManager>();

            _gameManager.OnGameStopped += StoppedGame;
        }
    }
}
