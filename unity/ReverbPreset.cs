using UnityEngine;

using UnityEngine.Audio;

namespace DoubleShotAudio
{
    [CreateAssetMenu(fileName = "NewReverbPreset.asset", menuName = "Double Shot Audio/Reverb Preset")]
    public class ReverbPreset : ScriptableObject
    {
        [Header("Reverb Preset Name")]
        public new string name;

        [Header("Reverb Parameters")]
        public float room;
        public float roomHF;
        public float roomLF;
        public float decayTime;
        public float decayHFRatio;
        public float reflections;
        public float reflectDelay;
        public float reverb;
        public float reverbDelay;
        public float hFReference;
        public float lFReference;
        public float diffusion;
        public float density;
    }
}