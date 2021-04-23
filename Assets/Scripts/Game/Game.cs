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
        public Canvas escMenu;

        public string Username { get; set; }
        public static bool turn { get; set; }

        void Start()
        {
            HideAll();
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
                Cursor.lockState = !attackPanel.gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Confined;
                attackPanel.gameObject.SetActive(!attackPanel.gameObject.activeSelf);
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = !escMenu.gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Confined;
                escMenu.gameObject.SetActive(!escMenu.gameObject.activeSelf);
            }
        }

        void HideAll()
        {
            attackPanel.gameObject.SetActive(false);
            escMenu.gameObject.SetActive(false);
        }
    }
}