using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CallAudioSnapshot : MonoBehaviour {

    public AudioMixerSnapshot sceneSnapshot;
    public float transitionLength = 1f; // TODO remove when no complaints

    private void OnEnable()
    {
        //Debug.Log("Audio Snapshot " + sceneSnapshot + " is loaded.");
        if (sceneSnapshot != null)
            sceneSnapshot.TransitionTo(transitionLength);
    }
}
