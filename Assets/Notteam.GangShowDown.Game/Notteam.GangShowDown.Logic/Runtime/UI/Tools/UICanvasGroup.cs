using Notteam.AnimationTween;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class UICanvasGroup : MonoBehaviour
    {
        [SerializeField] private bool isVisible;
        [SerializeField] private float timeAnimate = 0.5f;
        [SerializeField] private CanvasGroup canvasGroup;

        public event System.Action OnStartAnimation;
        public event System.Action<float> OnUpdateAnimation;
        public event System.Action OnFinalAnimation;

        private bool _changedIsVisible;

        private float _startAlpha;
        
        private void StartAnimation()
        {
            _startAlpha = canvasGroup.alpha;
            
            OnStartAnimation?.Invoke();
        }
        
        private void UpdateAnimation(float value)
        {
            canvasGroup.alpha = Mathf.Lerp(_startAlpha, isVisible ? 1.0f : 0.0f, value);
            
            OnUpdateAnimation?.Invoke(value);
        }
        
        private void FinalAnimation()
        {
            canvasGroup.blocksRaycasts = isVisible;
            
            OnFinalAnimation?.Invoke();
        }
        
        private void AnimateVisible()
        {
            AnimationTweenInstance.CreateTween(gameObject, "Animate Visible", timeAnimate, StartAnimation, UpdateAnimation, FinalAnimation);
        }

        private void Awake()
        {
            AnimateVisible();
        }

        private void Update()
        {
            if (_changedIsVisible != isVisible)
            {
                AnimateVisible();
                
                _changedIsVisible = isVisible;
            }
        }

        public void SetVisible(bool value) => isVisible = value;

        public void SetTime(float value) => timeAnimate = value;
    }
}
