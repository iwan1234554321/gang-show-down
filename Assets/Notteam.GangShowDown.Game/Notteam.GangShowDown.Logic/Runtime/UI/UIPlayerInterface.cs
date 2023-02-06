using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Notteam.GangShowDown.Logic
{
    [DefaultExecutionOrder(2)]
    public class UIPlayerInterface : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int maxDefence = 10;
        [SerializeField] private Image playerAvatarImage;
        [SerializeField] private Image playerModifer;
        [SerializeField] private TMP_Text playerNameText;
        [SerializeField] private UIProgressBar playerProgressBar;
        [SerializeField] private UIProgressText playerProgressText;
        [SerializeField] private GameObject endTurnButton;

        private int _progressBarHealthIndex;
        private int _progressBarDefenceIndex;
        private int _progressTextHealthIndex;
        private int _progressTextDefenceIndex;
        
        private MoveManager _moveManager;

        private void PreparePlayer(Player player)
        {
            Utils.SetMaxValues(player.Data.health, ref maxHealth);
            Utils.SetMaxValues(player.Data.defence, ref maxDefence);
                
            playerProgressText.ProgressCollection[_progressBarHealthIndex].maxValue = maxHealth;
            playerProgressText.ProgressCollection[_progressTextDefenceIndex].maxValue = maxDefence;
                
            playerAvatarImage.sprite = player.Data.avatar;
            playerNameText.text = player.Data.name;
            playerModifer.sprite = player.Data.modifer;
            playerModifer.color = player.Data.modiferColor;
                
            playerProgressBar.ProgressCollection[_progressBarHealthIndex].progress = (1.0f / maxHealth) * player.Data.health;
            playerProgressBar.ProgressCollection[_progressBarDefenceIndex].progress = (1.0f / maxDefence) * player.Data.defence;
            playerProgressText.ProgressCollection[_progressTextHealthIndex].progress = (1.0f / maxHealth) * player.Data.health;
            playerProgressText.ProgressCollection[_progressTextDefenceIndex].progress = (1.0f / maxDefence) * player.Data.defence;
                
            endTurnButton.SetActive(player.Data.isMain);
        }
        
        private void Awake()
        {
            _moveManager = FindObjectOfType<MoveManager>();

            _moveManager.OnPreparedPlayer += PreparePlayer;
            
            _progressBarHealthIndex = playerProgressBar.GetIndexProgress("Health");
            _progressBarDefenceIndex = playerProgressBar.GetIndexProgress("Defence");
            
            _progressTextHealthIndex = playerProgressText.GetIndexProgress("Health");
            _progressTextDefenceIndex = playerProgressText.GetIndexProgress("Defence");
        }
    }
}
