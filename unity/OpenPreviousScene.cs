using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class OpenRecent
{
    [global::UnityEditor.MenuItem(
        "File/Open Previous Scene",
        false,
        154)]
    static void OpenPreviousScene()
    {
        if (!EditorPrefs.HasKey("LastScene"))
            return;
        EditorSceneManager.OpenScene(EditorPrefs.GetString("LastScene"));
    }

    static OpenRecent()
    {
        EditorSceneManager.sceneOpened += LogSceneOpen;
    }

    static void LogSceneOpen(Scene scene, OpenSceneMode _mode)
    {
        if (EditorPrefs.HasKey("CurrentScene"))
        {
            //do nothing if opening the same scene again.
            if (EditorPrefs.GetString("CurrentScene") == scene.path)
                return;
            EditorPrefs.SetString("LastScene", EditorPrefs.GetString("CurrentScene"));
        }

        EditorPrefs.SetString("CurrentScene", scene.path);
    }
}
