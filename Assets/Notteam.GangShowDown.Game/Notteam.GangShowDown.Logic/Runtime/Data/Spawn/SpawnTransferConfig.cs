using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public abstract class SpawnTransferConfig : ScriptableObject
    {
        [SerializeField] private SpawnType type;

        public SpawnType Type => type;
        
        public abstract SpawnObject[] GetSpawnObjects(SpawnTransformData[] transformData, bool random = false);
    }
}
