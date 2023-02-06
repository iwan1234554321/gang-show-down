using System;
using Notteam.AnimationTween;
using UnityEngine;

namespace Notteam.IK
{
    [Serializable]
    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        
        public void Lerp(TransformData a, TransformData b, float t)
        {
            position = Vector3.Lerp(a.position, b.position, t);
            rotation = Quaternion.Lerp(a.rotation, b.rotation, t);
        }
    }

    [Serializable]
    public struct LimbTransformData
    {
        public TransformData targetPositions;
        public TransformData guidePositions;
        
        public void Lerp(LimbTransformData a, LimbTransformData b, float t)
        {
            targetPositions.Lerp(a.targetPositions, b.targetPositions, t);
            guidePositions.Lerp(a.guidePositions, b.guidePositions, t);
        }
    }

    [Serializable]
    public struct LimbFullBodyPoseData
    {
        public LimbTransformData handL;
        public LimbTransformData handR;
        public LimbTransformData legL;
        public LimbTransformData legR;
        [Space]
        public TransformData hip;
        public TransformData spineStart;
        public TransformData spineMiddle;
        public TransformData spineFinal;
        public TransformData neck;
        public TransformData head;

        public void Lerp(LimbFullBodyPoseData a, LimbFullBodyPoseData b, float t)
        {
            handL.Lerp(a.handL, b.handL, t);
            handR.Lerp(a.handR, b.handR, t);
            legL.Lerp(a.legL, b.legL, t);
            legR.Lerp(a.legR, b.legR, t);
            
            hip.Lerp(a.hip, b.hip, t);
            spineStart.Lerp(a.spineStart, b.spineStart, t);
            spineMiddle.Lerp(a.spineMiddle, b.spineMiddle, t);
            spineFinal.Lerp(a.spineFinal, b.spineFinal, t);
            neck.Lerp(a.neck, b.neck, t);
            head.Lerp(a.head, b.head, t);
        }
    }
    
    [CreateAssetMenu(fileName = "LimbFullBodyPose", menuName = "Notteam/IK/Create Limb Full Body Pose Data", order = 0)]
    public class LimbFullBodyPose : ScriptableObject
    {
        [SerializeField] private float time = 1.0f;
        [SerializeField] private AnimationSeparatedCurve curve;
        [Space]
        [SerializeField] private LimbFullBodyPoseData pose;
        
        public float Time => time;
        public AnimationSeparatedCurve Curve => curve;
        public LimbFullBodyPoseData Pose => pose;
        
        internal void SetData(LimbFullBodyPoseData poseData) => pose = poseData;
    }
}
