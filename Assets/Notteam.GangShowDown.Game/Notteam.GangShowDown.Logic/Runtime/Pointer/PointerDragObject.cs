using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [RequireComponent(typeof(PointerCollision))]
    public class PointerDragObject : MonoBehaviour
    {
        [SerializeField] private Transform draggedTransform;

        public event System.Action<Vector3> OnBeginDrag;
        public event System.Action<Vector3> OnProcessDrag;
        public event System.Action<Vector3> OnEndDrag;

        private bool _dragged;
        private bool _draggedOutside;
        
        private Vector3 _dragDelta;
        
        private PointerCollision _pointerCollision;

        private void BeginDragInternal(Vector3 position)
        {
            if (!_dragged)
            {
                if (draggedTransform)
                    _dragDelta = draggedTransform.position - position;
            
                OnBeginDrag?.Invoke(position);

                _dragged = true;
            }
        }
        
        private void ProcessDragInternal(Vector3 position)
        {
            _dragged = true;
            
            if (draggedTransform)
                draggedTransform.position = position + _dragDelta;
            
            OnProcessDrag?.Invoke(position);
        }

        private void EndDragInternal(Vector3 position)
        {
            if (_dragged)
            {
                OnEndDrag?.Invoke(position);

                _dragged = false;
            }
        }
        
        private void BeginDrag(Vector3 position)
        {
            if (!_draggedOutside)
                BeginDragInternal(position);
        }
        
        private void ProcessDrag(Vector3 position)
        {
            if (!_draggedOutside)
                ProcessDragInternal(position);
        }
        
        private void EndDrag(Vector3 position)
        {
            if (!_draggedOutside)
                EndDragInternal(position);
        }
        
        public void SetBeginDrag(Vector3 position)
        {
            _draggedOutside = true;
            
            BeginDragInternal(position);
        }
        
        public void SetProcessDrag(Vector3 position)
        {
            _draggedOutside = true;
            
            ProcessDragInternal(position);
        }
        
        public void SetEndDrag(Vector3 position)
        {
            _draggedOutside = false;
            
            EndDragInternal(position);
        }
        
        private void Awake()
        {
            _pointerCollision = GetComponent<PointerCollision>();

            _pointerCollision.onStartDragCollision.AddListener(BeginDrag);
            _pointerCollision.onProcessDragCollision.AddListener(ProcessDrag);
            _pointerCollision.onFinalDragCollision.AddListener(EndDrag);
        }
    }
}
