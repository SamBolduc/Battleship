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
        Cursor.lockState = !parameters.gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Confined;
        parameters.gameObject.SetActive(!parameters.gameObject.activeSelf);
    }

    public void SaveParameters()
    {
        HotkeysManager.Get().Save();
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.SavePreferences();
        }

        Cursor.lockState = CursorLockMode.Confined;
        parameters.gameObject.SetActive(false);
    }

}
