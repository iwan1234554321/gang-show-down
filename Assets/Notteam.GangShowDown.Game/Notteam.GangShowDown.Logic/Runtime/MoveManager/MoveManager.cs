using System;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [DefaultExecutionOrder(3)]
    public class MoveManager : MonoBehaviour
    {
        public event System.Action<Player> OnPreparedPlayer;
        public event System.Action OnFinishMove;

        private int _currentTeamIndex;
        private int _currentPlayerIndex;
        
        private Player _currentPlayer;
        
        private TeamManager _teamManager;
        private GameManager _gameManager;

        private Player GetPlayerFromTeamByIndex(int teamIndex, int playerIndex)
        {
            var errorTeam = teamIndex > _teamManager.Teams.Count - 1 || teamIndex < 0;

            if (errorTeam)
            {
                Debug.LogWarning($"This team index {teamIndex} is not exist");

                teamIndex = 0;
            }
            
            var errorPlayer = playerIndex > _teamManager.Teams[teamIndex].players.Count - 1 || playerIndex < 0;
            
            if (errorPlayer)
            {
                Debug.LogWarning($"This player index {playerIndex} is not exist");

                playerIndex = 0;
            }
            
            return _teamManager.Teams[teamIndex].players[playerIndex];
        }

        private void FinishMovePlayer()
        {
            OnFinishMove?.Invoke();
            
            _gameManager.CheckGame(ChangePlayer, null);
        }
        
        private void SubscribeToPlayer()
        {
            _currentPlayer.OnPlayerFinishMove += FinishMovePlayer;
        }
        
        private void UnSubscribeToPlayer()
        {
            _currentPlayer.OnPlayerFinishMove -= FinishMovePlayer;
        }
        
        private void PrepareCurrentPlayer(bool onStart)
        {
            _currentPlayer = GetPlayerFromTeamByIndex(_currentTeamIndex, _currentPlayerIndex);

            if (onStart)
            {
                var currentPlayerData = _currentPlayer.Data;

                currentPlayerData.isMain = true;
                
                _currentPlayer.SetData(new PlayerData(currentPlayerData));
            }
            
            SubscribeToPlayer();
            
            OnPreparedPlayer?.Invoke(_currentPlayer);
        }

        // Executed when the previous player has made a move
        private void ChangePlayer()
        {
            UnSubscribeToPlayer();
            
            var currentTeam = _teamManager.Teams[_currentTeamIndex];
            
            if (_currentPlayerIndex < currentTeam.players.Count - 1)
                _currentPlayerIndex++;
            else
            {
                _currentPlayerIndex = 0;

                if (_currentTeamIndex < _teamManager.Teams.Count - 1)
                    _currentTeamIndex++;
                else
                    _currentTeamIndex = 0;
            }
            
            PrepareCurrentPlayer(false);
        }
        
        private void Awake()
        {
            _teamManager = FindObjectOfType<TeamManager>();
            _gameManager = FindObjectOfType<GameManager>();

            PrepareCurrentPlayer(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SkipMove();
        }

        public void SkipMove()
        {
            if (_currentPlayer && _currentPlayer.Data.isMain)
                _currentPlayer.SkipMove();
        }
    }
}
