using System;
using TMPro;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    [Serializable]
    public struct UIProgressTextData
    {
        public string key;
        [Range(0, 1)]
        public float progress;
        public bool discrete;
        public float minValue;
        public float maxValue;
        [Space]
        public string prefix;
        [Space]
        public TMP_Text text;
    }
    
    [ExecuteAlways]
    public class UIProgressText : MonoBehaviour
    {
        [SerializeField] private UIProgressTextData[] progressCollection = Array.Empty<UIProgressTextData>();

        public UIProgressTextData[] ProgressCollection => progressCollection;
        
        private void Update()
        {
            for (var i = 0; i < progressCollection.Length; i++)
            {
                var progressData = progressCollection[i];
                
                if (progressData.text)
                {
                    var lerp = Mathf.Lerp(progressData.minValue, progressData.maxValue, progressData.progress);
                    
                    progressCollection[i].text.text = $"{progressData.prefix} {(progressData.discrete ? (int)lerp : lerp)}";
                }
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
