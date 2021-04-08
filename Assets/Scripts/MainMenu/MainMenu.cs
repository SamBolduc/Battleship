using Assets.Scripts.Game.Network.Packets.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        NetworkManager.Instance.Init();
    }

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
        new PlayPacket()
        {
            Username = "Player" + Random.Range(1000, 9999)
        }.Send();
    }
}
