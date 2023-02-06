using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [ExecuteAlways]
    public abstract class AnimationTransformTimeline : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float interpolate;
        [SerializeField] private Transform transformObject;

        public float Interpolate { get => interpolate; set => interpolate = value; }
        public Transform TransformObject => transformObject;

        public void SetTransformObject(Transform transformValue)
        {
            transformObject = transformValue;
        }
    }
}
