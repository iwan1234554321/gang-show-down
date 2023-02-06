using System;
using System.Collections.Generic;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public enum ActionType
    {
        Attack,
        Defense,
        Poison,
        Health
    }
    
    public enum ActionUseType
    {
        OnlyMe,
        MeAndOther,
        MeAndMyTeam,
        MeAndOtherTeam,
        OnlyOther,
        OnlyMyTeam,
        OnlyOtherTeam,
    }

    public enum ActionFunctionType
    {
        None,
        
        AddHealth,
        RemoveHealth,
        
        AddDefence,
        RemoveDefence,
        
        SetPoisoned,
        SetDefended,
        
        RemoveAction,
    }

    [Serializable]
    public struct ActionFunction
    {
        public ActionFunctionType type;
        
        public int updateCount;
        public int healthModifer;
        public int defenceModifer;
        
        public ActionType[] addOrRemoveAction;

        public ActionFunction(ActionFunction function)
        {
            type = function.type;
            
            updateCount = function.updateCount;
            healthModifer = function.healthModifer;
            defenceModifer = function.defenceModifer;
            
            addOrRemoveAction = new ActionType[function.addOrRemoveAction.Length];
            
            var localAddOrRemoveIndex = 0;
                
            foreach (var addOrRemove in function.addOrRemoveAction)
            {
                addOrRemoveAction[localAddOrRemoveIndex] = addOrRemove;
                    
                localAddOrRemoveIndex++;
            }
        }
    }
    
    [Serializable]
    public struct ActionData
    {
        public ActionType type;
        public ActionUseType useType;
        [Space]
        public Sprite icon;
        [Space]
        public Sprite iconModifer;
        public Color iconModiferColor;
        [Space]
        public ActionTimeline actionTimeline;
        public ActionTimeline skipActionTimeline;
        [Space]
        public List<ActionFunction> functions;

        public ActionData(ActionData data)
        {
            type = data.type;
            useType = data.useType;
            icon = data.icon;
            
            iconModifer = data.iconModifer;
            iconModiferColor = data.iconModiferColor;
            
            actionTimeline = data.actionTimeline;
            skipActionTimeline = data.skipActionTimeline;

            functions = new List<ActionFunction>();

            foreach (var function in data.functions)
                functions.Add(new ActionFunction(function));
        }
    }
}
