using UnityEngine;
using UnityEngine.UI;

namespace Notteam.GangShowDown.Logic
{
    [RequireComponent(typeof(Action))]
    public class ActionUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;

        private Action _action;

        private void Awake()
        {
            _action = GetComponent<Action>();
        }

        private void Update()
        {
            if (iconImage)
            {
                iconImage.sprite = _action.Data.icon;
            }
        }
    }
}
