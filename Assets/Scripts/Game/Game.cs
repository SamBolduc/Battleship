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
        public static bool lockCursor = true;

        public static bool inMenu = false;
        public static bool inParameters = false;

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
                attackPanel.gameObject.SetActive(!attackPanel.gameObject.activeSelf);
                Game.SetCursorLock(!attackPanel.gameObject.activeSelf);
            }

            if(Input.GetKeyDown(KeyCode.Escape) && !inParameters)
            {
                escMenu.gameObject.SetActive(!escMenu.gameObject.activeSelf);
                Game.SetCursorLock(!escMenu.gameObject.activeSelf);
            }


            if (Input.GetKeyDown(KeyCode.Mouse0) && !inMenu)
            {
                Vector2 mouse = Input.mousePosition;

                RectTransform rect = attackPanel.GetComponent<RectTransform>();

                Vector2 anchorPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, mouse, null, out anchorPos);

                Debug.LogWarning("X : " + anchorPos.x + " Y : " + anchorPos.y);
            }
        }


        public static void SetCursorLock(bool value)
        {
            lockCursor = value;
            if (!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;

                Cursor.visible = true;
                Game.inMenu = true;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Game.inMenu = false;
            }
        }


        void HideAll()
        {
            attackPanel.gameObject.SetActive(false);
            escMenu.gameObject.SetActive(false);
        }
    }
}