using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using CrazyBunch.Larry;

namespace DoubleShotAudio
{
    public class ReverbParameter : MonoBehaviour
    {
        private const float rmDryLevel = -10000f;
        public float raDryLevel = 0f;

        public int index = 0;

        private AudioMixer audioMixer
        {
            get { return AudioSystem.Mixer; }
        }

        [Header("Reverb Parameters")]
        public float room = -10000f;
        public float roomHF = -10000f;
        public float decayTime = 0.1f;
        public float decayHFRatio = 0.11f;
        public float reflections = -10000f;
        public float reflectDelay = 0f;
        public float reverb = -10000f;
        public float reverbDelay = 0f;
        public float diffusion = 0f;
        public float density = 0f;
        public float hFReference = 20f;
        public float roomLF = -10000f;
        public float lFReference = 20f;

        [Header("Strings to Reverb")]
        public string sRoom = "rmRoom";
        public string sRoomHF = "rmRoomHF";
        public string sDecayTime = "rmDecayTime";
        public string sDecayHFRatio = "rmDecayHFRatio";
        public string sReflections = "rmReflections";
        public string sReflectDelay = "rmReflectDelay";
        public string sReverb = "rmReverb";
        public string sReverbDelay = "rmReverbDelay";
        public string sDiffusion = "rmDiffusion";
        public string sDensity = "rmDensity";
        public string sHFReference = "rmHFReference";
        public string sRoomLF = "rmRoomLF";
        public string sLFReference = "rmLFReference";

        [Header("Scriptable Object")]
        [HideInInspector]
        public ReverbPreset reverbPreset;
        public ReverbPreset newReverbPreset;

        private void Awake()
        {
            if (audioMixer == null)
            {
                Debug.LogError("Audio mixer cannot be null to control reverb!");
            }
        }

        private void OnEnable()
        {

            SetStringsAndClamping();
            
            if (reverbPreset == null)
            {
                resetParameterToZero();
                SetReverbValueToAudioMixer();
                Debug.Log("Setting reverb value to zero...");

            }

            else
            {
                GetPresetValue(reverbPreset);
                SetReverbValueToAudioMixer();
                Debug.Log("Setting reverb value from preset...");

            }
        }

        public void resetParameterToZero()
        {
            room = -10000f;
            reflections = -10000f;
            reverb = -10000f;
            roomHF = -10000f;
            decayTime = 0.1f;
            decayHFRatio = 0.1f;
            reflectDelay = 0f;
            reverbDelay = 0f;
            diffusion = 0f;
            density = 0f;
            hFReference = 20f;
            roomLF = -10000f;
            lFReference = 20f;
        }

        public void SetStringsAndClamping()
        {
            if (index == 0)
            {
                audioMixer.SetFloat("rmDryLevel", rmDryLevel); // clamp dry level to -10000 for reverb master
                sRoom = "rmRoom";
                sRoomHF = "rmRoomHF";
                sDecayTime = "rmDecayTime";
                sDecayHFRatio = "rmDecayHFRatio";
                sReflections = "rmReflections";
                sReflectDelay = "rmReflectDelay";
                sReverb = "rmReverb";
                sReverbDelay = "rmReverbDelay";
                sDiffusion = "rmDiffusion";
                sDensity = "rmDensity";
                sHFReference = "rmHFReference";
                sRoomLF = "rmRoomLF";
                sLFReference = "rmLFReference";
            }

            else if (index == 1)
            {
                audioMixer.SetFloat("raDryLevel", raDryLevel); // clamp dry level to 0 for reverb ambience
                sRoom = "raRoom";
                sRoomHF = "raRoomHF";
                sDecayTime = "raDecayTime";
                sDecayHFRatio = "raDecayHFRatio";
                sReflections = "raReflections";
                sReflectDelay = "raReflectDelay";
                sReverb = "raReverb";
                sReverbDelay = "raReverbDelay";
                sDiffusion = "raDiffusion";
                sDensity = "raDensity";
                sHFReference = "raHFReference";
                sRoomLF = "raRoomLF";
                sLFReference = "raLFReference";
            }
        }
        
        public void GetPresetValue(ReverbPreset rp)
        {
            room = rp.room;
            roomHF = rp.roomHF;
            roomLF = rp.roomLF;
            decayTime = rp.decayTime;
            decayHFRatio = rp.decayHFRatio;
            reflections = rp.reflections;
            reflectDelay = rp.reflectDelay;
            reverb = rp.reverb;
            reverbDelay = rp.reverbDelay;
            hFReference = rp.hFReference;
            lFReference = rp.lFReference;
            diffusion = rp.diffusion;
            density = rp.density;
        }

        public void OverwritePreset(ReverbPreset rp)
        {
            rp.room = room;
            rp.roomHF = roomHF;
            rp.roomLF = roomLF;
            rp.decayTime = decayTime;
            rp.decayHFRatio = decayHFRatio;
            rp.reflections = reflections;
            rp.reflectDelay = reflectDelay;
            rp.reverb = reverb;
            rp.reverbDelay = reverbDelay;
            rp.hFReference = hFReference;
            rp.lFReference = lFReference;
            rp.diffusion = diffusion;
            rp.density = density;
        }

        public void SetReverbValueToAudioMixer()
        {
            if (index == 1) { audioMixer.SetFloat("raDryLevel", raDryLevel); }

            audioMixer.SetFloat(sRoom, room);
            audioMixer.SetFloat(sRoomHF, roomHF);
            audioMixer.SetFloat(sDecayTime, decayTime);
            audioMixer.SetFloat(sDecayHFRatio, decayHFRatio);
            audioMixer.SetFloat(sReflections, reflections);
            audioMixer.SetFloat(sReflectDelay, reflectDelay);
            audioMixer.SetFloat(sReverb, reverb);
            audioMixer.SetFloat(sReverbDelay, reverbDelay);
            audioMixer.SetFloat(sDiffusion, diffusion);
            audioMixer.SetFloat(sDensity, density);
            audioMixer.SetFloat(sHFReference, hFReference);
            audioMixer.SetFloat(sRoomLF, roomLF);
            audioMixer.SetFloat(sLFReference, lFReference);
        }
    }
}
