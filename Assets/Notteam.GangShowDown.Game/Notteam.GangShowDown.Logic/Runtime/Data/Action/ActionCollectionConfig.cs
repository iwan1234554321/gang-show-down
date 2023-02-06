using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [CreateAssetMenu(fileName = "ActionCollectionConfig", menuName = "Notteam/Notteam.GangShowDown/Create Action Collection Config", order = 0)]
    public class ActionCollectionConfig : ScriptableObject
    {
        [SerializeField] private Action prefab;
        [SerializeField] private ActionDataConfig[] actions;

        public Action Prefab => prefab;
        public ActionDataConfig[] Actions => actions;
    }
}
