using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [CreateAssetMenu(fileName = "ActionDataConfig", menuName = "Notteam/Notteam.GangShowDown/Create Action Data Config", order = 0)]
    public class ActionDataConfig : ScriptableObject
    {
        [SerializeField] private ActionData data;

        public ActionData Data => data;
    }
}
