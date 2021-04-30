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

        public Canvas attackMenu;
        public Canvas escMenu;
        public Canvas parametersMenu;

        public static Dictionary<CanvasType, Canvas> canvases = new Dictionary<CanvasType, Canvas>();

        public string Username { get; set; }
        public static bool turn { get; set; }
        public static bool lockCursor = true;

        void Start()
        {
            canvases.Add(CanvasType.ESC, escMenu);
            canvases.Add(CanvasType.ATTACK, attackMenu);
            canvases.Add(CanvasType.PARAMETERS, parametersMenu);

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
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (turn && !IsActive(CanvasType.ATTACK))
                {
                    ShowMenu(CanvasType.ATTACK);
                }
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsActive(CanvasType.ESC))
                {
                    ShowMenu(CanvasType.NONE);
                }
                else
                {
                    ShowMenu(CanvasType.ESC);
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && IsActive(CanvasType.ATTACK))
            {
                Vector2 mouse = Input.mousePosition;

                RectTransform rect = attackMenu.GetComponent<RectTransform>();

                Vector2 anchorPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, mouse, null, out anchorPos);

                Debug.LogWarning("X : " + anchorPos.x + " Y : " + anchorPos.y);

                new AttackPacket()
                {
                 x = anchorPos.x,
                 y = anchorPos.y
                }.Send();

            }
        }

        private static Canvas prevCanvas;

        public static void ShowMenu(CanvasType type)
        {
            if (prevCanvas != null)
            {
                prevCanvas.gameObject.SetActive(false);
            }

            Canvas canvas; 
            canvases.TryGetValue(type, out canvas);

            if (canvas == null || type == CanvasType.NONE)
            {
                prevCanvas = null;
                foreach(KeyValuePair<CanvasType, Canvas> c in canvases)
                {
                    
                    c.Value.gameObject.SetActive(false);
                    Debug.LogWarning($"Key {c.Key} ValueActive: {c.Value.gameObject.activeSelf}");
                }
                SetCursorLock(true);
                return;
            }

            prevCanvas = canvas;
            canvas.gameObject.SetActive(true);
            SetCursorLock(false);
        }

        public static bool IsActive(CanvasType type)
        {
            Canvas canvas;
            canvases.TryGetValue(type, out canvas);

            return canvas != null && canvas.gameObject.activeSelf;
        }

        public static Canvas getByType(CanvasType type)
        {
            Canvas canvas;
            canvases.TryGetValue(type, out canvas);

            return canvas;
        }

        public static void SetCursorLock(bool value)
        {
            lockCursor = value;
            if (!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;

                Cursor.visible = true;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }


        void HideAll()
        {
            ShowMenu(CanvasType.NONE);
        }

        public enum CanvasType
        {
            PARAMETERS,
            ATTACK,
            ESC,
            NONE



        }
    }
}