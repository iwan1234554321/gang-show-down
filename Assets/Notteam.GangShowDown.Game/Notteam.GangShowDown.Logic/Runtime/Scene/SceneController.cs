using UnityEngine;
using UnityEngine.SceneManagement;

namespace Notteam.GangShowDown.Logic
{
    [DefaultExecutionOrder(0)]
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private float timeFade = 0.5f;
        
        private string _loadSceneName = string.Empty;
        
        private Scene _currentScene;

        private UIFader _uiFader;

        private void LoadSceneInternal()
        {
            if (!string.IsNullOrEmpty(_loadSceneName))
                SceneManager.LoadScene(_loadSceneName);
        }
        
        private void Awake()
        {
            _uiFader = FindObjectOfType<UIFader>();
            
            _currentScene = SceneManager.GetActiveScene();

            _uiFader.OnFinalAnimation += LoadSceneInternal;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                LoadLevel(_currentScene.name);
        }

        public void LoadLevel(string nameScene)
        {
            _loadSceneName = nameScene;
            
            _uiFader.SwitchFade(true, timeFade);
        }
    }
}
