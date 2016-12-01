using UnityEngine;
using System.Collections;

public class FuseButton : MonoBehaviour {

	public GameObject endTurnButton;
	public AudioClip fuseSound;

	public void OnClick()
	{
		int resourceCost = CardFuser.instance.baseCard.ResourceCost + CardFuser.instance.modifiers [0].ResourceCost;
		if (CardFuser.instance.modifiers.Count > 1)
			resourceCost += CardFuser.instance.modifiers[1].ResourceCost;
		
		if (resourceCost <= TurnManager.instance.currPlayer.resources) {
			AudioManager.instance.Play (fuseSound, 0.4f);
			TurnManager.instance.currPlayer.PayResource (resourceCost);
			CardFuser.instance.Fuse ();
			UnitManager.m_instance.m_isInPlacement = true;
			UnitManager.m_instance.FindValidTiles ();
			HandManager.instance.currHand.gameObject.SetActive (false);
			endTurnButton.SetActive (false);
		}
	}
}
