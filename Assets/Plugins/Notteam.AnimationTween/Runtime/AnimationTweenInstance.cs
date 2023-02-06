using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Notteam.AnimationTween
{
    public static class AnimationTweenInstance
    {
        private static readonly List<AnimationTweenObject> Tweens = new();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialization()
        {
            var updater = new GameObject("AnimationTweenUpdater", typeof(AnimationTweenUpdater)).GetComponent<AnimationTweenUpdater>();
            
            updater.SetTweens(Tweens);

            SceneManager.sceneUnloaded += DestroyAnimationFromUnloadedScene;
            
            Debug.Log($"<color=#409CFF>Animation Tween Instance</color> Initialized");
        }

        private static void DestroyAnimationFromUnloadedScene(Scene scene)
        {
            for (var i = 0; i < Tweens.Count; i++)
            {
                if (Tweens[i].SceneAnimation == scene)
                    Tweens.Remove(Tweens[i]);
            }
        }
        
        public static AnimationTweenObject CreateTween(GameObject target, string id, float time, Action startAnimation = null, Action<float> updateAnimation = null, Action finalAnimation = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                Debug.LogWarning($"The current tween id {id} must not be empty");
                
                return null;
            }

            var completeId = $"{id} : {target.GetInstanceID()}";
            
            var existTween = Tweens.Find(element => element.Id == completeId);

            if (existTween != null)
            {
                existTween.Reset();
                
                Tweens.Remove(existTween);
            }

            Tweens.Add(new AnimationTweenObject(target, id, time, startAnimation, updateAnimation, finalAnimation));
            
            return Tweens[^1];
        }

        public static void DestroyTween(GameObject target, string id)
        {
            var completeId = $"{id} : {target.GetInstanceID()}";
            
            var existTween = Tweens.Find(element => element.Id == completeId);
            
            if (existTween != null)
            {
                existTween.Reset();
                
                Tweens.Remove(existTween);
            }
        }
    }
}