using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Game : MonoBehaviour
{

    void Start()
    {
        while(NetworkManager.TcpBootstrap == null)
        {
            // Waiting for server connection
        }
        NetworkManager.ConnectGameServer("ip", "188.40.72.202", 8989);
        NetworkManager.SendByte(Encoding.UTF8.GetBytes(""));
    }

    
    void Update()
    {
        
    }


}
