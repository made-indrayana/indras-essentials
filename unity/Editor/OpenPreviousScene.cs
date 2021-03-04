/*
        ▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷
        ◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁
                                                                                           
             ((((                                                                          
            ((((     _______       _______.     ___                                        
             ))))   |       \     /       |    /   \                                       
          _ .---.   |  .--.  |   |   (----`   /  ^  \                                      
         ( |`---'|  |  |  |  |    \   \      /  /_\  \    _                                
          \|     |  |  '--'  |.----)   |    /  _____  \  /   _   _|  _        _  ._ |   _  
          : .___, : |_______/ |_______/    /__/     \__\ \_ (_) (_| (/_ \/\/ (_) |  |< _>  
           `-----'                                                                         
                                                                                           
        ▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷
        ◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁
*/

// Open Previous Scene
// by Made Indrayana - Double Shot Audio
// Shortcut for opening previous scene if you juggle around two scenes a lot.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class OpenPreviousScene
{
    [global::UnityEditor.MenuItem(
        "File/Open Previous Scene &2",
        false,
        154)]
    static void PreviousScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo(); // Safeguarding against switching scene without saving

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
