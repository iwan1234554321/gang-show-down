using Notteam.AnimationTween;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class ActionTimeline : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private AnimationSeparatedCurve curve;

        public event System.Action OnStartTimeline;
        public event System.Action<float> OnUpdateTimeline;
        public event System.Action OnFinishTimeline;
        
        public float Time => time;

        private void StartTimeline()
        {
            OnStartTimeline?.Invoke();
        }
        
        private void UpdateTimeline(float value)
        {
            OnUpdateTimeline?.Invoke(value);
        }
        
        private void FinishTimeline()
        {
            OnFinishTimeline?.Invoke();
            
            Destroy(gameObject);
        }
        
        private void CreateTimeline()
        {
            var timeline = AnimationTweenInstance.CreateTween(gameObject, "Action Timeline", time, StartTimeline, UpdateTimeline, FinishTimeline);

            if (curve)
                timeline.SetCurve(curve.Curve);
        }
        
        private void Awake()
        {
            CreateTimeline();
        }
    }
}
