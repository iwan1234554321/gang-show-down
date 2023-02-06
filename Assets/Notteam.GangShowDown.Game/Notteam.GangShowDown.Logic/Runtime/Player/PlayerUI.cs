using UnityEngine;
using UnityEngine.UI;

namespace Notteam.GangShowDown.Logic
{
    [RequireComponent(typeof(Player))]
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int maxDefence = 10;
        [SerializeField] private Image modificatorImage;
        [SerializeField] private UIProgressBar uiProgressBar;
        [SerializeField] private UIProgressText uiProgressText;

        private int _progressBarHealthIndex;
        private int _progressBarDefenceIndex;
        private int _progressTextHealthIndex;
        private int _progressTextDefenceIndex;
        
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();

            _progressBarHealthIndex = uiProgressBar.GetIndexProgress("Health");
            _progressBarDefenceIndex = uiProgressBar.GetIndexProgress("Defence");
            
            _progressTextHealthIndex = uiProgressText.GetIndexProgress("Health");
            _progressTextDefenceIndex = uiProgressText.GetIndexProgress("Defence");
        }

        private void Update()
        {
            Utils.SetMaxValues(_player.Data.health, ref maxHealth);
            Utils.SetMaxValues(_player.Data.defence, ref maxDefence);
            
            uiProgressText.ProgressCollection[_progressTextHealthIndex].maxValue = maxHealth;
            uiProgressText.ProgressCollection[_progressTextDefenceIndex].maxValue = maxHealth;
            
            uiProgressBar.ProgressCollection[_progressBarHealthIndex].progress = (1.0f / maxHealth) * _player.Data.health;
            uiProgressBar.ProgressCollection[_progressBarDefenceIndex].progress = (1.0f / maxDefence) * _player.Data.defence;
            uiProgressText.ProgressCollection[_progressTextHealthIndex].progress = (1.0f / maxHealth) * _player.Data.health;
            uiProgressText.ProgressCollection[_progressTextDefenceIndex].progress = (1.0f / maxDefence) * _player.Data.defence;

            modificatorImage.sprite = _player.Data.modifer;
            modificatorImage.color = _player.Data.modiferColor;
        }
    }
}
