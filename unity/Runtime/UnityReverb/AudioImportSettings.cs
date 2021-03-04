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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class AudioImportSetter
{
    [MenuItem("Assets/Double Shot/Audio Import Settings/Random Ambience", false, 1000)]
    private static void RandomAmbience()
    {
        AudioClip[] clip = Selection.GetFiltered<AudioClip>(SelectionMode.DeepAssets);

        AssetDatabase.StartAssetEditing();
        {
            int guidCount = clip.Length;
            for (int i = 0; i < guidCount; ++i)
            {
                string assetPath = AssetDatabase.GetAssetPath(clip[i]);
                EditorUtility.DisplayProgressBar(string.Format("Setting Import Settings [{0} / {1}]", i, guidCount), assetPath, i / (float)guidCount);

                AudioImporter importer = (AudioImporter)AudioImporter.GetAtPath(assetPath);
                if (importer != null)
                {
                    bool changed = false;

                    AudioImporterSampleSettings settings = importer.defaultSampleSettings;
                    if (changed |= (settings.loadType != AudioClipLoadType.CompressedInMemory))
                        settings.loadType = AudioClipLoadType.CompressedInMemory;

                    if (changed |= (settings.compressionFormat != AudioCompressionFormat.Vorbis))
                        settings.compressionFormat = AudioCompressionFormat.Vorbis;

                    if (changed |= (settings.quality != 0.7f))
                        settings.quality = 0.7f;

                    if (changed |= (settings.sampleRateSetting != AudioSampleRateSetting.PreserveSampleRate))
                        settings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;

                    if (changed |= (importer.preloadAudioData != false))
                        importer.preloadAudioData = false;

                    if (changed |= (importer.loadInBackground != true))
                        importer.loadInBackground = true;


                    if (changed == true)
                    {
                        importer.defaultSampleSettings = settings;
                        importer.SaveAndReimport();
                    }
                }
            }
            EditorUtility.ClearProgressBar();
        }
        AssetDatabase.StopAssetEditing();

        AssetDatabase.SaveAssets();
    }


    [MenuItem("Assets/Double Shot/Audio Import Settings/Comp Vorbis 70", false, 2000)]
    private static void CompVorbis70()
    {
        AudioClip[] clip = Selection.GetFiltered<AudioClip>(SelectionMode.DeepAssets);

        AssetDatabase.StartAssetEditing();
        {
            int guidCount = clip.Length;
            for (int i = 0; i < guidCount; ++i)
            {
                string assetPath = AssetDatabase.GetAssetPath(clip[i]);
                EditorUtility.DisplayProgressBar(string.Format("Setting Import Settings [{0} / {1}]", i, guidCount), assetPath, i / (float)guidCount);

                AudioImporter importer = (AudioImporter)AudioImporter.GetAtPath(assetPath);
                if (importer != null)
                {
                    bool changed = false;

                    AudioImporterSampleSettings settings = importer.defaultSampleSettings;
                    if (changed |= (settings.loadType != AudioClipLoadType.CompressedInMemory))
                        settings.loadType = AudioClipLoadType.CompressedInMemory;

                    if (changed |= (settings.compressionFormat != AudioCompressionFormat.Vorbis))
                        settings.compressionFormat = AudioCompressionFormat.Vorbis;

                    if (changed |= (settings.quality != 0.7f))
                        settings.quality = 0.7f;

                    if (changed |= (settings.sampleRateSetting != AudioSampleRateSetting.PreserveSampleRate))
                        settings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;

                    if (changed == true)
                    {
                        importer.defaultSampleSettings = settings;
                        importer.SaveAndReimport();
                    }
                }
            }
            EditorUtility.ClearProgressBar();
        }
        AssetDatabase.StopAssetEditing();

        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Double Shot/Audio Import Settings/Decomp PCM", false, 2000)]
    private static void DecompPCM()
    {
        AudioClip[] clip = Selection.GetFiltered<AudioClip>(SelectionMode.DeepAssets);

        AssetDatabase.StartAssetEditing();
        {
            int guidCount = clip.Length;
            for (int i = 0; i < guidCount; ++i)
            {
                string assetPath = AssetDatabase.GetAssetPath(clip[i]);
                EditorUtility.DisplayProgressBar(string.Format("Setting Import Settings [{0} / {1}]", i, guidCount), assetPath, i / (float)guidCount);

                AudioImporter importer = (AudioImporter)AudioImporter.GetAtPath(assetPath);
                if (importer != null)
                {
                    bool changed = false;

                    AudioImporterSampleSettings settings = importer.defaultSampleSettings;
                    if (changed |= (settings.loadType != AudioClipLoadType.DecompressOnLoad))
                        settings.loadType = AudioClipLoadType.DecompressOnLoad;

                    if (changed |= (settings.compressionFormat != AudioCompressionFormat.PCM))
                        settings.compressionFormat = AudioCompressionFormat.PCM;

                    if (changed |= (settings.quality != 0.7f))
                        settings.quality = 0.7f;

                    if (changed |= (settings.sampleRateSetting != AudioSampleRateSetting.PreserveSampleRate))
                        settings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;

                    if (changed == true)
                    {
                        importer.defaultSampleSettings = settings;
                        importer.SaveAndReimport();
                    }
                }
            }
            EditorUtility.ClearProgressBar();
        }
        AssetDatabase.StopAssetEditing();

        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Double Shot/Audio Import Settings/Comp ADPCM", false, 2000)]
    private static void CompADPCM()
    {
        AudioClip[] clip = Selection.GetFiltered<AudioClip>(SelectionMode.DeepAssets);

        AssetDatabase.StartAssetEditing();
        {
            int guidCount = clip.Length;
            for (int i = 0; i < guidCount; ++i)
            {
                string assetPath = AssetDatabase.GetAssetPath(clip[i]);
                EditorUtility.DisplayProgressBar(string.Format("Setting Import Settings [{0} / {1}]", i, guidCount), assetPath, i / (float)guidCount);

                AudioImporter importer = (AudioImporter)AudioImporter.GetAtPath(assetPath);
                if (importer != null)
                {
                    bool changed = false;

                    AudioImporterSampleSettings settings = importer.defaultSampleSettings;
                    if (changed |= (settings.loadType != AudioClipLoadType.CompressedInMemory))
                        settings.loadType = AudioClipLoadType.CompressedInMemory;

                    if (changed |= (settings.compressionFormat != AudioCompressionFormat.ADPCM))
                        settings.compressionFormat = AudioCompressionFormat.ADPCM;

                    if (changed |= (settings.sampleRateSetting != AudioSampleRateSetting.PreserveSampleRate))
                        settings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;

                    if (changed == true)
                    {
                        importer.defaultSampleSettings = settings;
                        importer.SaveAndReimport();
                    }
                }
            }
            EditorUtility.ClearProgressBar();
        }
        AssetDatabase.StopAssetEditing();

        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Double Shot/Audio Import Settings/Clear 96kHz and set to 44kHz", false, 3000)]
    private static void ClearHighSampleRate()
    {
        AudioClip[] clip = Selection.GetFiltered<AudioClip>(SelectionMode.DeepAssets);

        AssetDatabase.StartAssetEditing();
        {
            int guidCount = clip.Length;
            for (int i = 0; i < guidCount; ++i)
            {
                string assetPath = AssetDatabase.GetAssetPath(clip[i]);
                EditorUtility.DisplayProgressBar(string.Format("Setting Import Settings [{0} / {1}]", i, guidCount), assetPath, i / (float)guidCount);

                AudioImporter importer = (AudioImporter)AudioImporter.GetAtPath(assetPath);
                if (importer != null)
                {
                    bool changed = false;

                    AudioImporterSampleSettings settings = importer.defaultSampleSettings;

                    if (changed |= clip[i].frequency > 48000)
                    {
                        settings.sampleRateSetting = AudioSampleRateSetting.OverrideSampleRate;
                        settings.sampleRateOverride = 48000;
                    }


                    if (changed == true)
                    {
                        importer.defaultSampleSettings = settings;
                        importer.SaveAndReimport();
                    }
                }
            }
            EditorUtility.ClearProgressBar();
        }
        AssetDatabase.StopAssetEditing();

        AssetDatabase.SaveAssets();
    }
}