using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace Notteam.IK
{
    [DefaultExecutionOrder(2)]
    public class LimbFullBody : MonoBehaviour
    {
        [Serializable]
        public struct TransformConfigureData
        {
            public Transform transform;
            [Space]
            public bool usePosition;
            public Vector3 rotationOffset;
            [Space]
            public Transform target;
        }
        
        [Serializable]
        public struct LimbConfigureData
        {
            public Limb limb;
            [Space]
            public Vector3 upperRotationOffset;
            public Vector3 lowerRotationOffset;
            public Vector3 lastRotationOffset;
            [Space]
            public Transform upperTransform;
            public Transform lowerTransform;
            public Transform lastTransform;
        }

        [Serializable]
        public struct FullBodyData
        {
            public LimbConfigureData handL;
            public LimbConfigureData handR;
            public LimbConfigureData legL;
            public LimbConfigureData legR;
            [Space]
            public TransformConfigureData hip;
            public TransformConfigureData spineStart;
            public TransformConfigureData spineMiddle;
            public TransformConfigureData spineFinal;
            public TransformConfigureData neck;
            public TransformConfigureData head;
        }
        
        [SerializeField] private LimbFullBodyPoser poser;
        [Space]
        [SerializeField] private FullBodyData fullBodyData;

        private LimbFullBodyPose _cachedPose;
        
        private void SetupLimbConfigureData(LimbConfigureData data)
        {
            if (data.upperTransform && data.lowerTransform && data.limb)
            {
                data.upperTransform.position = data.limb.UpperArmPosition;
                data.upperTransform.rotation = data.limb.UpperArmRotation * Quaternion.Euler(data.upperRotationOffset);
                
                data.lowerTransform.position = data.limb.ForeArmPosition;
                data.lowerTransform.rotation = data.limb.ForeArmRotation * Quaternion.Euler(data.lowerRotationOffset);

                if (data.lastTransform)
                    data.lastTransform.rotation = data.limb.HandArmRotation * Quaternion.Euler(data.lastRotationOffset);
            }
        }

        private void SetupTransform(TransformConfigureData data)
        {
            if (data.transform && data.target)
            {
                if (data.usePosition)
                    data.transform.position = data.target.position;
                
                data.transform.rotation = data.target.rotation * Quaternion.Euler(data.rotationOffset);
            }
        }

        private void SetupLimbPose(LimbConfigureData limb, LimbTransformData data)
        {
            limb.limb.Target.localPosition = data.targetPositions.position;
            limb.limb.Target.localRotation = data.targetPositions.rotation;
                
            limb.limb.Guide.localPosition = data.guidePositions.position;
            limb.limb.Guide.localRotation = data.guidePositions.rotation;
        }

        private void SetupTransformData(TransformConfigureData transformData, TransformData data)
        {
            transformData.target.localPosition = data.position;
            transformData.target.localRotation = data.rotation;
        }
        
#if UNITY_EDITOR
        private LimbTransformData GetLimbTransformData(LimbConfigureData data)
        {
            var limbTransformData = default(LimbTransformData);
            
            if (data.limb.Target && data.limb.Guide)
            {
                limbTransformData.targetPositions.position = data.limb.Target.localPosition;
                limbTransformData.targetPositions.rotation = data.limb.Target.localRotation;
                
                limbTransformData.guidePositions.position = data.limb.Guide.localPosition;
                limbTransformData.guidePositions.rotation = data.limb.Guide.localRotation;
            }

            return limbTransformData;
        }

        private TransformData GetTransformData(TransformConfigureData data)
        {
            var transformData = default(TransformData);

            if (data.transform)
            {
                transformData.position = data.target.localPosition;
                transformData.rotation = data.target.localRotation;
            }
            
            return transformData;
        }
        
        [ContextMenu("Create Pose Data")]
        private void CreatePoseData()
        {
            var data = default(LimbFullBodyPoseData);

            data.handL = GetLimbTransformData(fullBodyData.handL);
            data.handR = GetLimbTransformData(fullBodyData.handR);
            data.legL = GetLimbTransformData(fullBodyData.legL);
            data.legR = GetLimbTransformData(fullBodyData.legR);
            
            data.hip = GetTransformData(fullBodyData.hip);
            data.spineStart = GetTransformData(fullBodyData.spineStart);
            data.spineMiddle = GetTransformData(fullBodyData.spineMiddle);
            data.spineFinal = GetTransformData(fullBodyData.spineFinal);
            data.neck = GetTransformData(fullBodyData.neck);
            data.head = GetTransformData(fullBodyData.head);
            
            var asset = ScriptableObject.CreateInstance<LimbFullBodyPose>();
            
            asset.SetData(data);
            
            var path = EditorUtility.SaveFilePanelInProject("Save Limb Full Body Pose Data", "LimbFullBodyPose", "asset", string.Empty);

            if (!string.IsNullOrEmpty(path))
                AssetDatabase.CreateAsset(asset, path);
            
            AssetDatabase.Refresh();
        }
#endif
        private void Update()
        {
            SetupTransform(fullBodyData.hip);
            SetupTransform(fullBodyData.spineStart);
            SetupTransform(fullBodyData.spineMiddle);
            SetupTransform(fullBodyData.spineFinal);
            SetupTransform(fullBodyData.neck);
            SetupTransform(fullBodyData.head);
            
            SetupLimbConfigureData(fullBodyData.handL);
            SetupLimbConfigureData(fullBodyData.handR);
            SetupLimbConfigureData(fullBodyData.legL);
            SetupLimbConfigureData(fullBodyData.legR);
            
            if (poser && poser.enabled)
            {
                SetupTransformData(fullBodyData.hip, poser.PoseData.hip);
                SetupTransformData(fullBodyData.spineStart, poser.PoseData.spineStart);
                SetupTransformData(fullBodyData.spineMiddle, poser.PoseData.spineMiddle);
                SetupTransformData(fullBodyData.spineFinal, poser.PoseData.spineFinal);
                SetupTransformData(fullBodyData.neck, poser.PoseData.neck);
                SetupTransformData(fullBodyData.head, poser.PoseData.head);
                
                SetupLimbPose(fullBodyData.handL, poser.PoseData.handL);
                SetupLimbPose(fullBodyData.handR, poser.PoseData.handR);
                SetupLimbPose(fullBodyData.legL, poser.PoseData.legL);
                SetupLimbPose(fullBodyData.legR, poser.PoseData.legR);
            }
        }
    }
}
