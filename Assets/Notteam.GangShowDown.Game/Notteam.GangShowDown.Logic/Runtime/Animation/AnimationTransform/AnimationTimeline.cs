using UnityEngine;
using UnityEngine.Events;

namespace Notteam.GangShowDown.Logic
{
    [ExecuteAlways]
    public class AnimationTimeline : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float interpolate;

        [Space]
        [SerializeField] private UnityEvent<float> onUpdateEvent;

        public float Interpolate { get => interpolate; set => interpolate = value; }

        private void Update()
        {
            onUpdateEvent?.Invoke(interpolate);
        }
    }
}
