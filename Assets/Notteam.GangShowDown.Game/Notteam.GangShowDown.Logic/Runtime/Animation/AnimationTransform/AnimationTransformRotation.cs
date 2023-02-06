using Notteam.AnimationTween;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class AnimationTransformRotation : MonoBehaviour
    {
        [SerializeField] private float time = 0.5f;
        [SerializeField] private bool updateRotation;
        [SerializeField] private AnimationSeparatedCurve rotateX;
        [SerializeField] private AnimationSeparatedCurve rotateY;
        [SerializeField] private AnimationSeparatedCurve rotateZ;
        [SerializeField] private Transform transformObject;

        public event System.Action OnStartAnimation;
        public event System.Action<float> OnUpdateAnimation;
        public event System.Action OnFinalAnimation;
        
        private void StartAnimation()
        {
            OnStartAnimation?.Invoke();
        }
        
        private void UpdateAnimation(float value)
        {
            if (rotateX && rotateY && rotateZ)
            {
                transformObject.localRotation = Quaternion.Euler(rotateX.Curve.Evaluate(value), rotateY.Curve.Evaluate(value), rotateZ.Curve.Evaluate(value));
            }
            
            OnUpdateAnimation?.Invoke(value);
        }
        
        private void FinalAnimation()
        {
            OnFinalAnimation?.Invoke();
        }
        
        private void AnimateScale()
        {
            AnimationTweenInstance.CreateTween(gameObject, "Animation Scale", time, StartAnimation, UpdateAnimation, FinalAnimation);
        }
        
        private void Update()
        {
            if (updateRotation)
            {
                AnimateScale();
                
                updateRotation = false;
            }
        }

        public void SetUpdate(bool updateValue) => updateRotation = updateValue;
    }
}
