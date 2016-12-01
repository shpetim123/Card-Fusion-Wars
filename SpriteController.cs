using UnityEngine;
using System.Collections.Generic;

public class SpriteController : MonoBehaviour {

	public static SpriteController instance = null;

	Dictionary<string,Sprite> spriteDictionary;



	void Awake()
	{
		if (instance == null)
			instance = this;
		else {
			Destroy (this);
			Debug.Log ("Spritecontroller instance already exists");
		}

		spriteDictionary = new Dictionary<string, Sprite> ();

		Sprite[] sprites = Resources.LoadAll<Sprite> ("CardPics");


		foreach (Sprite s in sprites) {
			Debug.Log (s.name);
			spriteDictionary.Add (s.name, s);
		}	 

		sprites = Resources.LoadAll<Sprite> ("Tiles");
		foreach (Sprite s in sprites) {
			Debug.Log (s.name);
			spriteDictionary.Add (s.name, s);
		}

	}


	// Use this for initialization
	void Start () {

	

	}

	public Sprite GetSprite(string name)
	{

		if (spriteDictionary.ContainsKey (name))
			return spriteDictionary [name];
		else {
			Debug.LogError ("NAME "+ name + " DOES NOT EXIST IN SPRITE DICTIONARY");
			return null;
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
