using System;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public enum SpawnRequest
    {
        None,
        
        SceneStart,
        
        Player,
        PlayerTeam,
        EnemyTeam,
    }
    
    [Serializable]
    public struct SpawnTransformData
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    
    [Serializable]
    public struct SpawnRequestData
    {
        public SpawnRequest requestType;
        public SpawnTransferConfig[] configs;
    }
    
    [CreateAssetMenu(fileName = "SpawnConfig", menuName = "Notteam/Notteam.GangShowDown/Create Spawn Config", order = 0)]
    public class SpawnConfig : ScriptableObject
    {
        [SerializeField] private SpawnRequestData[] requestData;

        public SpawnObject[] GetSpawnObjectsByRequest(SpawnRequest requestType, SpawnTransformData[] transformData, SpawnType type, bool random = false)
        {
            var collection = new SpawnObject[transformData.Length];

            foreach (var data in requestData)
            {
                if (data.requestType == requestType)
                {
                    foreach (var config in data.configs)
                    {
                        if (config && config.Type == type)
                        {
                            collection = config.GetSpawnObjects(transformData, random);
                            
                            break;
                        }
                    }

                    break;
                }
            }
            
            return collection;
        }
    }
}
