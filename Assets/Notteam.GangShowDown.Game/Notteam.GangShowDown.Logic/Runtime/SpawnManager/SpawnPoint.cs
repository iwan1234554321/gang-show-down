using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public enum SpawnType
    {
        Player,
        Enemy,
        Action
    }
    
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private SpawnType spawnType;
        public SpawnType SpawnType => spawnType;
    }
}
