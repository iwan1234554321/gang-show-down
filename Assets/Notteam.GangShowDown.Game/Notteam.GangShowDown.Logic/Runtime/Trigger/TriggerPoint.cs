using UnityEngine;
using UnityEngine.Events;

namespace Notteam.GangShowDown.Logic
{
    public class TriggerPoint : MonoBehaviour
    {
        [SerializeField] private Vector3 pointOffset;
        [Space]
        [SerializeField] private UnityEvent onTriggerEnter;
        [SerializeField] private UnityEvent onTriggerExit;

        private Trigger _trigger;
        private TriggerSystem _triggerSystem;

        public Vector3 Position => transform.position + (transform.rotation * pointOffset);

        private void TriggerIsDestroyed()
        {
            onTriggerExit?.Invoke();
                
            _trigger.OnDestroyEvent -= TriggerIsDestroyed;
        }
        
        private void Awake()
        {
            _triggerSystem = FindObjectOfType<TriggerSystem>();
        }

        private void OnEnable()
        {
            if (_triggerSystem)
                _triggerSystem.AddTriggerPoint(this);
        }
        
        private void OnDisable()
        {
            if (_triggerSystem)
                _triggerSystem.RemoveTriggerPoint(this);
        }
        
        private void OnDestroy()
        {
            if (_triggerSystem)
                _triggerSystem.RemoveTriggerPoint(this);
        }

        public void OnTriggerPointEnter(Trigger trigger)
        {
            if (_trigger == null)
            {
                _trigger = trigger;

                _trigger.OnDestroyEvent += TriggerIsDestroyed;
                
                onTriggerEnter?.Invoke();
            }
        }
        
        public void OnTriggerPointExit(Trigger trigger)
        {
            if (_trigger == trigger)
            {
                onTriggerExit?.Invoke();
                
                _trigger.OnDestroyEvent -= TriggerIsDestroyed;
                
                _trigger = null;
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(Position, 0.1f);
        }
    }
}
