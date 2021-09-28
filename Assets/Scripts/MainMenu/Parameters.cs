using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Parameters : MonoBehaviour
{

    private bool _waitingForKey = false;
    private string _waitingTag = null;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Parameters
        if (scene.buildIndex == 2)
        {
            HotkeysManager.Get().Keys.ForEach(key =>
            {
                HotkeysManager.Get().UpdateButton(key.Name);
            });

            Slider[] sliders = FindObjectsOfType<Slider>();
            foreach (Slider slider in sliders)
            {
                AudioManager.preferences.ForEach(pref =>
                {
                    if(slider.tag.Equals(pref.type))
                    {
                        slider.value = pref.audioLevel;
                    }
                });
            }
        }
    }

    private void OnGUI()
    {
        if (!_waitingForKey || _waitingTag == null)
        {
            return;
        }

        Event e = Event.current;
        if (e.isKey)
        {
            HotkeysManager.Get().Set(_waitingTag, e.keyCode);
            _waitingForKey = false;
            _waitingTag = null;
        }
    }

    public void SetHotkey()
    {
        _waitingForKey = true;
        _waitingTag = gameObject.tag;
    }

    public void OnSoundChange()
    {
        Slider slider = GameObject.FindGameObjectWithTag(gameObject.tag).GetComponent<Slider>();
        if (slider != null)
        {
            Text text = slider.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = Mathf.Round(slider.value * 100) + "";

                AudioManager audioManager = FindObjectOfType<AudioManager>();
                if(audioManager != null)
                {
                    audioManager.SetVolume(gameObject.tag, slider.value);
                } 
                else
                {
                    Debug.LogWarning("No AudioManager found");
                }
            }
        }
    }

    public void Apply()
    {
        HotkeysManager.Get().Save();
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if(audioManager != null)
        {
            audioManager.SavePreferences();
        }

        SceneManager.LoadScene(0);
    }

}
