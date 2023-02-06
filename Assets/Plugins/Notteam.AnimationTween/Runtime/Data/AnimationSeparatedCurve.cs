using UnityEngine;

namespace Notteam.AnimationTween
{
    [CreateAssetMenu(fileName = "AnimationCurve", menuName = "Notteam/AnimationTween/Create Animation Curve", order = 0)]
    public class AnimationSeparatedCurve : ScriptableObject
    {
        [SerializeField] private AnimationCurve curve;

        public AnimationCurve Curve => curve;
    }
}
