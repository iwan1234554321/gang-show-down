using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [CreateAssetMenu(fileName = "ActionSpawnConfig", menuName = "Notteam/Notteam.GangShowDown/Create Action Spawn Config", order = 0)]
    public class ActionSpawnConfig : SpawnTransferConfig
    {
        [SerializeField] private ActionCollectionConfig actionCollectionConfig;
        
        public override SpawnObject[] GetSpawnObjects(SpawnTransformData[] transformData, bool random = false)
        {
            var actionObjects = new SpawnObject[transformData.Length];
            
            if (actionCollectionConfig && actionCollectionConfig.Prefab)
            {
                for (var i = 0; i < actionObjects.Length; i++)
                {
                    if (i <= actionCollectionConfig.Actions.Length - 1)
                    {
                        var randomValue = Utils.GetArrayRandomNumber(actionCollectionConfig.Actions.Length, 100);
                    
                        var currentIndex = random ? randomValue : i;
                    
                        var action = actionCollectionConfig.Actions[currentIndex];

                        var actionClone = Instantiate(actionCollectionConfig.Prefab, transformData[i].position, transformData[i].rotation);

                        actionClone.Data = new ActionData(action.Data);
                    
                        actionObjects[i] = actionClone;
                    }
                }
            }
            
            return actionObjects;
        }
    }
}
