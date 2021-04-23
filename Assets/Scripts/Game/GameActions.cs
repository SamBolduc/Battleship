using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActions : MonoBehaviour
{

    public Canvas escMenu;
    public Canvas parameters;

    private void Start()
    {
        parameters.gameObject.SetActive(false);
    }

    public void ShowParameters()
    {
        escMenu.gameObject.SetActive(false);
        parameters.gameObject.SetActive(!parameters.gameObject.activeSelf);
        Game.inParameters = true;
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

        parameters.gameObject.SetActive(false);
        Game.inParameters = false;
        Game.SetCursorLock(true);
    }

}
