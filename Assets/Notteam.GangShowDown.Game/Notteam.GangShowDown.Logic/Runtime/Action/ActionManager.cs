using System;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [DefaultExecutionOrder(2)]
    public class ActionManager : MonoBehaviour
    {
        [SerializeField] private Vector3 positionActionOffset;

        private Action _currentAction;

        private ActionTimeline _currentActionTimeline;
        
        private MoveManager _moveManager;
        private SpawnManager _spawnManager;

        private void PreparedPlayer(Player player)
        {
            var spawnTransformData = new SpawnTransformData
            {
                position = player.transform.position + positionActionOffset,
                rotation = Quaternion.identity,
            };

            var actionsSpawned = Array.Empty<SpawnObject>();
            
            switch (player.Type)
            {
                case SpawnType.Player:
                    
                    actionsSpawned = _spawnManager.SpawnByRequest(SpawnRequest.Player, new[] { spawnTransformData }, SpawnType.Action, true);
                    
                    break;
                case SpawnType.Enemy:
                    
                    actionsSpawned = _spawnManager.SpawnByRequest(SpawnRequest.EnemyTeam, new[] { spawnTransformData }, SpawnType.Action, true);
                    
                    break;
            }

            _currentAction = actionsSpawned[0].GetComponent<Action>();
            
            player.SetAction(_currentAction);
        }
        
        private void Awake()
        {
            _moveManager = FindObjectOfType<MoveManager>();
            _spawnManager = FindObjectOfType<SpawnManager>();

            _moveManager.OnPreparedPlayer += PreparedPlayer;
        }
    }
}
