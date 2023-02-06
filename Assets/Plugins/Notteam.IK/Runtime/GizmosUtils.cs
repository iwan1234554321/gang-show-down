using UnityEngine;

namespace Notteam.IK
{
    public static class GizmosUtils
    {
        public static void DrawBone(Vector3 bonePosition, Vector3 nextBonePosition, Vector3 guideDirection, bool useBothBone, bool useGlobalMatrixForJoint, float thickValue, float sizeJoint, Color colorBone, Color colorJoint, bool drawPositionSphere = true)
        {
            if (drawPositionSphere)
            {
                if (useGlobalMatrixForJoint)
                    Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);

                Gizmos.color = colorJoint;
                Gizmos.DrawWireSphere(bonePosition, sizeJoint);
            }

            if (useBothBone)
            {
                Gizmos.matrix = Matrix4x4.TRS(bonePosition, Quaternion.LookRotation(nextBonePosition - bonePosition, guideDirection), new Vector3(thickValue, thickValue, (nextBonePosition - bonePosition).magnitude));

                Gizmos.color = colorBone;
                Gizmos.DrawWireCube(Vector3.forward * 0.5f, Vector3.one);
            }
        }
    }
}
