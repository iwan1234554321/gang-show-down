using System;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] private float radius = 1.0f;

        [Header("Visual Settings")]
        [SerializeField] private Color color = new(0, 1, 0, 0.5f);
        
        public event Action<TriggerPoint> OnTriggerEnterEvent;
        public event Action<TriggerPoint> OnTriggerExitEvent;
        public event System.Action OnDestroyEvent;

        private TriggerPoint _touchedTriggerPoint;
        
        private TriggerSystem _triggerSystem;

        private void Awake()
        {
            _triggerSystem = FindObjectOfType<TriggerSystem>();
        }

        private void Update()
        {
            foreach (var point in _triggerSystem.Points)
            {
                var distance = (point.Position - transform.position).sqrMagnitude;
                
                if (_touchedTriggerPoint == null)
                {
                    if ((distance * distance) < radius)
                    {
                        OnTriggerEnterEvent?.Invoke(point);
                        
                        _touchedTriggerPoint = point;
                        
                        _touchedTriggerPoint.OnTriggerPointEnter(this);
                    }
                }
                else
                {
                    if (_touchedTriggerPoint == point & (distance * distance) > radius)
                    {
                        OnTriggerExitEvent?.Invoke(_touchedTriggerPoint);
                        
                        _touchedTriggerPoint.OnTriggerPointExit(this);
                        
                        _touchedTriggerPoint = null;
                    }
                }
            }
        }

        private void OnDestroy()
        {
            OnDestroyEvent?.Invoke();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, radius / 2);
            Gizmos.DrawWireSphere(transform.position, radius / 2);
        }
    }
}
