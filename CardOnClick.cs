using UnityEngine;
using System.Collections;

public class CardOnClick : MonoBehaviour {

	public Card thisCard;

	void OnMouseDown()
	{
		Debug.Log ("MOUSE DOWN");
		CardController.instance.cardWasClicked = true;
		if (thisCard.GetCardType == CardType.Base && thisCard.InHand) {
			CardFuser.instance.ChangeBase (thisCard);
			AudioManager.instance.Play (CardController.instance.cardEffect, 0.6f);
		} else if (thisCard.GetCardType == CardType.Modifier && thisCard.InHand) {
			CardFuser.instance.AddModifier (thisCard);
			AudioManager.instance.Play (CardController.instance.cardEffect, 0.6f);
		} else if (thisCard.GetCardType == CardType.Base) {
			CardFuser.instance.RemoveBase ();
			AudioManager.instance.Play (CardController.instance.cardEffect, 0.6f);
		} else if (thisCard.GetCardType == CardType.Modifier) {
			CardFuser.instance.RemoveModifier (thisCard);
			AudioManager.instance.Play (CardController.instance.cardEffect, 0.6f);
		}
		
	}
}
