using System.Collections.Generic;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public static class Utils
    {
        #region Actions

        public static PlayerData ApplyActionDataToPlayerData(PlayerData changedPlayerData, ref List<ActionData> actions)
        {
            var listTypeActionsForRemove = new List<ActionType>();
            
            for (var i = 0; i < actions.Count; i++)
            {
                var action = actions[i];

                for (var j = 0; j < action.functions.Count; j++)
                {
                    var function = action.functions[j];

                    switch (function.type)
                    {
                        case ActionFunctionType.AddHealth:
                            
                            changedPlayerData.health += function.healthModifer;
                            
                            break;
                        case ActionFunctionType.RemoveHealth:

                            if (!changedPlayerData.isDefended)
                            {
                                for (var k = 0; k < function.healthModifer; k++)
                                {
                                    if (changedPlayerData.defence > 0)
                                        changedPlayerData.defence -= 1;
                                    else
                                    {
                                        if (changedPlayerData.health > 0)
                                            changedPlayerData.health -= 1;
                                    }
                                }
                            }
                            
                            break;
                        case ActionFunctionType.AddDefence:
                            
                            changedPlayerData.defence += function.defenceModifer;
                            
                            break;
                        case ActionFunctionType.RemoveDefence:
                            
                            if (changedPlayerData.defence > 0)
                                changedPlayerData.defence -= function.defenceModifer;
                            
                            break;
                        case ActionFunctionType.SetPoisoned:
                            
                            changedPlayerData.isPoisoned = true;
                            
                            break;
                        case ActionFunctionType.SetDefended:
                            
                            changedPlayerData.isDefended = true;
                            
                            break;
                        case ActionFunctionType.RemoveAction:
                            
                            for (var k = 0; k < function.addOrRemoveAction.Length; k++)
                            {
                                var currentRemovedAction = function.addOrRemoveAction[k];
                                        
                                for (var x = 0; x < actions.Count; x++)
                                {
                                    var currentAction = actions[x];

                                    if (currentAction.type == currentRemovedAction)
                                        listTypeActionsForRemove.Add(currentAction.type);
                                }
                            }
                            
                            break;
                    }

                    if (function.updateCount > 0)
                    {
                        Debug.Log($"FUNCTION : {function.type} MINUS UPDATE COUNT");
                        
                        function.updateCount -= 1;
                    }

                    actions[i].functions[j] = function;
                }
                
                actions[i] = action;
                
                changedPlayerData.modifer = action.iconModifer;
                changedPlayerData.modiferColor = action.iconModiferColor;
            }

            foreach (var type in listTypeActionsForRemove)
            {
                for (var i = 0; i < actions.Count; i++)
                {
                    if (actions[i].type == type)
                        actions.RemoveAt(i);
                }
            }
            
            for (var i = 0; i < actions.Count; i++)
            {
                var action = actions[i];

                for (var x = 0; x < action.functions.Count; x++)
                {
                    var function = action.functions[x];
                    
                    if (function.updateCount == 0)
                    {
                        switch (function.type)
                        {
                            case ActionFunctionType.SetPoisoned:

                                if (changedPlayerData.isPoisoned)
                                    changedPlayerData.isPoisoned = false;
                                
                                break;
                            
                            case ActionFunctionType.SetDefended:

                                if (changedPlayerData.isDefended)
                                    changedPlayerData.isDefended = false;
                                
                                break;
                        }
                        
                        action.functions.RemoveAt(x);

                        x--;
                    }
                }

                actions[i] = action;
                
                if (action.functions.Count == 0)
                    actions.RemoveAt(i);
            }

            if (actions.Count == 0)
            {
                changedPlayerData.modifer = null;
                changedPlayerData.modiferColor = Color.clear;
            }
            
            return changedPlayerData;
        }
        
        public static bool CheckCompatibilityAction(ActionUseType useType, Player player, Player otherPlayer)
        {
            switch (useType)
            {
                case ActionUseType.OnlyMe:
                    if (player == otherPlayer)
                        return true;
                            
                    break;
                case ActionUseType.MeAndOther:
                    return true;
                case ActionUseType.MeAndMyTeam:
                    if (player == otherPlayer || otherPlayer.Type == player.Type)
                        return true;
                            
                    break;
                case ActionUseType.MeAndOtherTeam:
                    if (player == otherPlayer || otherPlayer.Type != player.Type)
                        return true;
                            
                    break;
                case ActionUseType.OnlyOther:
                    if (player != otherPlayer)
                        return true;
                    
                    break;
                case ActionUseType.OnlyMyTeam:
                    
                    if (player != otherPlayer & otherPlayer.Type == player.Type)
                        return true;
                            
                    break;
                case ActionUseType.OnlyOtherTeam:
                    
                    if (player != otherPlayer & otherPlayer.Type != player.Type)
                        return true;
                            
                    break;
            }
        
            return false;
        }

        #endregion
        
        #region Other

        public static int GetArrayRandomNumber(int arrayCount, int rangeScale)
        {
            var step = rangeScale / arrayCount;

            var random = Random.Range(0, rangeScale);

            for (var i = 0; i < arrayCount; i++)
            {
                var currentStep = step * (i + 1);

                if (random < currentStep)
                    return i;
            }
            
            return 0;
        }
        
        public static void SetMaxValues(int currentValue, ref int value)
        {
            if (currentValue > value)
            {
                value = currentValue;
            }
        }
        
        public static void SetMinValues(int currentValue, ref int value)
        {
            if (currentValue < value)
            {
                value = currentValue;
            }
        }

        #endregion
    }
}
