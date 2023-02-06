using UnityEngine;

namespace Notteam.IK
{
    [DefaultExecutionOrder(1)]
    public class LimbFullBodyPoser : MonoBehaviour
    {
        [SerializeField] private LimbFullBodyPose pose;

        private LimbFullBodyPoseData _poseData;
        public LimbFullBodyPoseData PoseData => _poseData;

        private LimbTransformData ConvertedLimbPoseDataToWorld(LimbTransformData poseLimbData)
        {
            var localLimbData = default(LimbTransformData);
            
            localLimbData.targetPositions.position = poseLimbData.targetPositions.position;
            localLimbData.targetPositions.rotation = poseLimbData.targetPositions.rotation;
            
            localLimbData.guidePositions.position = poseLimbData.guidePositions.position;
            localLimbData.guidePositions.rotation = poseLimbData.guidePositions.rotation;

            return localLimbData;
        }
        
        private TransformData ConvertedTransformPoseDataToWorld(TransformData poseTransformData)
        {
            var localTransformData = default(TransformData);
            
            localTransformData.position = poseTransformData.position;
            localTransformData.rotation = poseTransformData.rotation;

            return localTransformData;
        }

        private void Update()
        {
            if (pose)
            {
                _poseData.hip = ConvertedTransformPoseDataToWorld(pose.Pose.hip);
                _poseData.spineStart = ConvertedTransformPoseDataToWorld(pose.Pose.spineStart);
                _poseData.spineMiddle = ConvertedTransformPoseDataToWorld(pose.Pose.spineMiddle);
                _poseData.spineFinal = ConvertedTransformPoseDataToWorld(pose.Pose.spineFinal);
                _poseData.neck = ConvertedTransformPoseDataToWorld(pose.Pose.neck);
                _poseData.head = ConvertedTransformPoseDataToWorld(pose.Pose.head);
                
                _poseData.handL = ConvertedLimbPoseDataToWorld(pose.Pose.handL);
                _poseData.handR = ConvertedLimbPoseDataToWorld(pose.Pose.handR);
                _poseData.legL = ConvertedLimbPoseDataToWorld(pose.Pose.legL);
                _poseData.legR = ConvertedLimbPoseDataToWorld(pose.Pose.legR);
            }
        }

        internal void SetPoseData(LimbFullBodyPoseData poseData)
        {
            _poseData = poseData;
        }
    }
}
