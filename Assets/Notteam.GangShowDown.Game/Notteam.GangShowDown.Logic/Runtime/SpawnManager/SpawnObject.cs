using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class SpawnObject : MonoBehaviour
    {
        [SerializeField] private SpawnType type;

        public SpawnType Type => type;
    }
}
