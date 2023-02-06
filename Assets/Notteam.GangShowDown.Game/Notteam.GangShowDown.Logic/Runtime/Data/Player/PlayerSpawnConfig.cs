using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [CreateAssetMenu(fileName = "PlayerSpawnConfig", menuName = "Notteam/Notteam.GangShowDown/Create Player Spawn Config", order = 0)]
    public class PlayerSpawnConfig : SpawnTransferConfig
    {
        [SerializeField] private PlayerCharacterCollectionConfig playerCharacterCollectionConfig;
        
        public override SpawnObject[] GetSpawnObjects(SpawnTransformData[] transformData, bool random = false)
        {
            var playerObjects = new SpawnObject[transformData.Length];
            
            if (playerCharacterCollectionConfig)
            {
                for (var i = 0; i < playerObjects.Length; i++)
                {
                    if (i <= playerCharacterCollectionConfig.Characters.Length - 1)
                    {
                        var randomValue = Utils.GetArrayRandomNumber(playerCharacterCollectionConfig.Characters.Length, 100);
                    
                        var currentIndex = random ? randomValue : i;
                    
                        var config = playerCharacterCollectionConfig.Characters[currentIndex];

                        if (config.Data.prefab)
                        {
                            var playerClone = Instantiate(config.Data.prefab, transformData[i].position, transformData[i].rotation);

                            playerClone.Data = new PlayerData(config.Data.playerData);

                            playerObjects[i] = playerClone;
                        }
                    }
                }
            }
            
            return playerObjects;
        }
    }
}
