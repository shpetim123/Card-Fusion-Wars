using UnityEngine;
using System.Collections.Generic;

public class InHandHover : MonoBehaviour {

	public bool inHand = true;
	public Card thisCard;
	void Start()
	{


	}


	void OnMouseEnter(){
		if (thisCard.InHand) 
		{
			Vector3 newLoc = thisCard.CardObjectTransform.position;
			newLoc.y += 1.85f;
			newLoc.z -= 15;
			thisCard.GetCardMover.targetLoc = newLoc;
		}
		//transform.Translate(new Vector3(0,1.1f,-15));
	}

	void OnMouseExit(){
		if(thisCard.InHand)
		{
			Vector3 newLoc = thisCard.CardObjectTransform.position;
			newLoc.y -= 1.85f;
			newLoc.z += 15;
			thisCard.GetCardMover.targetLoc = newLoc;
		}
		//transform.Translate (new Vector3 (0, -1.1f,15));
		HandManager.instance.SortHand ();
	}
}
