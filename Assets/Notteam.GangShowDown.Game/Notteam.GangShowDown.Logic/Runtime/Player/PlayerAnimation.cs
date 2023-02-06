using Notteam.IK;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class PlayerAnimation : MonoBehaviour
    {
        private LimbFullBodyPoseAnimator _animator;

        public LimbFullBodyPoseAnimator Animator => _animator;
        
        private void Awake()
        {
            _animator = GetComponentInChildren<LimbFullBodyPoseAnimator>();
        }
    }
}
