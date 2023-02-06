using System;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class AnimationTransformRotationTimeline : AnimationTransformTimeline
    {
        [Serializable]
        public struct AnimationTransformRotationSequenceData
        {
            [Range(0, 1)]
            public float time;
            public Vector3 transformRotation;
        }
        
        [SerializeField] private AnimationTransformRotationSequenceData[] rotationSequence;

        private void Update()
        {
            if (TransformObject)
            {
                for (var i = 0; i < rotationSequence.Length; i++)
                {
                    if (i < rotationSequence.Length - 1)
                    {
                        var currentSequence = rotationSequence[i];
                        var nextSequence = rotationSequence[i + 1];
                    
                        if (Interpolate >= currentSequence.time & Interpolate <= nextSequence.time)
                        {
                            var normalizedLerp = (Interpolate - currentSequence.time) / (nextSequence.time - currentSequence.time);
                        
                            var currentRotation = Quaternion.Euler(currentSequence.transformRotation);
                            var nextRotation = Quaternion.Euler(nextSequence.transformRotation);
                        
                            TransformObject.localRotation = Quaternion.Lerp(currentRotation, nextRotation, normalizedLerp);
                        }
                    }
                }
            }
        }
    }
}
