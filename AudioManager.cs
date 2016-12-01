using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	AudioClip backMusic;
	AudioSource source;


	void Awake()
	{
		if (instance == null) {
			instance = this;
		} 
		else 
		{
			Debug.Log ("Audio manager instance already found");
			Destroy (this);
		}
	}


	// Use this for initialization
	void Start () {
		source = this.GetComponent<AudioSource>();
	}


	public void Play (AudioClip clip, float _inVolume)
	{
		source.volume = _inVolume;
		source.PlayOneShot (clip);
	}
}
