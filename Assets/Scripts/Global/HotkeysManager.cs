
using System.Collections.Generic;
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

    private Dictionary<string, KeyCode> keys;

    public HotkeysManager()
    {
        keys = new Dictionary<string, KeyCode>()
        {
            {"HOTKEY_FORWARD", KeyCode.W},
            {"HOTKEY_BACKWARD", KeyCode.S},
            {"HOTKEY_RIGHT", KeyCode.D},
            {"HOTKEY_LEFT", KeyCode.A},
            {"HOTKEY_JUMP", KeyCode.Space},
            {"HOTKEY_SPRINT", KeyCode.LeftShift},
            {"HOTKEY_INTERACT", KeyCode.E},
        };
    }
    
    public void Set(string tag, KeyCode key)
    {
        if(!keys.ContainsKey(tag))
        {
            Debug.LogWarning("Hotkey tag : " + tag + " hasn't been found");
            return;
        }

        keys[tag] = key;

        Text text = GetButtonText(tag);
        if(text == null)
        {
            return;
        }

        Color32 gray = new Color32(33, 33, 33, 255);
        Color32 red = new Color32(211, 47, 47, 255);

        foreach (KeyValuePair<string, KeyCode> entry in keys)
        {
            Text entryText = GetButtonText(entry.Key);
            if (entryText != null)
            {
                entryText.color = gray;
            }

            bool isDuplicated = false;
            foreach (KeyValuePair<string, KeyCode> subEntry in keys)
            {
                if(entry.Value == subEntry.Value && entry.Key != subEntry.Key)
                {
                    isDuplicated = true;
                    break;
                }
            }

            if(isDuplicated)
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

        text.text = key.ToString();
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

}
