using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    class OnGameOpening
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void AfterSceneLoaded()
        {
            // Load saved hotkeys
            HotkeysManager.Get().Load();

            // Load saved sound preferences
            AudioManager audioManager = GameObject.FindObjectOfType<AudioManager>();
            if(audioManager != null)
            {
                audioManager.LoadPreferences();
            } else
            {
                Debug.LogWarning("no audio manager found");
            }
        }

    }
}
