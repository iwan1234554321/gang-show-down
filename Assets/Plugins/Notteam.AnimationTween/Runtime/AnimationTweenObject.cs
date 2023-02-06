using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Notteam.AnimationTween
{
    [Serializable]
    public sealed class AnimationTweenObject
    {
        private string _id;
        private GameObject _target;
        private Scene _sceneAnimation;

        private float _time;
        private float _timeline;

        private AnimationCurve _animationCurve;

        private event Action StartAnimation;
        private event Action<float> UpdateAnimation;
        private event Action FinalAnimation;
        
        private bool _isComplete;

        public string Id => _id;
        public GameObject Target => _target;
        public Scene SceneAnimation => _sceneAnimation;
        public bool IsComplete => _isComplete;

        public AnimationTweenObject(GameObject target, string id, float time, Action startAnimation, Action<float> updateAnimation, Action finalAnimation)
        {
            _target = target;
            
            _id = $"{id} : {_target.GetInstanceID()}";
            
            _sceneAnimation = _target.scene;

            _time = time;
            _timeline = 0;
            
            _animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
            
            StartAnimation = startAnimation;
            UpdateAnimation = updateAnimation;
            FinalAnimation = finalAnimation;
        }
        
        internal void Update(float deltaTime)
        {
            if (_target)
            {
                if (!_isComplete & _timeline < 1.0f)
                {
                    if (_timeline <= 0.0f)
                        StartAnimation?.Invoke();
                
                    _timeline += (1.0f / _time) * deltaTime;
                    _timeline = Mathf.Clamp01(_timeline);
                
                    UpdateAnimation?.Invoke(_animationCurve.Evaluate(_timeline));

                    if (_timeline >= 1.0f)
                    {
                        FinalAnimation?.Invoke();
                    
                        _isComplete = true;
                    }
                }
            }
        }

        internal void Reset()
        {
            UpdateAnimation = null;
            StartAnimation = null;
            FinalAnimation = null;
        }
        
        public AnimationTweenObject SetCurve(AnimationCurve curve)
        {
            _animationCurve = curve;
            
            return this;
        }
        
        public AnimationTweenObject AppendToStartAnimation(Action action)
        {
            StartAnimation += action;
            
            return this;
        }
        
        public AnimationTweenObject AppendToUpdateAnimation(Action<float> action)
        {
            UpdateAnimation += action;

            return this;
        }
        
        public AnimationTweenObject AppendToFinalAnimation(Action action)
        {
            FinalAnimation += action;
            
            return this;
        }
    }
}
