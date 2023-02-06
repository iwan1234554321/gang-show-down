using System;
using UnityEngine;

namespace Notteam.AnimationTween
{
    public abstract class AnimationTweenSceneObject : MonoBehaviour
    {
        [SerializeField] private float time = 0.5f;
        [SerializeField] private AnimationSeparatedCurve animationCurve;

        public event Action OnStartAnimation;
        public event Action<float> OnUpdateAnimation;
        public event Action OnFinalAnimation;

        protected virtual void StartAnimation() {}
        protected abstract void UpdateAnimation(float value);
        protected virtual void FinalAnimation() {}
        
        private void StartAnimationInternal()
        {
            StartAnimation();
            
            OnStartAnimation?.Invoke();
        }
        
        private void UpdateAnimationInternal(float value)
        {
            UpdateAnimation(value);
            
            OnUpdateAnimation?.Invoke(value);
        }
        
        private void FinalAnimationInternal()
        {
            FinalAnimation();
            
            OnFinalAnimation?.Invoke();
        }
        
        protected void Play(string id)
        {
            var tween = AnimationTweenInstance.CreateTween(gameObject, id, time, StartAnimationInternal, UpdateAnimationInternal, FinalAnimationInternal);

            if (animationCurve)
                tween.SetCurve(animationCurve.Curve);
        }
    }
}
