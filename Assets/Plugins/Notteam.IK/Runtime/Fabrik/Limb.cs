using System;
using UnityEngine;

namespace Notteam.IK
{
    [ExecuteAlways]
    [DefaultExecutionOrder(-1)]
    public class Limb : MonoBehaviour
    {
        [Serializable]
        public struct LimbVisualData
        {
            public float thick;
            public float sizeJoint;
            public Color boneColor;
            public Color targetColor;
            public Color guideColor;
            public Color jointColor;

            public LimbVisualData(
                float thickValue,
                float sizeJointValue,
                Color boneColorValue,
                Color targetColorValue,
                Color guideColorValue,
                Color jointColorValue)
            {
                thick = thickValue;
                sizeJoint = sizeJointValue;
                boneColor = boneColorValue;
                targetColor = targetColorValue;
                guideColor = guideColorValue;
                jointColor = jointColorValue;
            }
        }

        [Header("Setup")]
        [SerializeField] private LimbLength lengthData;
        [SerializeField] private float upperArmLength = 0.4f;
        [SerializeField] private float foreArmLength = 0.3f;
        [SerializeField] private bool useTargetRotation;

        [Header("Targets")]
        [SerializeField] private Transform target;
        [SerializeField] private Transform guide;
        
        [Header("Simulation")]
        [SerializeField] private int iterations = 10;

        [Space]
        [SerializeField] private LimbVisualData visualSettings = new(0.1f, 0.05f, Color.cyan, Color.red, Color.yellow, Color.green);

        private Vector3 _upperArmPosition;
        private Vector3 _foreArmPosition;
        private Vector3 _handArmPosition;
        
        private Quaternion _upperArmRotation;
        private Quaternion _foreArmRotation;
        private Quaternion _handArmRotation;

        private Vector3 _targetDirection;
        
        private float _completeArmLength;

        public Transform Target => target;
        public Transform Guide => guide;
        public Vector3 UpperArmPosition => _upperArmPosition;
        public Vector3 ForeArmPosition => _foreArmPosition;
        public Vector3 HandArmPosition => _handArmPosition;
        
        public Quaternion UpperArmRotation => _upperArmRotation;
        public Quaternion ForeArmRotation => _foreArmRotation;
        public Quaternion HandArmRotation => _handArmRotation;

        private Vector3 SetPositionRelativePoint(Vector3 relativePoint, Vector3 currentPosition, float distance)
        {
            return relativePoint - (relativePoint - currentPosition).normalized * distance;
        }
        
        private void ResolveIK()
        {
            // Temporary solution, since there is a problem with the penetration of a bone inside another on small iterations
            var lengthOffset = 0.01f;
            
            for (var i = 0; i < iterations; i++)
            {
                { // forward

                    _handArmPosition = transform.position + _targetDirection.normalized * Mathf.Clamp(_targetDirection.magnitude, 0, _completeArmLength - lengthOffset);
                    
                    var foreArmLocalPosition = transform.InverseTransformPoint(SetPositionRelativePoint(_handArmPosition, _foreArmPosition, foreArmLength));

                    _foreArmPosition = transform.TransformPoint(0, foreArmLocalPosition.y >= 0.0f ? 0.0f : foreArmLocalPosition.y, foreArmLocalPosition.z);
                }
                
                { // back
                    
                    _handArmPosition = transform.TransformPoint(transform.InverseTransformPoint(SetPositionRelativePoint(_foreArmPosition, _handArmPosition, foreArmLength)));
                    
                    var foreArmLocalPosition = transform.InverseTransformPoint(SetPositionRelativePoint(/*_upperArmPosition*/transform.position, _foreArmPosition, upperArmLength));
                    
                    _foreArmPosition = transform.TransformPoint(0, foreArmLocalPosition.y >= 0.0f ? 0.0f : foreArmLocalPosition.y, foreArmLocalPosition.z);
                }
                
                
            }
        }

        private void Simulation()
        {
            if (lengthData)
            {
                upperArmLength = lengthData.UpperPartLength;
                foreArmLength = lengthData.LowerPartLength;
            }
            
            _completeArmLength = upperArmLength + foreArmLength;
            
            var transformPosition = transform.position;
            var transformDirection = transform.forward;
            
            _upperArmPosition = transformPosition;
            
            if (target)
            {
                _targetDirection = target.position - transform.position;
                
                var guideDirection = guide ? -(guide.position - transform.position).normalized : Vector3.up;
                
                transform.rotation = Quaternion.LookRotation(target.position - transform.position, guideDirection);

                ResolveIK();

                _upperArmRotation = Quaternion.LookRotation(_foreArmPosition - _upperArmPosition, transform.right);
                _foreArmRotation = Quaternion.LookRotation(_handArmPosition - _foreArmPosition, transform.right);

                if (useTargetRotation)
                    _handArmRotation = target.rotation;
            }
            else
            {
                _foreArmPosition = _upperArmPosition + transformDirection * upperArmLength;
                _handArmPosition = _foreArmPosition + transformDirection * foreArmLength;
            }
        }
        
        // private void FixedUpdate()
        // {
        //     Simulation();
        // }

        private void Update()
        {
            // if (Application.isPlaying == false)
                Simulation();
        }

        private void OnDrawGizmos()
        {
            var guideDirection = Vector3.up;

            if (guide)
                guideDirection = -(guide.position - transform.position).normalized;
            
            GizmosUtils.DrawBone(_upperArmPosition, _foreArmPosition, guideDirection, true, false, visualSettings.thick, visualSettings.sizeJoint, visualSettings.boneColor, visualSettings.jointColor);
            GizmosUtils.DrawBone(_foreArmPosition, _handArmPosition, guideDirection,true, true, visualSettings.thick, visualSettings.sizeJoint, visualSettings.boneColor, visualSettings.jointColor);

            if (target)
            {
                Gizmos.matrix = Matrix4x4.TRS(target.position, target.rotation, target.localScale);
                Gizmos.color = visualSettings.targetColor;
                Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            }
            
            if (guide)
            {
                Gizmos.matrix = Matrix4x4.TRS(guide.position, guide.rotation, guide.localScale);
                Gizmos.color = visualSettings.guideColor;
                Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            }
        }
    }
}
