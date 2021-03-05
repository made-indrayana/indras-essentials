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

using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace DoubleShot
{
    public class MusicAmbienceManager : MonoBehaviour
    {
        private static MusicAmbienceManager instance = null;
        public static MusicAmbienceManager Instance
        {
            get { return instance; }
        }

        private FMOD.Studio.EventInstance music;
        private FMOD.Studio.EventInstance ambience;
#pragma warning disable 0649
        [SerializeField] private MusicAmbienceData[] database;

        [Header("Fade Times (max 10 seconds!)")]
        [Tooltip("Fade ins must be defined in FMOD as \"Fade In\" Parameter, otherwise they will be ignored.")]
        [SerializeField] private float fadeInTimeFirstTime;
        [SerializeField] private float fadeInTimeCrossfade;

        [Header("Runtime Debug")]
        [SerializeField, ReadOnly] private string currentMusicEvent;
        [SerializeField, ReadOnly] private string currentAmbienceEvent;
        [SerializeField, ReadOnly] private bool firstLoad = true;
        [SerializeField, ReadOnly] private bool hasBeenCalled = false;
#pragma warning restore 0649
        public FMOD.Studio.EventDescription description;

        #region Singleton
        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this.gameObject);
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Play music in FMOD with defined Fade Time.
        /// </summary>
        /// <param name="musicEvent"></param>
        /// <param name="fadeTime"></param>
        public void PlayMusic(string musicEvent, float fadeTime)
        {
            currentMusicEvent = musicEvent;
            music = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
            music.setParameterByName("Fade In", fadeTime);
            if (FMODUtils.IsInstance3D(music))
                music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            music.start();
        }

        /// <summary>
        /// Play ambience in FMOD with defined Fade Time.
        /// </summary>
        /// <param name="ambienceEvent"></param>
        /// <param name="fadeTime"></param>
        public void PlayAmbience(string ambienceEvent, float fadeTime)
        {
            currentAmbienceEvent = ambienceEvent;
            ambience = FMODUnity.RuntimeManager.CreateInstance(ambienceEvent);
            ambience.setParameterByName("Fade In", fadeTime);
            if (FMODUtils.IsInstance3D(ambience))
                ambience.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            ambience.start();
        }

        /// <summary>
        /// Stop running music and ambience with fade out.
        /// </summary>
        public void StopMusicAmbience()
        {
            music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            music.release();
            ambience.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambience.release();
            currentAmbienceEvent = null;
            currentMusicEvent = null;
        }

        /// <summary>
        /// Stop running music with fade out.
        /// </summary>
        public void StopMusic()
        {
            music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            music.release();
            currentMusicEvent = null;
        }

        /// <summary>
        /// Stop running ambience with fade out.
        /// </summary>
        public void StopAmbience()
        {
            ambience.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambience.release();
            currentAmbienceEvent = null;
        }

        /// <summary>
        /// Play music and ambience in the defined scene.<br/>
        /// <para>This is very useful to stop the music with fade out and load the next scene's music and ambience
        /// before the actual scene loading happens.</para>
        /// </summary>
        /// <param name="nextSceneName"></param>
        public void PlayMusicAmbienceInScene(string nextSceneName)
        {
            foreach (MusicAmbienceData data in database)
            {
                if (data.name.Equals(nextSceneName))
                {
                    hasBeenCalled = true;

                    if (firstLoad)
                    {
                        firstLoad = !firstLoad;
                        GetSceneMusicAmbience(data);
                        if(!string.IsNullOrEmpty(currentMusicEvent))
                            PlayMusic(currentMusicEvent, fadeInTimeFirstTime);
                        if(!string.IsNullOrEmpty(currentAmbienceEvent))
                            PlayAmbience(currentAmbienceEvent, fadeInTimeFirstTime);
                    }

                    else
                    {
                        var nextMusicEvent = data.music;
                        var nextAmbienceEvent = data.ambience;

                        if (currentMusicEvent != nextMusicEvent)
                        {
                            StopMusic();
                            if(!string.IsNullOrEmpty(nextMusicEvent))
                                PlayMusic(nextMusicEvent, fadeInTimeCrossfade);
                        }

                        if (currentAmbienceEvent != nextAmbienceEvent)
                        {
                            StopAmbience();
                            if (!string.IsNullOrEmpty(nextAmbienceEvent)) 
                                PlayAmbience(nextAmbienceEvent, fadeInTimeCrossfade);
                        }

                    }

                    hasBeenCalled = false;

                }
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Updates the currentMusicEvent and currentAmbienceEvent to the current scene's.<br/>
        /// Only useful for the first scene load.
        /// </summary>
        /// <param name="data"></param>
        private void GetSceneMusicAmbience(MusicAmbienceData data)
        {
            var music = data.music;
            var ambience = data.ambience;

            if (string.IsNullOrEmpty(music))
                Debug.Log("No music found in this scene, not playing anything");
            else
            {
                currentMusicEvent = music;
                //Debug.Log("Current Music Event is: " + currentMusicEvent);
            }

            if (string.IsNullOrEmpty(ambience))
                Debug.Log("No ambience found in this scene, not playing anything");
            else
            {
                currentAmbienceEvent = ambience;
                //Debug.Log("Current Ambience Event is: " + currentAmbienceEvent);
            }

        }
        #endregion

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log(scene.name);   
            
            if (SceneManager.GetActiveScene().name == "LoadingScreen")
            {
                return;
            }

            else
            {
                switch (hasBeenCalled)
                {
                    case true:
                        hasBeenCalled = false;
                        break;
                    case false:
                        PlayMusicAmbienceInScene(SceneManager.GetActiveScene().name);
                        break;

                }

            }
        }

        private void OnDestroy()
        {
            music.release();
            ambience.release();

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    } 
}
