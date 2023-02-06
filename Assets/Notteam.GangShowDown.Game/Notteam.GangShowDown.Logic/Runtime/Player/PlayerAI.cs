using System.Collections.Generic;
using Notteam.AnimationTween;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Notteam.GangShowDown.Logic
{
    [DefaultExecutionOrder(5)]
    public class PlayerAI : MonoBehaviour
    {
        [SerializeField] private float delayMin = 0.3f;
        [SerializeField] private float delayMax = 0.7f;
        [SerializeField] private float moveTime = 0.5f;
        [SerializeField] private Vector3 positionAction;
        [SerializeField] private bool endDrag;

        private Action _playerAction;
        
        private MoveManager _moveManager;
        private TeamManager _teamManager;

        private void AnimateMoveAction(Vector3 destinationPoint)
        {
            var startPosition = positionAction;
            
            AnimationTweenInstance.CreateTween(gameObject, "Animate Move Action", moveTime, updateAnimation: f =>
                {
                    positionAction = Vector3.Lerp(startPosition, new Vector3(destinationPoint.x, startPosition.y, destinationPoint.z), f);
                },
                finalAnimation: () =>
                {
                    _playerAction.DragObject.EndDrag(positionAction);
                });
        }
        
        private void SelectTarget(Player currentPlayer)
        {
            if (currentPlayer && _playerAction)
            {
                var compatibilityPlayers = new List<Player>();
            
                foreach (var crew in _teamManager.Teams)
                {
                    foreach (var player in crew.players)
                    {
                        var playerActionCollector = player.GetComponent<ActionCollector>();
            
                        if (playerActionCollector)
                        {
                            if (Utils.CheckCompatibilityAction(_playerAction.Data.useType, currentPlayer, player))
                                compatibilityPlayers.Add(player);
                        }
                    }
                }
            
                if (compatibilityPlayers.Count > 0)
                {
                    var randomPlayerSelection = Utils.GetArrayRandomNumber(compatibilityPlayers.Count, 100);

                    var selectedTarget = compatibilityPlayers[randomPlayerSelection];
                    
                    
                
                    _playerAction.DragObject.BeginDrag(positionAction);
                    
                    AnimateMoveAction(selectedTarget.transform.position);
                }
            }
        }
        
        private void DelayAI(Player currentPlayer)
        {
            var randomTime = Random.Range(delayMin, delayMax);
            
            AnimationTweenInstance.CreateTween(gameObject, "Delay AI", randomTime, finalAnimation: () =>
            {
                SelectTarget(currentPlayer);
            });
        }
        
        private void PreparedPlayer(Player player)
        {
            if (!player.Data.isMain)
            {
                _playerAction = player.PlayerAction;

                positionAction = _playerAction.transform.position;
                
                DelayAI(player);
            }
        }
        
        private void Awake()
        {
            _moveManager = FindObjectOfType<MoveManager>();
            _teamManager = FindObjectOfType<TeamManager>();

            _moveManager.OnPreparedPlayer += PreparedPlayer;
        }

        private void Update()
        {
            if (_playerAction)
            {
                _playerAction.DragObject.ProcessDrag(positionAction);
            }

            if (endDrag)
            {
                _playerAction.DragObject.EndDrag(positionAction);
                
                endDrag = false;
            }
        }

        // [SerializeField] private float timeDelayMin = 0.1f;
        // [SerializeField] private float timeDelayMax = 0.7f;
        // [SerializeField] private float timeActionMove = 2.5f;
        // [SerializeField] private AnimationSeparatedCurve curveActionMove;
        //
        // private bool _collectPlayers;
        //
        // private ActionCollector _currentActionCollector;
        //
        // private TeamManager _teamManager;
        // private PlayersMoveManager _playersMoveManager;
        // private ActionManager _actionManager;
        //
        // private void CreateDelay()
        // {
        //     var randomTime = Random.Range(timeDelayMin, timeDelayMax);
        //     
        //     AnimationTweenInstance.CreateTween(gameObject, "AI Delay Before Action", randomTime, finalAnimation: SelectPlayerAndAction);
        // }
        //
        // private void AnimateMoveAction(Vector3 destinationPoint)
        // {
        //     // var startActionPosition = _actionManager.CurrentAction.transform.position;
        //     //
        //     // AnimationTweenInstance.CreateTween(gameObject, "Animate Move Action", timeActionMove, updateAnimation: f =>
        //     //     {
        //     //         if (_actionManager.CurrentAction)
        //     //             _actionManager.CurrentAction.transform.position = Vector3.Lerp(startActionPosition, new Vector3(destinationPoint.x, startActionPosition.y, destinationPoint.z), f);
        //     //     },
        //     //     finalAnimation: () =>
        //     //     {
        //     //         _actionManager.CurrentAction.ForceAddAction(_currentActionCollector);
        //     //         
        //     //     }).SetCurve(curveActionMove.Curve);
        // }
        //
        // private void SelectPlayerAndAction()
        // {
        //     // if (_playersMoveManager.CurrentPlayer && _playersMoveManager.CurrentPlayer.Data.isOwner == false && _actionManager.CurrentAction)
        //     // {
        //     //     var compatibilityPlayers = new List<Player>();
        //     //
        //     //     foreach (var crew in _teamManager.Teams)
        //     //     {
        //     //         foreach (var player in crew.players)
        //     //         {
        //     //             var playerActionCollector = player.GetComponent<ActionCollector>();
        //     //
        //     //             if (playerActionCollector)
        //     //             {
        //     //                 if (_actionManager.CurrentAction.CheckActionCompatibility(_playersMoveManager.CurrentPlayer, player)/* && !playerActionCollector.HasExistAction(_actionManager.CurrentAction.Data)*/)
        //     //                     compatibilityPlayers.Add(player);
        //     //             }
        //     //         }
        //     //     }
        //     //
        //     //     if (compatibilityPlayers.Count > 0)
        //     //     {
        //     //         var randomPlayerSelection = Utils.GetArrayRandomNumber(compatibilityPlayers.Count, 100);
        //     //
        //     //         _currentActionCollector = compatibilityPlayers[randomPlayerSelection].GetComponent<ActionCollector>();
        //     //         
        //     //         AnimateMoveAction(compatibilityPlayers[randomPlayerSelection].transform.position);
        //     //     }
        //     // }
        // }
        //
        // private void UpdateMove()
        // {
        //     CreateDelay();
        // }
        //
        // private void Awake()
        // {
        //     _teamManager = FindObjectOfType<TeamManager>();
        //     _playersMoveManager = FindObjectOfType<PlayersMoveManager>();
        //     _actionManager = FindObjectOfType<ActionManager>();
        //
        //     _playersMoveManager.OnUpdateMove += UpdateMove;
        // }
    }
}
