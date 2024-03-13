using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	public AudioMixerGroup mixerGroup;

	//public Sound[] sounds;
	public List<Sound> sounds;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.volume = s.volume;
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.spatialBlend = s.spatialBlend;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	public void StopAllSound()
	{
		foreach (Sound s in sounds)
		{
			s.source.Stop();
		}
	}

	public void Play(string soundName)
	{
		Sound s = SoundtoPlay(soundName);

		if (s != null)
		{
			s.source.Play();
			
		}
	}

	public void PlayBackgroundMusic()
	{
		StopAllSound();
		Camera.main.gameObject.GetComponent<AudioSource>().Play();
	}

	public void PlayAtPoint(string soundName)
	{
		GameObject originator = Camera.main.gameObject;
		Sound s = SoundtoPlay(soundName);

		if (s != null)
		{
			AudioSource.PlayClipAtPoint(s.clip, originator.transform.position, s.volume);
		}
	}

	public void PlayAtPoint(string soundName, GameObject originator)
	{
		Sound s = SoundtoPlay(soundName);

		if (s != null)
		{
			AudioSource.PlayClipAtPoint(s.clip, originator.transform.position, s.volume);
		}
	}

	private Sound SoundtoPlay(string soundName)
	{
		Sound s = sounds.Find(x => x.name.Contains(soundName));

		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return null;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		return s;
	}

}
