using UnityEngine.Audio;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	public static List<AudioPreference> preferences = new List<AudioPreference>();

	private string _filePath = null;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		if(preferences.Count <= 0)
        {
			preferences.Add(new AudioPreference()
			{
				type = "AMBIANT_SOUND",
				audioLevel = 1
			});
			preferences.Add(new AudioPreference()
			{
				type = "SPECIAL_EFFECTS",
				audioLevel = 1
			});
		}

		_filePath = Application.dataPath + Path.DirectorySeparatorChar + "sounds.json";

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

    void Start()
    {
		Play("OceanSound");
    }

    public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void SetVolume(string type, float volume)
    {
		preferences.ForEach(pref =>
		{
            if (pref.type.Equals(type))
            {
				pref.audioLevel = volume;
            }
		});

		foreach (Sound s in sounds)
		{
			if (s.type.Equals(type))
            {
				s.volume = volume;
				s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			}
		}
	}

	public void SavePreferences()
    {
		if (_filePath != null)
        {
			File.WriteAllText(_filePath, JsonConvert.SerializeObject(preferences));
		}
	}

	public void LoadPreferences()
    {
		if (File.Exists(_filePath))
		{
			preferences = JsonConvert.DeserializeObject<List<AudioPreference>>(File.ReadAllText(_filePath));

			preferences.ForEach(pref =>
			{
				foreach(Sound sound in sounds)
                {
					if(sound.type.Equals(pref.type))
                    {
						sound.volume = pref.audioLevel;
						sound.source.volume = sound.volume;
                    }
                }
			});
		}
	}

}
