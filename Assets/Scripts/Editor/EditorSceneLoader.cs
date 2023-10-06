#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public static class EditorSceneLoader
    {
        private const string PATH = "Assets/Scenes/";
        private const string SCENE_SUFFIX = ".unity";

        private const string BOOT_SCENE_NAME = "BootstrapScene";
        private const string GAME_SCENE_NAME = "GameScene";

        [MenuItem("Scenes/Load BootstrapScene")]
        public static void LoadBootScene() => OpenScene(GetScenePath(BOOT_SCENE_NAME));
        [MenuItem("Scenes/Load Game Scene")]
        public static void LoadGameScene() => OpenScene(GetScenePath(GAME_SCENE_NAME));

        private static void OpenScene(string path)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(path);
            }
        }

        private static string GetScenePath(string name) => PATH + name + SCENE_SUFFIX;
    }
}
#endif