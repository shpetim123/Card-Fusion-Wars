using UnityEngine;
using System.Collections;

public class CardMover : MonoBehaviour {

	public Vector3 targetLoc;
	public float distance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Vector3.Distance (targetLoc, this.transform.position) > CardController.instance.cardTargetRadius) 
		{
			transform.position = Vector3.Lerp (this.transform.position, targetLoc, 0.4f );
		}
	}
}
