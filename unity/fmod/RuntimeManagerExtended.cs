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

// FMOD RuntimeManager.cs extension
// by Made Indrayana - Double Shot Audio
// Extended the class so you can pass (multiple) parameters with PlayOneShot and PlayOneShotAttached

using System;
using UnityEngine;

namespace FMODUnity
{
    public class RuntimeManagerExtended : RuntimeManager
    {
        #region Modified PlayOneShot() for adding more than one parameter with tuplets (string, float)
        /// <summary>
        /// Extended PlayOneShot() function to be able to use (multiple) parameters.<br />
        /// Example usage: FMODUnity.RuntimeManagerExtended.PlayOneShot("event:/CertainEvent", this.gameObject.position.transform, ("Parameter Name", value);
        /// </summary>
        /// <param name="path"></param>
        /// <param name="position"></param>
        /// <param name="parameters"></param>
        public static void PlayOneShot(string path, Vector3 position = new Vector3(), params (string name, float value)[] parameters)
        {
            try
            {
                PlayOneShot(PathToGUID(path), position, parameters);
            }
            catch (EventNotFoundException)
            {
                Debug.LogWarning("[FMOD] Event not found: " + path);
            }
        }

        /// <summary>
        /// Extended PlayOneShot() function to be able to use (multiple) parameters.<br />
        /// Example usage: FMODUnity.RuntimeManagerExtended.PlayOneShot(certainEventGuid, this.gameObject.position.transform, ("Parameter Name", value);
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="position"></param>
        /// <param name="parameters"></param>
        public static void PlayOneShot(Guid guid, Vector3 position = new Vector3(), params (string name, float value)[] parameters)
        {
            var instance = CreateInstance(guid);
            instance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
            foreach (var parameter in parameters)
            {
                instance.setParameterByName(parameter.name, parameter.value);
            }
            instance.start();
            instance.release();
        }
        #endregion

        #region Modified PlayOneShotAttached() for adding more than one parameter with tuplets (string, float)
        /// <summary>
        /// Extended PlayOneShotAttached() function to be able to use (multiple) parameters.<br />
        /// Example usage: FMODUnity.RuntimeManagerExtended.PlayOneShotAttached("event:/CertainEvent", this.gameObject, ("Parameter Name", value);
        /// /// </summary>
        /// <param name="path"></param>
        /// <param name="gameObject"></param>
        /// <param name="parameters"></param>
        public static void PlayOneShotAttached(string path, GameObject gameObject, params (string name, float value)[] parameters)
        {
            try
            {
                PlayOneShotAttached(PathToGUID(path), gameObject, parameters);
            }
            catch (EventNotFoundException)
            {
                Debug.LogWarning("[FMOD] Event not found: " + path);
            }
        }

        /// <summary>
        /// Extended PlayOneShotAttached() function to be able to use (multiple) parameters.<br />
        /// Example usage: FMODUnity.RuntimeManagerExtended.PlayOneShotAttached(certainEventGuid, this.gameObject, ("Parameter Name", value);
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="gameObject"></param>
        /// <param name="parameters"></param>
        public static void PlayOneShotAttached(Guid guid, GameObject gameObject, params (string name, float value)[] parameters)
        {
            var instance = CreateInstance(guid);
            AttachInstanceToGameObject(instance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
            foreach (var parameter in parameters)
            {
                instance.setParameterByName(parameter.name, parameter.value);
            }
            instance.start();
            instance.release();
        }
        #endregion
    }
}

