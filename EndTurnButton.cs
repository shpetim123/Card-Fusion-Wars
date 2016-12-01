using UnityEngine;
using System.Collections;

public class EndTurnButton : MonoBehaviour {

	public void EndTurnPress()
	{
		TurnManager.instance.EndTurn ();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
