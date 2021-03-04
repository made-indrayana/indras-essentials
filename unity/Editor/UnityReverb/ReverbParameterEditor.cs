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

// WIP!

using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
using CrazyBunch.Larry;

namespace DoubleShotAudio
{
    [CustomEditor(typeof(ReverbParameter))]
    public class ReverbParameterEditor : Editor
    {
        private readonly static string[] reverbsToControl = new string[] { "Master", "Random Ambience" };
        private bool showAdvancedParam;

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            // Set target to original script
            ReverbParameter mainScript = (ReverbParameter)target;
            if (mainScript == null)
                return;

            // Pointer
            var audioMixer = AudioSystem.Mixer;

            #region Choosing which Reverb to control
            EditorGUILayout.LabelField("Reverb To Control", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Reverb");

                EditorGUI.BeginChangeCheck();
                {
                    mainScript.index = EditorGUILayout.Popup(mainScript.index, ReverbParameterEditor.reverbsToControl, EditorStyles.popup);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    mainScript.SetStringsAndClamping();
                }
            }
            EditorGUILayout.EndHorizontal();
            #endregion

            EditorGUILayout.LabelField("Reverb Preset", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Preset");

                EditorGUI.BeginChangeCheck();
                {
                    mainScript.reverbPreset = (ReverbPreset)EditorGUILayout.ObjectField(mainScript.reverbPreset, typeof(ReverbPreset), false);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(mainScript.reverbPreset);

                    if (mainScript.reverbPreset == null)
                    {
                        mainScript.resetParameterToZero();
                    }
                    else
                    {
                        mainScript.GetPresetValue(mainScript.reverbPreset);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            if (mainScript.reverbPreset == null)
            {
                EditorGUILayout.HelpBox("Assign reverb preset to load values.", MessageType.Info);
                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Main Parameters", EditorStyles.boldLabel);

            //Only turn on dry control for Random Ambience
            if(mainScript.index == 1) { mainScript.raDryLevel = EditorGUILayout.Slider("Dry Level", mainScript.raDryLevel, -10000f, 0f); }

            mainScript.room = EditorGUILayout.Slider("Room", mainScript.room, -10000f, 0f);
            mainScript.reflections = EditorGUILayout.Slider("Reflections", mainScript.reflections, -10000f, 1000f);
            mainScript.reverb = EditorGUILayout.Slider("Reverb", mainScript.reverb, -10000f, 2000f);

            EditorGUILayout.Space();


            showAdvancedParam = EditorGUILayout.Foldout(showAdvancedParam, "Advanced Parameter");
            if (showAdvancedParam)
            {
                // Controlling public variables in script to update in realtime
                mainScript.roomHF = EditorGUILayout.Slider("Room HF", mainScript.roomHF, -10000f, 0f);
                mainScript.decayTime = EditorGUILayout.Slider("Decay Time", mainScript.decayTime, 0.1f, 20f);
                mainScript.decayHFRatio = EditorGUILayout.Slider("Decay HF Ratio", mainScript.decayHFRatio, 0.1f, 2f);
                mainScript.reflectDelay = EditorGUILayout.Slider("Reflections Delay", mainScript.reflectDelay, 0f, 0.3f);
                mainScript.reverbDelay = EditorGUILayout.Slider("Reverb Delay", mainScript.reverbDelay, 0f, 0.1f);
                mainScript.diffusion = EditorGUILayout.Slider("Diffusion", mainScript.diffusion, 0f, 100f);
                mainScript.density = EditorGUILayout.Slider("Density", mainScript.density, 0f, 100f);
                mainScript.hFReference = EditorGUILayout.Slider("HF Reference", mainScript.hFReference, 20f, 20000f);
                mainScript.roomLF = EditorGUILayout.Slider("Room LF", mainScript.roomLF, -10000f, 0f);
                mainScript.lFReference = EditorGUILayout.Slider("LF Reference", mainScript.lFReference, 20f, 1000f);
            }
            

            if (audioMixer != null)
            {
                // Pass public variables to AudioMixer
                mainScript.SetReverbValueToAudioMixer();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Reset parameter to zero"))
            {
                mainScript.resetParameterToZero();
            }

            if (GUILayout.Button("Reset parameter to preset value"))
            {
                mainScript.GetPresetValue(mainScript.reverbPreset);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Preset Management", EditorStyles.boldLabel);


            if (GUILayout.Button("Save value as a new preset"))
            {
                this.CreateReverbPresetInstance(mainScript);
            }
            if (GUILayout.Button("Overwrite current preset"))
            {
                if (EditorUtility.DisplayDialog("Warning!", "Are you sure you want to overwrite the preset " + mainScript.reverbPreset.name + "?", "Yes", "No"))
                {
                    mainScript.OverwritePreset(mainScript.reverbPreset);
                }
            }

            if (GUI.changed)
            {
                if(!EditorApplication.isPlaying)
                    UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
            }
        }

        public void CreateReverbPresetInstance(ReverbParameter parameter)
        {
            ReverbPreset presetInstance = ScriptableObject.CreateInstance<ReverbPreset>();
            parameter.OverwritePreset(presetInstance);

            string path = "Assets/Project_Larry/Audio/ReverbPresets";
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New" + typeof(ReverbPreset).ToString() + ".asset");

            AssetDatabase.CreateAsset(presetInstance, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = presetInstance;

            parameter.reverbPreset = presetInstance;
        }

        public void SearchForReverbPreset()
        {
            string[] guids;

            // search for a ScriptObject called ScriptObj
            guids = AssetDatabase.FindAssets("t:ReverbPreset");
            foreach (string guid in guids)
            {
                Debug.Log("ReverbPreset: " + AssetDatabase.GUIDToAssetPath(guid));
            }
        }


    }
}