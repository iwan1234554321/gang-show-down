using System.Collections.Generic;
using UnityEngine;

namespace Notteam.AnimationTween
{
    [DefaultExecutionOrder(1000)]
    internal sealed class AnimationTweenUpdater : MonoBehaviour
    {
        private List<AnimationTweenObject> _tweens = new();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            for (var i = 0; i < _tweens.Count; i++)
            {
                if (!_tweens[i].IsComplete && _tweens[i].Target)
                    _tweens[i].Update(Time.deltaTime);
                else
                    _tweens.RemoveAt(i);
            }
        }

        internal void SetTweens(List<AnimationTweenObject> collection)
        {
            _tweens = collection;
        }
    }
}
