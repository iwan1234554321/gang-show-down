using UnityEngine;
using UnityEngine.Events;

namespace Notteam.GangShowDown.Logic
{
    public class UIButton : MonoBehaviour
    {
        [SerializeField] private UnityEvent onEnter;
        [SerializeField] private UnityEvent onExit;
        [SerializeField] private UnityEvent onDown;
        [SerializeField] private UnityEvent onUp;
        
        public void Enter()
        {
            onEnter?.Invoke();
        }
        
        public void Exit()
        {
            onExit?.Invoke();
        }
        
        public void Down()
        {
            onDown?.Invoke();
        }
        
        public void Up()
        {
            onUp?.Invoke();
        }
    }
}
