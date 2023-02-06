using System.Collections.Generic;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [DefaultExecutionOrder(0)]
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private SpawnConfig config;
        
        private readonly List<SpawnPoint> _playerSpawnPoints = new();
        private readonly List<SpawnPoint> _enemySpawnPoints = new();
        private readonly List<SpawnPoint> _actionSpawnPoints = new();

        private SpawnTransformData[] ConvertSpawnPointsToSpawnTransformData(SpawnPoint[] points)
        {
            var spawnTransformData = new SpawnTransformData[points.Length];

            for (var i = 0; i < spawnTransformData.Length; i++)
            {
                spawnTransformData[i].position = points[i].transform.position;
                spawnTransformData[i].rotation = points[i].transform.rotation;
            }

            return spawnTransformData;
        }
        
        private SpawnObject[] SpawnByRequestInternal(SpawnRequest requestType, SpawnTransformData[] transformData, SpawnType type, bool random = false)
        {
            return config.GetSpawnObjectsByRequest(requestType, transformData, type, random);
        }
        
        private SpawnObject[] SpawnObjectsWithConvertedSpawnPoints(SpawnRequest requestType, SpawnPoint[] spawnPoints, SpawnType type, bool random = false)
        {
            var transformData = ConvertSpawnPointsToSpawnTransformData(spawnPoints);

            return SpawnByRequestInternal(requestType, transformData, type, random);
        }
        
        private void Awake()
        {
            foreach (var spawnPoint in GetComponentsInChildren<SpawnPoint>())
            {
                if (spawnPoint.SpawnType == SpawnType.Player)
                    _playerSpawnPoints.Add(spawnPoint);
                else if (spawnPoint.SpawnType == SpawnType.Enemy)
                    _enemySpawnPoints.Add(spawnPoint);
                else if (spawnPoint.SpawnType == SpawnType.Action)
                    _actionSpawnPoints.Add(spawnPoint);
            }

            SpawnObjectsWithConvertedSpawnPoints(SpawnRequest.SceneStart, _playerSpawnPoints.ToArray(), SpawnType.Player);
            SpawnObjectsWithConvertedSpawnPoints(SpawnRequest.SceneStart, _enemySpawnPoints.ToArray(), SpawnType.Enemy);
            SpawnObjectsWithConvertedSpawnPoints(SpawnRequest.SceneStart, _actionSpawnPoints.ToArray(), SpawnType.Action);
        }

        public SpawnObject[] SpawnByRequest(SpawnRequest requestType, SpawnTransformData[] transformData, SpawnType type, bool random = false)
        {
            return SpawnByRequestInternal(requestType, transformData, type, random);
        }
    }
}
