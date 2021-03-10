using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneLoad : MonoBehaviour
{

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //TODO : Method is supposed to be called automatically, but is not.
        Debug.LogWarning("loaded parameters");

        // Parameters
        if (scene.buildIndex == 2)
        {
            HotkeysManager.Get().Keys.ForEach(key =>
            {
                HotkeysManager.Get().UpdateButton(key.Name);
            });
        }
    }

}
