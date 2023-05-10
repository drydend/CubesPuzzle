using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public class SceneLoader
    {
        private const string GameSceneName = "GameScene";

        public void LoadGameScene()
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}
