using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class EditorSaveDataCleaner
    {
        private static string SavePath => Application.persistentDataPath + @"\LevelSaveData.txt";

        [MenuItem("Edit/ClearSaveData")]
        public static void ClearAllSaveData()
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }
        }

    }
}
