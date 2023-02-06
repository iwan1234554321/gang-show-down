using Notteam.AnimationTween;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class AnimationTransformScale : MonoBehaviour
    {
        [SerializeField] private float time = 0.5f;
        [SerializeField] private Vector3 scale;
        [SerializeField] private AnimationSeparatedCurve curve;
        [SerializeField] private Transform transformObject;

        public event System.Action OnStartAnimation;
        public event System.Action<float> OnUpdateAnimation;
        public event System.Action OnFinalAnimation;

        private Vector3 _startScale;
        
        private Vector3 _changedScale;

        private void StartAnimation()
        {
            _startScale = transformObject.localScale;
            
            OnStartAnimation?.Invoke();
        }
        
        private void UpdateAnimation(float value)
        {
            transformObject.localScale = Vector3.Lerp(_startScale, scale, value);
            
            OnUpdateAnimation?.Invoke(value);
        }
        
        private void FinalAnimation()
        {
            OnFinalAnimation?.Invoke();
        }
        
        private void AnimateScale()
        {
            var tween = AnimationTweenInstance.CreateTween(gameObject, "Animation Scale", time, StartAnimation, UpdateAnimation, FinalAnimation);

            if (curve)
                tween.SetCurve(curve.Curve);
        }
        
        private void Update()
        {
            if (_changedScale != scale)
            {
                AnimateScale();
                
                _changedScale = scale;
            }
        }

        public void SetScale(Vector3 scaleValue) => scale = scaleValue;
    }
}
