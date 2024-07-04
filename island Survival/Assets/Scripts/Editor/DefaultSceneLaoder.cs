#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// Unity i her başlattığımızda önce Zero yu yükler, sonra seçili olan sahneyi de ekleyerek çalışmaya devam eder 
/// </summary>
[InitializeOnLoad]
public static class DefaultSceneLoader
{
    public const string LAST_SELECTED_SCENE_KEY = "LastSelectedScene";
    
    static DefaultSceneLoader()
    {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    static void LoadDefaultScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            var lastname = EditorSceneManager.GetActiveScene().name;
            // Build settings te yoksa
          
            EditorSceneManager.LoadScene(lastname);
        }
    }

    static bool IsSceneInBuild(string sceneName)
    {
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.path.Contains(sceneName))
            {
                return true;
            }
        }

        return false;
    }
}
#endif