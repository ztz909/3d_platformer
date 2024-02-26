using UnityEngine;
using UnityEditor;
using static System.IO.Path;
using static System.IO.Directory;
using static UnityEditor.AssetDatabase;
public class Setup
{
    [MenuItem("Tools/Setup/Create Default Folders")]
    public static void CreateDefaultFolders()
    {
        Folders.CreateDefault("_Project", "Animations", "Images", "Materials","Music", "Prefabs", "ScriptableObjects", "Scripts", "Textures");
        Refresh();
    }

    [MenuItem("Tools/Setup/Import My Favorite Assets")]
    public static void ImportMyFavoriteAssets()
    {
        Assets.ImportAsset("DOTween HOTween v2.unitypackage", "Demigiant/Editor ExtensionsAnimation");
        Assets.ImportAsset("Midgard Textures.unitypackage", "LaFinca/Textures MaterialsNature");
    }

    static class Folders
    {
        public static void CreateDefault(string root, params string[] folders)
        {
            var fullpath = Combine(Application.dataPath, root);
            foreach (var folder in folders)
            {
                var path = Combine(fullpath, folder);
                if (!Exists(path))
                {
                    CreateDirectory(path);
                }
            }
        }
    }
    public static class Assets
    {
        public static void ImportAsset(string asset, string subfolder, string folder = "C:/Users/Zacharias/AppData/Roaming/Unity/Asset Store-5.x")
        {
            AssetDatabase.ImportPackage(Combine(folder, subfolder, asset), false);
        }
    }
}