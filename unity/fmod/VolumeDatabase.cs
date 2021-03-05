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
    [CreateAssetMenu(fileName = "VolumeDatabase", menuName = "Double Shot/Volume Database", order = 1)]
    public class VolumeDatabase : ScriptableObject
    {
        [Header("FMOD Bus IDs")]
        public string MasterBusID = "Bus:/";
        public string MusicBusID;
        public string AmbienceBusID;
        public string SfxBusID;

        [Header("Volume Status")]
        public float MasterVolume = 1;
        public float MusicVolume = 1;
        public float AmbienceVolume = 1;
        public float SfxVolume = 1;
        
        [Header("Mute Status")]
        public bool MasterMute;
        public bool MusicMute;
        public bool AmbienceMute;
        public bool SfxMute;

    } 
}
