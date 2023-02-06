using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public enum PointerEventState
    {
        Exit,
        Down,
        Up,
        Entered,
    }
    
    [DefaultExecutionOrder(0)]
    public class PointerSystem : MonoBehaviour
    {
        [SerializeField] private float planeHeight = 0.5f;

        [Header("Visual Settings")]
        [SerializeField] private Color planeColor = new Color(0.0f, 1.0f, 0.0f, 0.25f);
        
        private bool _selected;

        private Plane _plane;
        private Vector3 _pointerPosition;
        
        private GameObject _hitGameObject;
        private GameObject _cachedHitGameObject;

        private PointerCollision _selectedPointerCollision;
        
        private Camera _mainCamera;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void AddingAndRemovePointerCollision(PointerCollision collision)
        {
            if (collision)
            {
                _selectedPointerCollision = collision;
                
                _selectedPointerCollision.RegisterEnterCollision();
                
                SelectedPointerEventRegistration(PointerEventState.Entered);
            }
            else
            {
                if (_selectedPointerCollision)
                {
                    _selectedPointerCollision.RegisterExitCollision();
                    
                    SelectedPointerEventRegistration(PointerEventState.Exit);
                    
                    _selectedPointerCollision = null;
                }
            }
        }

        private void SelectedPointerEventRegistration(PointerEventState state)
        {
            if (_selectedPointerCollision)
            {
                switch (state)
                {
                    case PointerEventState.Down:
                        _selectedPointerCollision.RegisterStartDrag(_pointerPosition);
                        break;
                    case PointerEventState.Up:
                        _selectedPointerCollision.RegisterFinalDrag(_pointerPosition);
                        break;
                }
            }
        }

        private void ProcessDrag(Vector3 position)
        {
            if (_selectedPointerCollision)
                _selectedPointerCollision.ProcessDrag(position);
        }

        private void CalculatePlaneAndPointerPosition(Ray ray)
        {
            _plane = new Plane(Vector3.up, Vector3.up * planeHeight);
            
            _plane.Raycast(ray, out var enter);
            
            _pointerPosition = ray.GetPoint(enter);
        }
        
        private void Update()
        {
            _hitGameObject = null;
            
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            CalculatePlaneAndPointerPosition(ray);
            
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider)
                    _hitGameObject = hit.collider.gameObject;
            }

            if (_cachedHitGameObject != _hitGameObject && _selected == false)
            {
                if (_hitGameObject)
                {
                    var pointerCollision = _hitGameObject.GetComponent<PointerCollision>();

                    AddingAndRemovePointerCollision(pointerCollision);
                }
                else
                    AddingAndRemovePointerCollision(null);
                
                _cachedHitGameObject = _hitGameObject;
            }

            if (Input.GetMouseButton(0))
            {
                if (!_selected)
                {
                    SelectedPointerEventRegistration(PointerEventState.Down);
                    
                    _selected = true;
                }
            }
            else
            {
                if (_selected)
                {
                    SelectedPointerEventRegistration(PointerEventState.Up);
                    
                    _selected = false;
                }
            }

            if (_selected)
                ProcessDrag(_pointerPosition);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = planeColor;
            Gizmos.DrawSphere(_pointerPosition, 0.1f);
            Gizmos.DrawCube(Vector3.up * planeHeight, new Vector3(20, 0.01f, 20));
            Gizmos.DrawWireCube(Vector3.up * planeHeight, new Vector3(20, 0.01f, 20));
        }
    }
}
