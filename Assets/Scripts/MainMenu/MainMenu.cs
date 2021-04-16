using Assets.Scripts.Game.Network.Packets;
using Assets.Scripts.Game.Network.Packets.Types;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
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
            Username = "Player" + UnityEngine.Random.Range(1000, 9999)
        }.Send();
        SceneManager.LoadScene(5);
    }
}
