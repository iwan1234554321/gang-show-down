using System;
using Notteam.AnimationTween;
using UnityEngine;
using UnityEngine.UI;

namespace Notteam.GangShowDown.Logic
{
    [Serializable]
    public class UIGraphicColorChangerData
    {
        public float time = 0.5f;
        public AnimationSeparatedCurve curve;
        [Space]
        public MaskableGraphic graphic;
        public Color[] colors;

        private int _currentIndex;
        
        private Color _startColor;
        
        private void StartAnimation()
        {
            _startColor = graphic.color;
        }
        
        private void UpdateAnimation(float value)
        {
            graphic.color = Color.Lerp(_startColor, colors[_currentIndex], value);
        }
        
        public void UpdateColor(int index)
        {
            _currentIndex = Mathf.Clamp(index, 0, colors.Length - 1);
            
            if (graphic)
            {
                var tween = AnimationTweenInstance.CreateTween(graphic.gameObject, "UI Color Change", time, StartAnimation, UpdateAnimation);

                if (curve)
                    tween.SetCurve(curve.Curve);
            }
        }
    }
    
    public class UIGraphicColorChanger : MonoBehaviour
    {
        [SerializeField] private int currentColorIndex;
        [SerializeField] private UIGraphicColorChangerData[] colorChangerData = Array.Empty<UIGraphicColorChangerData>();

        private int _changedCurrentColorIndex;

        private void Awake()
        {
            foreach (var data in colorChangerData)
                data.UpdateColor(currentColorIndex);
        }

        private void Update()
        {
            if (_changedCurrentColorIndex != currentColorIndex)
            {
                foreach (var data in colorChangerData)
                    data.UpdateColor(currentColorIndex);
                
                _changedCurrentColorIndex = currentColorIndex;
            }
        }

        public void SetColorIndex(int value)
        {
            currentColorIndex = value;
        }
    }
}
