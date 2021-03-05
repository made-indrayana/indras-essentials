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

namespace DoubleShot
{
    public class VolumeManager : MonoBehaviour
    {
        private static VolumeManager instance = null;
        public static VolumeManager Instance
        {
            get { return instance; }
        }

        public VolumeDatabase Database;

        FMOD.Studio.Bus masterBus, musicBus, ambienceBus, sfxBus;

        public float MasterBusVolume
        {
            get { return (Database != null) ? Database.MasterVolume : 0; }
            set
            {
                if (Database.MasterVolume == value || Database == null) return;
                Database.MasterVolume = value;
                masterBus.setVolume(value);
            }
        }
        public float MusicBusVolume
        {
            get { return (Database != null) ? Database.MusicVolume : 0; }
            set
            {
                if (Database.MusicVolume == value || Database == null) return;
                Database.MusicVolume = value;
                musicBus.setVolume(value);
            }
        }
        public float AmbienceBusVolume
        {
            get { return (Database != null) ? Database.AmbienceVolume : 0; }
            set
            {
                if (Database.AmbienceVolume == value || Database == null) return;
                Database.AmbienceVolume = value;
                ambienceBus.setVolume(value);
            }
        }
        public float SfxBusVolume
        {
            get { return (Database != null) ? Database.SfxVolume : 0; }
            set
            {
                if (Database.SfxVolume == value || Database == null) return;
                Database.SfxVolume = value;
                sfxBus.setVolume(value);
            }
        }
        public bool MasterMute
        {
            get
            {
                if (Database != null)
                {
                    return Database.MasterMute;
                }
                else return false;
            }
            set
            {
                if (Database.MasterMute == value || Database == null) return;

                Database.MasterMute = value;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.audioMasterMute = value;
#else
                        FMODUnity.RuntimeManager.MuteAllEvents(value);
#endif
            }
        }
        public bool MusicMute
        {
            get
            {
                if (Database != null)
                {
                    return Database.MusicMute;
                }
                else return false;
            }
            set
            {
                if (Database.MusicMute == value || Database == null) return;
                Database.MusicMute = value;
                musicBus.setMute(value);
            }
        }
        public bool AmbienceMute
        {
            get
            {
                if (Database != null)
                {
                    return Database.AmbienceMute;
                }
                else return false;
            }
            set
            {
                if (Database.AmbienceMute == value || Database == null) return;
                Database.AmbienceMute = value;
                ambienceBus.setMute(value);
            }
        }
        public bool SfxMute
        {
            get
            {
                if (Database != null)
                {
                    return Database.SfxMute;
                }
                else return false;
            }
            set
            {
                if (Database.SfxMute == value || Database == null) return;
                Database.SfxMute = value;
                sfxBus.setMute(value);
            }
        }


        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this.gameObject);
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

        }

        private void Start()
        {
            masterBus = FMODUnity.RuntimeManager.GetBus(Database.MasterBusID);
            musicBus = FMODUnity.RuntimeManager.GetBus(Database.MusicBusID);
            ambienceBus = FMODUnity.RuntimeManager.GetBus(Database.AmbienceBusID);
            sfxBus = FMODUnity.RuntimeManager.GetBus(Database.SfxBusID);

            masterBus.setVolume(MasterBusVolume);
            musicBus.setVolume(MusicBusVolume);
            ambienceBus.setVolume(AmbienceBusVolume);
            sfxBus.setVolume(SfxBusVolume);


            musicBus.setMute(MusicMute);
            ambienceBus.setMute(AmbienceMute);
            sfxBus.setMute(SfxMute);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.audioMasterMute = MasterMute;
#else
                        FMODUnity.RuntimeManager.MuteAllEvents(MasterMute);
#endif
        }

        public void ResetVolume()
        {
            MasterBusVolume = 1f;
            MusicBusVolume = 1f;
            AmbienceBusVolume = 1f;
            SfxBusVolume = 1f;

            MusicMute = false;
            AmbienceMute = false;
            SfxMute = false;
            MasterMute = false;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.audioMasterMute = false;
#else
                        FMODUnity.RuntimeManager.MuteAllEvents(false);
#endif
        }
    } 
}
