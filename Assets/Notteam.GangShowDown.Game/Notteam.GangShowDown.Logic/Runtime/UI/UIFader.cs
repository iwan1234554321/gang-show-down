using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [RequireComponent(typeof(UICanvasGroup))]
    public class UIFader : MonoBehaviour
    {
        public event System.Action OnStartAnimation;
        public event System.Action<float> OnUpdateAnimation;
        public event System.Action OnFinalAnimation;
        
        private UICanvasGroup _uiCanvasGroup;
        
        private void Awake()
        {
            _uiCanvasGroup = GetComponent<UICanvasGroup>();

            _uiCanvasGroup.OnStartAnimation += () =>
            {
                OnStartAnimation?.Invoke();
            };
            _uiCanvasGroup.OnUpdateAnimation += (f) =>
            {
                OnUpdateAnimation?.Invoke(f);
            };
            _uiCanvasGroup.OnFinalAnimation += () =>
            {
                OnFinalAnimation?.Invoke();
            };
        }

        public void SwitchFade(bool value, float time)
        {
            _uiCanvasGroup.SetTime(time);
            _uiCanvasGroup.SetVisible(value);
        }
    }
}
