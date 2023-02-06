using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public enum GameState
    {
        None,
        OnePlayerDead,
        OnePlayerPoisoned,
    }
    
    [DefaultExecutionOrder(4)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameState stopGameState;

        public event System.Action OnGameStopped;
        
        private TeamManager _teamManager;

        private bool CheckPlayersIsDead()
        {
            foreach (var team in _teamManager.Teams)
            {
                foreach (var player in team.players)
                {
                    if (player.Data.health <= 0)
                        return true;
                }
            }

            return false;
        }
        
        private bool CheckPlayersIsPoisoned()
        {
            foreach (var team in _teamManager.Teams)
            {
                foreach (var player in team.players)
                {
                    if (player.Data.isPoisoned)
                        return true;
                }
            }

            return false;
        }
        
        private GameState CheckGameState()
        {
            if (CheckPlayersIsDead())
                return GameState.OnePlayerDead;
            
            if (CheckPlayersIsPoisoned())
                return GameState.OnePlayerPoisoned;
            
            return default;
        }

        private void Awake()
        {
            _teamManager = FindObjectOfType<TeamManager>();
        }

        // Checks if the game can continue
        public void CheckGame(System.Action continueGame, System.Action stopGame)
        {
            if (stopGameState == CheckGameState())
            {
                stopGame?.Invoke();
                    
                Debug.Log($"Game stopped because the return state was like this : {stopGameState}");
                
                OnGameStopped?.Invoke();
                    
                return;
            }
            
            continueGame?.Invoke();
        }
    }
}
