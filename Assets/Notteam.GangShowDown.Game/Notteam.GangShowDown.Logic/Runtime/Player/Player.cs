using System;
using System.Collections.Generic;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [RequireComponent(typeof(PlayerAnimation))]
    public class Player : SpawnObject, IComparable<Player>
    {
        [SerializeField] private bool skipMove;
        
        public event System.Action OnPlayerFinishMove;
        public event System.Action OnSkipMove;

        private PlayerData _data;

        private Action _playerAction;
        
        private PlayerAnimation _playerAnimation;
        private ActionCollector _actionCollector;

        private AnimationTransformRotation _animationTransformRotation;
        
        public PlayerData Data { get => _data; set => _data = value; }
        public Action PlayerAction => _playerAction;

        private void PlayAnimationByAction(List<ActionData> actions)
        {
            foreach (var action in actions)
            {
                switch (action.type)
                {
                    case ActionType.Attack:
                        
                        _playerAnimation.Animator.PlayAnimation("Hit");
                        
                        _animationTransformRotation.SetUpdate(true);
                        
                        break;
                    case ActionType.Poison:
                        
                        _playerAnimation.Animator.PlayAnimation("Poison");
                        
                        break;
                }
            }
        }

        private void PlayAnimationByAppliedActions()
        {
            if (_data.health == 0)
            {
                _playerAnimation.Animator.PlayAnimation("Dead");
            }
        }

        private void PlayAnimationByExecuteAction(ActionData data, bool skip)
        {
            if (!skip)
            {
                switch (data.type)
                {
                    case ActionType.Attack:
                    
                        _playerAnimation.Animator.PlayAnimation("Throw");
                    
                        break;
                
                    case ActionType.Poison:
                    
                        _playerAnimation.Animator.PlayAnimation("Throw");
                    
                        break;
                }
            }
        }
        
        private void ExecuteAction(ActionData data, ActionTimeline timeline, bool skip)
        {
            PlayAnimationByExecuteAction(data, skip);
            
            if (timeline)
                timeline.OnFinishTimeline += FinishMove;
            else
                FinishMove();
        }
        
        private void FinishMove()
        {
            OnPlayerFinishMove?.Invoke();
        }
        
        private void Awake()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
            _actionCollector = GetComponent<ActionCollector>();

            _animationTransformRotation = FindObjectOfType<AnimationTransformRotation>();

            _actionCollector.OnGetActions += PlayAnimationByAction;
            _actionCollector.OnApplyActions += PlayAnimationByAppliedActions;
        }

        private void Update()
        {
            if (skipMove)
            {
                SkipMove();
                
                skipMove = false;
            }
        }

        public void SkipMove()
        {
            OnSkipMove?.Invoke();
        }
        
        public void SetData(PlayerData data) => _data = data;
        public void SetAction(Action action)
        {
            _playerAction = action;
            
            _playerAction.SetPlayer(this);

            _playerAction.OnExecuted += ExecuteAction;
        } 
        
        public int CompareTo(Player other)
        {
            return transform.GetSiblingIndex().CompareTo(other.transform.GetSiblingIndex());
        }
    }
}
