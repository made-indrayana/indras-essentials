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

namespace DoubleShot.Utils
{
    public static class AudioFader
    {
        public static bool isFading
        {
            get { return (isFadingIn || isFadingOut); }
        }

        private static bool isFadingIn = false;
        private static bool isFadingOut = false;

        /// <summary>Coroutine for audio fade in/out. fadeTime is true to real seconds.
        /// You can use as many AudioSources as possible in one execution, useful for e.g. fading in/out a group of ambisonics sources.
        /// </summary>
        public static IEnumerator Fade(Direction direction, float fadeTime, params AudioSource[] audioSources)
        {
            // IMPORTANT FOR isFading CHECK!! DO NOT REMOVE
            yield return null;

            isFadingIn = (direction == Direction.In) ? true : isFadingIn;
            isFadingOut = (direction == Direction.Out) ? true : isFadingOut;

            float startVolume = 0f, endVolume = 1f;

            switch (direction)
            {
                case Direction.In:
                    startVolume = (audioSources[0].volume > 0.1f) ? 0f : audioSources[0].volume;
                    endVolume = 1f;
                    foreach (AudioSource a in audioSources)
                    {
                        a.volume = 0f;
                        a.Play();
                    }
                    break;

                case Direction.Out:
                    startVolume = (audioSources[0].volume > 0.9f) ? 1f : audioSources[0].volume;
                    endVolume = 0f;
                    break;
            }

            for (float f = 0; f <= fadeTime; f += Time.deltaTime)
            {
                foreach (AudioSource a in audioSources)
                {
                    a.volume = Mathf.Lerp(startVolume, endVolume, f / fadeTime);
                }

                yield return null;

            }

            isFadingIn = (direction == Direction.In) ? false : isFadingIn;
            isFadingOut = (direction == Direction.Out) ? false : isFadingOut;

            if (direction == Direction.Out && !isFading)
            {
                foreach (AudioSource a in audioSources) { a.Stop(); a.clip = null; }
            }
        }
    }
}