using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class TransformBillboard : MonoBehaviour
    {
        private Transform _cameraTransform;
        
        private void Awake()
        {
            if (Camera.main != null)
                _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            if (_cameraTransform)
                transform.forward = _cameraTransform.forward;
        }
    }
}
