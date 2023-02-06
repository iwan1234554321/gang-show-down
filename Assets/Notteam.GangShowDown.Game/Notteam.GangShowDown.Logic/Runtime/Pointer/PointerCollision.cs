using UnityEngine;
using UnityEngine.Events;

namespace Notteam.GangShowDown.Logic
{
    public class PointerCollision : MonoBehaviour
    {
        public UnityEvent onEnterCollision;
        public UnityEvent onExitCollision;
        
        public UnityEvent<Vector3> onStartDragCollision;
        public UnityEvent<Vector3> onProcessDragCollision;
        public UnityEvent<Vector3> onFinalDragCollision;

        public void RegisterEnterCollision()
        {
            onEnterCollision?.Invoke();
        }
        
        public void RegisterExitCollision()
        {
            onExitCollision?.Invoke();
        }

        public void RegisterStartDrag(Vector3 position)
        {
            onStartDragCollision?.Invoke(position);
        }
        
        public void ProcessDrag(Vector3 position)
        {
            onProcessDragCollision?.Invoke(position);
        }
        
        public void RegisterFinalDrag(Vector3 position)
        {
            onFinalDragCollision?.Invoke(position);
        }
    }
}
