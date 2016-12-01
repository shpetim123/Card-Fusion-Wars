using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class backMusic : MonoBehaviour {

	public static backMusic instance;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else
			Destroy (gameObject);
	}


	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
