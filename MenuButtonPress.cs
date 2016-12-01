using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtonPress : MonoBehaviour
{
	void Start ()
	{
	
	}

	void Update ()
	{
	
	}

	public void PlayButtonClick()
	{
		SceneManager.LoadScene ("CharliePeteDev");
		Debug.Log ("Here");
	}

	public void ExitButtonPress()
	{
		Application.Quit ();
		Debug.Log ("Quitting");
	}

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene ("MainMenu");
		Debug.Log ("Returning to Menu");
	}
}
