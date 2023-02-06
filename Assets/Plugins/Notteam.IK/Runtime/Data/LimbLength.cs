using UnityEditor;
using UnityEngine;

namespace Notteam.IK
{
    public static class CreateLimbLengthAsset
    {
        [MenuItem("GameObject/Notteam/IK/Create Limb Length Data")]
        public static void Create()
        {
            var selectedTransform = Selection.activeTransform;

            if (selectedTransform.childCount > 0 && selectedTransform.GetChild(0).childCount > 0)
            {
                var upperTransform = selectedTransform;
                var middleTransform = selectedTransform.GetChild(0);
                var lowerTransform = selectedTransform.GetChild(0).GetChild(0);

                var upperLength = (upperTransform.position - middleTransform.position).magnitude;
                var lowerLength = (lowerTransform.position - middleTransform.position).magnitude;
                
                var asset = ScriptableObject.CreateInstance<LimbLength>();
                
                asset.SetLengthData(upperLength, lowerLength);
                
                var path = EditorUtility.SaveFilePanelInProject("Save Limb Length Data", "LimbLength", "asset", string.Empty);
                
                if (!string.IsNullOrEmpty(path))
                    AssetDatabase.CreateAsset(asset, path);
            }
            else
                Debug.LogWarning("Selected object cannot be calculated because it lacks child objects");
        }
    }
    
    [CreateAssetMenu(fileName = "LimbLength", menuName = "Notteam/IK/Create Limb Length Data", order = 0)]
    public class LimbLength : ScriptableObject
    {
        [SerializeField] private float upperPartLength;
        [SerializeField] private float lowerPartLength;

        public float UpperPartLength => upperPartLength;
        public float LowerPartLength => lowerPartLength;

        internal void SetLengthData(float upperPartLengthValue, float lowerPartLengthValue)
        {
            upperPartLength = upperPartLengthValue;
            lowerPartLength = lowerPartLengthValue;
        }
    }
}
