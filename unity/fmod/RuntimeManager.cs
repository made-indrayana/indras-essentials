// Modified by Made Indrayana from Double Shot Audio
// FMOD Studio Unity Integration 2.00.09
// Add the following lines to the file here -> "Assets/Plugins/FMOD/src/Runtime"
// To reduce confusion I would recommend to add these lines somewhere near the original PlayOneShot() and PlayOneShotAttached()

/////////// START: Modified PlayOneShot() for adding more than one parameter with tuplets (string, float)
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
/////////// END: Modified PlayOneShot() for adding more than one parameter with tuplets (string, float)

        public static void PlayOneShotAttached(string path, GameObject gameObject)
        {
            try
            {
                PlayOneShotAttached(PathToGUID(path), gameObject);
            }
            catch (EventNotFoundException)
            {
                Debug.LogWarning("[FMOD] Event not found: " + path);
            }
        }
        
        public static void PlayOneShotAttached(Guid guid, GameObject gameObject)
        {
            var instance = CreateInstance(guid);
            AttachInstanceToGameObject(instance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
            instance.start();
            instance.release();
        }
        

/////////// START: Modified PlayOneShotAttached() for adding more than one parameter with tuplets (string, float)       
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


        public static void PlayOneShotAttached(Guid guid, GameObject gameObject, params (string name, float value)[]parameters)
        {
            var instance = CreateInstance(guid);
            AttachInstanceToGameObject(instance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
            foreach(var parameter in parameters)
            {
                instance.setParameterByName(parameter.name, parameter.value);
            }
            instance.start();
            instance.release();
        }
/////////// END: Modified PlayOneShotAttached() for adding more than one parameter with tuplets (string, float)
