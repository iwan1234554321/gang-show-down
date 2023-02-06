using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class AnimationTransformPositionTimeline : AnimationTransformTimeline
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform finalPoint;
        
        private void Update()
        {
            if (TransformObject && startPoint && finalPoint)
            {
                TransformObject.position = Vector3.Lerp(startPoint.position, finalPoint.position, Interpolate);
            }
        }
    }
}
