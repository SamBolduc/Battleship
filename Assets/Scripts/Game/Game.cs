using Assets.Scripts.Game.Network.Packets;
using Assets.Scripts.Game.Network.Packets.Types;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.Scripts.Game
{
    public class 
        Game : MonoBehaviour
    {

        public Canvas attackMenu;
        public Canvas escMenu;
        public Canvas parametersMenu;

        public GameObject attackCircle;
        public GameObject boatStatus;

        private static Game instance;

        public static Game GetGameObject()
        {
            return instance;
        }

        public static Dictionary<CanvasType, Canvas> canvases = new Dictionary<CanvasType, Canvas>();

        public string Username { get; set; }
        public static bool turn { get; set; }
        public static bool lockCursor = true;

        public static void UpdateBoats(List<Boat> myBoats, List<Boat> enemyBoats)
        {
            Game game = Game.GetGameObject();
            if (game == null) return;
            game.boatStatus.GetComponent<BoatStatusScript>().UpdateStatus(myBoats, enemyBoats);
        }

        public static void AttackResponse(AttackPacket packet)
        {
            Game game = Game.GetGameObject();
            if (game == null) return;
            var canvas = Game.getByType(CanvasType.ATTACK);
            if (canvas == null) return;

            var position = new Vector3(packet.x, packet.y, 0);
            var rotation = new Quaternion(0,0,0,0);

            Image circle = Instantiate(game.attackCircle, position, rotation).GetComponent<Image>();
            if (packet.damageDealt > 0)
            {
                circle.color = new Color32(239, 83, 80, 100);
            }
            circle.transform.SetParent(canvas.transform, false);
        }

        void Start()
        {
            instance = this;
            canvases.Add(CanvasType.ESC, escMenu);
            canvases.Add(CanvasType.ATTACK, attackMenu);
            canvases.Add(CanvasType.PARAMETERS, parametersMenu);

            ShowOverlay();
        }

        public void ShowOverlay()
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

        void OnApplicationQuit()
        {
            NetworkManager.Instance.Disconnect();
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

            if (IsActive(CanvasType.ATTACK))
            {
                if (!turn)
                {
                    ShowMenu(CanvasType.NONE);
                    return;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Vector2 mouse = Input.mousePosition;

                    RectTransform rect = attackMenu.GetComponent<RectTransform>();

                    Vector2 anchorPos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, mouse, null, out anchorPos);

                    new AttackPacket()
                    {
                        x = anchorPos.x,
                        y = anchorPos.y
                    }.Send();
                }
            }

        }

        private static Canvas prevCanvas;

        public static void ShowMenu(CanvasType type)
        {
            if (prevCanvas != null)
            {
                prevCanvas.gameObject.SetActive(false);
            }

            if (type == CanvasType.NONE)
            {
                prevCanvas = null;
                foreach (KeyValuePair<CanvasType, Canvas> c in canvases)
                {
                    c.Value.gameObject.SetActive(false);
                    Debug.LogWarning($"Key {c.Key} ValueActive: {c.Value.gameObject.activeSelf}");
                }

                SetCursorLock(true);
                return;
            }

            Canvas canvas; 
            canvases.TryGetValue(type, out canvas);

            prevCanvas = canvas;
            canvas.gameObject.SetActive(true);
            SetCursorLock(false);
        }

        public static bool IsActive(CanvasType type)
        {
            Canvas canvas;
            canvases.TryGetValue(type, out canvas);

            if (type == CanvasType.NONE && lockCursor) return true; //In game

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
            {
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