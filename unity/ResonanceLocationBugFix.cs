/*
        ------------------------------------------------------------------------------------------------------------------------------
        ------------------------------------------------------------------------------------------------------------------------------
                                                                                           
             ((((                                                                          
            ((((     _______       _______.     ___                                        
             ))))   |       \     /       |    /   \                                       
          _ .---.   |  .--.  |   |   (----`   /  ^  \                                      
         ( |`---'|  |  |  |  |    \   \      /  /_\  \    _                                
          \|     |  |  '--'  |.----)   |    /  _____  \  /   _   _|  _        _  ._ |   _  
          : .___, : |_______/ |_______/    /__/     \__\ \_ (_) (_| (/_ \/\/ (_) |  |< _>  
           `-----'                                                                         
                                                                                           
        ------------------------------------------------------------------------------------------------------------------------------
        ------------------------------------------------------------------------------------------------------------------------------
*/

// Google Resonance Audio Location Bug Fix
// by Made Indrayana - Double Shot Audio
// Location fix by forcing an AudioSource to continually stop and play to force localization update on Google Resonance

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResonanceLocationBugFix : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSource;

	void Start () {

        InvokeRepeating("EmptyLoop", 0.1f, 0.1f);
	}
	
	void EmptyLoop()
    {
        audioSource.Stop();
        audioSource.Play();

        //Other way to trigger is:
        //audioSource.enabled = false;
        //audioSource.enabled = true;
    }

}
