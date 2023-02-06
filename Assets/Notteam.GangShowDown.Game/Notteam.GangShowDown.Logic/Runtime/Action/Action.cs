using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class Action : SpawnObject
    {
        public event System.Action<ActionData, ActionTimeline, bool> OnExecuted;

        private ActionData _data;
        
        private Player _actionPlayer;

        private ActionCollector _actionCollector;

        private Trigger _trigger;
        private PointerDragObject _dragObject;
        private AnimationTransformScale _animationTransformScale;
        
        public ActionData Data { get => _data; set => _data = value; }

        public PointerDragObject DragObject => _dragObject;

        private void TriggerEntered(TriggerPoint collider)
        {
            var existActionCollector = collider.GetComponent<ActionCollector>();
            
            if (existActionCollector)
                _actionCollector = existActionCollector;
        }
        
        private void TriggerExit(TriggerPoint collider)
        {
            var existActionCollector = collider.GetComponent<ActionCollector>();
            
            if (existActionCollector)
                _actionCollector = null;
        }

        private ActionTimeline InstantiateActionTimeline(bool skipMove = false)
        {
            return skipMove switch
            {
                true when _data.skipActionTimeline => Instantiate(_data.skipActionTimeline, Vector3.zero, Quaternion.identity),
                false when _data.actionTimeline => Instantiate(_data.actionTimeline, Vector3.zero, Quaternion.identity),
                _ => null
            };
        }

        private void ExecuteAction(bool skipMove = false)
        {
            _animationTransformScale.SetScale(Vector3.zero);

            _animationTransformScale.OnFinalAnimation += () =>
            {
                OnExecuted?.Invoke(_data, InstantiateActionTimeline(skipMove), skipMove);
                    
                Reset();
            
                Destroy(gameObject);
            };
        }
        
        private void AddActionToCollector()
        {
            if (_actionCollector && _actionPlayer)
            {
                if (Utils.CheckCompatibilityAction(_data.useType, _actionPlayer, _actionCollector.Player))
                {
                    _actionCollector.AddAction(_data);
                    
                    ExecuteAction();
                }
            }
        }
        
        private void EndDrag()
        {
            AddActionToCollector();
        }

        private void SkipPlayerMove()
        {
            ExecuteAction(true);
        }

        private void Reset()
        {
            OnExecuted = null;
            
            _actionPlayer.OnSkipMove -= SkipPlayerMove;
        }

        private void Awake()
        {
            _trigger = GetComponentInChildren<Trigger>();
            _dragObject = GetComponentInChildren<PointerDragObject>();
            _animationTransformScale = GetComponentInChildren<AnimationTransformScale>();

            _animationTransformScale.SetScale(Vector3.one);
            
            _trigger.OnTriggerEnterEvent += TriggerEntered;
            _trigger.OnTriggerExitEvent += TriggerExit;

            _dragObject.OnEndDrag += (_) => { EndDrag(); };
        }

        public void SetPlayer(Player player)
        {
            _actionPlayer = player;

            _actionPlayer.OnSkipMove += SkipPlayerMove;
        }
    }
}
