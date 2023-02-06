using System;
using Notteam.AnimationTween;
using UnityEngine;

namespace Notteam.IK
{
    [Serializable]
    public struct PoseSequenceData
    {
        public string name;
        
        [Header("Params Sequence")]
        public bool loop;
        public bool hold;
        
        public LimbFullBodyPose[] poses;
    }
    
    [RequireComponent(typeof(LimbFullBodyPoser))]
    public class LimbFullBodyPoseAnimator : MonoBehaviour
    {
        [SerializeField] private bool animate;
        [SerializeField] private PoseSequenceData[] sequences;

        private bool _setDataToPoser;
        
        private bool _changedAnimate;
        
        private int _currentSequence;
        private int _currentSequencePose;

        private int _changedSequence;
        
        private LimbFullBodyPoseData _startPoseData;
        private LimbFullBodyPoseData _currentPoseData;
        
        private LimbFullBodyPoser _poser;

        private void SetDefaultPose()
        {
            if (sequences.Length > 0 && sequences[0].poses.Length > 0)
                _currentPoseData = sequences[0].poses[0].Pose;
        }
        
        private void Awake()
        {
            _poser = GetComponent<LimbFullBodyPoser>();

            SetDefaultPose();
            
            if (animate)
                Animate();
        }
        
        private void Animate()
        {
            var currentPose = sequences[_currentSequence].poses[_currentSequencePose];
            
            var tween = AnimationTweenInstance.CreateTween(gameObject, "Animate IK Pose", currentPose.Time, StartAnimation, UpdateAnimation, FinalAnimation);

            if (currentPose.Curve)
                tween.SetCurve(currentPose.Curve.Curve);
        }
        
        private void StartAnimation()
        {
            _startPoseData = _currentPoseData;
        }
        
        private void UpdateAnimation(float value)
        {
            if (animate)
                _currentPoseData.Lerp(_startPoseData, sequences[_currentSequence].poses[_currentSequencePose].Pose, value);
            else
                AnimationTweenInstance.DestroyTween(gameObject, "Animate IK Pose");
        }
        
        private void FinalAnimation()
        {
            var currentSequence = sequences[_currentSequence];
            
            if (_currentSequencePose < currentSequence.poses.Length - 1)
            {
                _currentSequencePose++;
                
                Animate();
            }
            else
            {
                if (!currentSequence.hold)
                {
                    _currentSequencePose = 0;

                    if (!currentSequence.loop)
                        _currentSequence = 0;
                
                    Animate();
                }
            }
        }

        private void Update()
        {
            if (_changedAnimate != animate)
            {
                if (animate)
                {
                    _setDataToPoser = true;
                    
                    Animate();
                }
                else
                {
                    _currentSequencePose = 0;
                    _currentSequence = 0;
                    
                    SetDefaultPose();
                    
                    _setDataToPoser = false;
                }
                
                _changedAnimate = animate;
            }
            
            if (_setDataToPoser)
                _poser.SetPoseData(_currentPoseData);
        }

        public void PlayAnimation(string nameSequence)
        {
            if (animate)
            {
                for (var i = 0; i < sequences.Length; i++)
                {
                    var sequence = sequences[i];
                
                    if (sequence.name == nameSequence)
                    {
                        _currentSequencePose = 0;
                        _currentSequence = i;
                    
                        Animate();
                    
                        break;
                    }
                }
            }
        }
    }
}
