using System.Collections.Generic;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [RequireComponent(typeof(Player))]
    public class ActionCollector : MonoBehaviour
    {
        private List<ActionData> _actionsOnObject = new();

        public event System.Action<List<ActionData>> OnGetActions;
        public event System.Action OnApplyActions;
        
        private Player _player;
        private MoveManager _moveManager;
        public Player Player => _player;

        private void FinishMove()
        {
            OnGetActions?.Invoke(_actionsOnObject);
            
            var changedPlayerData = Utils.ApplyActionDataToPlayerData(_player.Data, ref _actionsOnObject);
            
            _player.SetData(changedPlayerData);
            
            OnApplyActions?.Invoke();
        }
        
        private void Awake()
        {
            _player = GetComponent<Player>();
            _moveManager = FindObjectOfType<MoveManager>();
            
            _moveManager.OnFinishMove += FinishMove;
        }

        public void AddAction(ActionData data)
        {
            // if (!HasExistAction(data)) // I commented it out because there was no talk in the test task about the limitations of adding actions, but the functionality can be restored
                _actionsOnObject.Add(data);
        }
    }
}
