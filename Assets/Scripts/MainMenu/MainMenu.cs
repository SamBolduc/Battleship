using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToParameters()
    {
        SceneManager.LoadScene(2);
    }

    public void FindGame()
    {
        SceneManager.LoadScene(3);
    }

}
