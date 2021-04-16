using Assets.Scripts.Game.Network.Packets;
using Assets.Scripts.Game.Network.Packets.Types;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Game : MonoBehaviour
    {

        public Canvas attackPanel;

        public string Username { get; set; }
        public static bool turn { get; set; }

        void Start()
        {
            attackPanel.gameObject.SetActive(false);
            OverlayManager overlay = GameObject.FindObjectOfType<OverlayManager>();
            if (turn)
            {
                overlay.DisplayText("À l'attaque!", "C'est à votre tour. Cliquez sur E pour attaquer", 10);
            }
            else
            {
                overlay.DisplayText("Attendez!", "L'ennemi vous envoie une attaque...", 10);
            }
        }

        void Update()
        {
            if(turn && Input.GetKeyDown(KeyCode.E))
            {
                attackPanel.gameObject.SetActive(true);
            }
        }
    }
}