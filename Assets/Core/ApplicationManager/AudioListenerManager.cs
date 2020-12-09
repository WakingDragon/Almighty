using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioListenerManager : MonoBehaviour
{
    AudioListener activeListener;

    public void UpdateListeners(Scene scene)
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        bool foundListener = false;

        for (int i = 0; i < listeners.Length;i++)
        {
            if(listeners[i].gameObject.scene != scene || foundListener)
            {
                //listeners[i].enabled = false;
                listeners[i].gameObject.SetActive(false);
            }
            else
            {
                //listeners[i].enabled = true;
                listeners[i].gameObject.SetActive(true);
            }
        }
    }


    //OLD...

    //public void RegisterNewActiveListener(AudioListener newListener)
    //{
    //    if (newListener != null) 
    //    { 
    //        activeListener = newListener; 
    //    }
    //}
    
    //public void ParseListeners(Scene scene, bool setActive)
    //{
    //    if(!setActive) { return; }

    //    AudioListener[] listeners = FindObjectsOfType<AudioListener>();
    //    GameObject[] gos = scene.GetRootGameObjects();

    //    for (int i = 0; i < listeners.Length; i++)
    //    {
    //        for(int j = 0; j < gos.Length; j++)
    //        {
    //            if (listeners[i].gameObject == gos[j].gameObject)
    //            {
    //                activeListener = listeners[i];
    //            }
    //        }
    //    }

    //    for (int i = 0; i < listeners.Length; i++)
    //    {
    //        if (listeners[i] == activeListener)
    //        {
    //            listeners[i].enabled = true;
    //        }
    //        else
    //        {
    //            listeners[i].enabled = false;
    //        }
    //    }
    //}
}
