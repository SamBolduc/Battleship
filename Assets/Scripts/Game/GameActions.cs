using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActions : MonoBehaviour
{

    public void ShowParameters()
    {
        Game.ShowMenu(Game.CanvasType.PARAMETERS);
        Game.SetCursorLock(false);
    }

    public void SaveParameters()
    {
        HotkeysManager.Get().Save();
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.SavePreferences();
        }

        Game.ShowMenu(Game.CanvasType.ESC);
    }
}
