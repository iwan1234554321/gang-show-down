using System;
using UnityEngine;
using UnityEngine.UI;

namespace Notteam.GangShowDown.Logic
{
    [Serializable]
    public struct UIProgressGraphicData
    {
        public string key;
        [Range(0, 1)]
        public float progress;
        public Image graphic;
    }
    
    [ExecuteAlways]
    public class UIProgressBar : MonoBehaviour
    {
        [SerializeField] private UIProgressGraphicData[] progressCollection = Array.Empty<UIProgressGraphicData>();

        public UIProgressGraphicData[] ProgressCollection => progressCollection;
        
        private void Update()
        {
            for (var i = 0; i < progressCollection.Length; i++)
            {
                if (progressCollection[i].graphic)
                    progressCollection[i].graphic.fillAmount = progressCollection[i].progress;
            }
        }
        
        public int GetIndexProgress(string keyValue)
        {
            var index = 0;

            for (var i = 0; i < progressCollection.Length; i++)
            {
                if (progressCollection[i].key == keyValue)
                {
                    index = i;
                    
                    break;
                }
            }

            return index;
        }
    }
}
