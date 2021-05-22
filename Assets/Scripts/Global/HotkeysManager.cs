
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HotkeysManager
{

    private static HotkeysManager _instance;
    public static HotkeysManager Get()
    {
        if(_instance == null)
        {
            _instance = new HotkeysManager();
        }

        return _instance;
    }

    private readonly string _filePath = Application.dataPath + Path.DirectorySeparatorChar + "hotkeys.json";

    public List<Hotkey> Keys { get; set; }

    public HotkeysManager()
    {
        Keys = new List<Hotkey>()
        {
            new Hotkey() {
                Name = "HOTKEY_FORWARD",
                KeyCode = KeyCode.W
            },
            new Hotkey() {
                Name = "HOTKEY_BACKWARD",
                KeyCode = KeyCode.S
            },
            new Hotkey() {
                Name = "HOTKEY_RIGHT",
                KeyCode = KeyCode.D
            },
            new Hotkey() {
                Name = "HOTKEY_LEFT",
                KeyCode = KeyCode.A
            },
            new Hotkey() {
                Name = "HOTKEY_JUMP",
                KeyCode = KeyCode.Space
            },
            new Hotkey() {
                Name = "HOTKEY_SPRINT",
                KeyCode = KeyCode.LeftShift
            },
            new Hotkey() {
                Name = "HOTKEY_INTERACT",
                KeyCode = KeyCode.E
            },
        };
        Load();
    }
    
    public void Set(string tag, KeyCode key)
    {
        Hotkey hotkey = GetHotkey(tag);
        if(hotkey == null)
        {
            Debug.LogWarning("Hotkey tag : " + tag + " hasn't been found");
            return;
        }

        hotkey.KeyCode = key;
        UpdateButton(tag);
    }

    private Text GetButtonText(string tag)
    {
        GameObject obj = GameObject.FindGameObjectWithTag(tag);
        if (obj == null)
        {
            Debug.LogWarning("No object with tag : " + tag + " found");
            return null;
        }

        Button button = obj.GetComponentInChildren<Button>();
        if (button == null)
        {
            Debug.LogWarning("No button found under object with tag : " + tag);
            return null;
        }

        Text text = button.GetComponentInChildren<Text>();
        if (text == null)
        {
            Debug.LogWarning("No text found under button with tag : " + tag);
            return null;
        }

        return text;
    }

    public void UpdateButton(string tag)
    {
        Text text = GetButtonText(tag);
        if (text == null)
        {
            return;
        }

        Color32 gray = new Color32(33, 33, 33, 255);
        Color32 red = new Color32(211, 47, 47, 255);

        foreach (Hotkey entry in Keys)
        {
            Text entryText = GetButtonText(entry.Name);
            if (entryText != null)
            {
                entryText.color = gray;
            }

            bool isDuplicated = false;
            foreach (Hotkey subEntry in Keys)
            {
                if (entry.KeyCode == subEntry.KeyCode && entry.Name != subEntry.Name)
                {
                    isDuplicated = true;
                    break;
                }
            }

            if (isDuplicated)
            {
                if (entryText != null)
                {
                    entryText.color = red;
                }
                else
                {
                    entryText.color = gray;
                }
            }
        }

        Hotkey keyCode = GetHotkey(tag);
        if(keyCode != null)
        {
            text.text = keyCode.KeyCode.ToString();
        }
        else
        {
            Debug.LogWarning("Couldn't update button text because the keycode wasn't found");
        }
    }

    public Hotkey GetHotkey(string name)
    {
        foreach (Hotkey key in Keys)
        {
            if (key.Name.Equals(name))
            {
                return key;
            }
        }
        return null;
    }

    public void Save()
    {
        File.WriteAllText(_filePath, JsonConvert.SerializeObject(Keys));
    }

    public void Load()
    {
        if (File.Exists(_filePath))
        {
            Keys = JsonConvert.DeserializeObject<List<Hotkey>>(File.ReadAllText(_filePath));

            foreach (var hotkey in Keys)
            {
                UpdateButton(hotkey.Name);
            }
        }
    }

}
