using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

	public string name;

	public AudioClip clip;
	//public GameObject originator;

	[Range(0f, 1f)]
	public float volume = .75f;
	[Range(0f, 1f)]
	public float volumeVariance = .1f;

	[Range(.1f, 3f)]
	public float pitch = 1f;
	[Range(0f, 1f)]
	public float pitchVariance = .1f;

	[Range(0f, 1f)]
	public float spatialBlend = 1f;

	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

	//public Sound(string name, AudioClip clip, GameObject originator)
	//{
	//	this.name = name;
	//	this.clip = clip;
	//	this.originator = originator;
	//}
}