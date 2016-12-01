


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandManager : MonoBehaviour {

	//public GameObject thisObject;
	public static HandManager instance;
	public GameObject cardPrefab;
	public Hand currHand;

	public float cardSpacing = 1.5f;


	void Awake()
	{
		if (instance == null) {
			instance = this;
			Debug.Log ("HANDMANAGER INSTANCE MADE");
		}
		else {
			Destroy (this);
			Debug.Log ("HandManager instance already exists");
		}

	}

	public void ChangeCurrentHand(Hand newHand)
	{
		currHand.gameObject.SetActive (false);
		currHand = newHand;
		newHand.gameObject.SetActive (true);
	}

	// Use this for initialization
	void Start () {
	}

	public void SetupHand()
	{
		currHand.cards = new List<Card>();
		AddCard ("CaveMan");
		AddCard ("CaveMan");
		AddCard ("Spear");
		AddCard ("Goat");
		AddCard ("Oracles Hut");
		AddCard ("Hunting Camp");
		AddCard ("Hunting Camp");
		AddCard ("Club");
		AddCard ("Stone");
		AddCard ("Sling");
		SortHand ();
	}

	void AddCard(string cardname)
	{
		Card newCard = CardController.instance.GetCardCopy (cardname);
		newCard.CardObjectTransform.parent = currHand.transform;
		currHand.cards.Add (newCard);
		if (newCard.GetCardType == CardType.Base)
			currHand.numBases++;
		else
			currHand.numModifiers++;
	}

	public void PushCard(Card _card)
	{
		currHand.cards.Add (_card);
		_card.CardObjectTransform.parent = currHand.transform;
		Debug.Log (currHand.cards.Count);
		_card.InHand = true;
		SortHand ();
		if (_card.GetCardType == CardType.Base)
			currHand.numBases++;
		else
			currHand.numModifiers++;
	}

	public void RemoveCard(Card _card)
	{
		if(currHand.cards.Contains(_card))
		{
			currHand.cards.Remove (_card);
			Debug.Log ("OH GOD " + currHand.cards.Count);
			_card.InHand = false;
			SortHand ();
			if (_card.GetCardType == CardType.Base)
				currHand.numBases--;
			else
				currHand.numModifiers--;
		}
	}

	public void SortHand()
	{
		Debug.Log ("SortHand");
		float cardCount = currHand.cards.Count;
		for (int i = 0; i < cardCount; i++) 
		{
			//Transform pos = cards[i].GetComponent<Transform>();
			Vector3 pos = new Vector3 (currHand.transform.position.x -(((cardCount-1) / 2) * cardSpacing) + (i * cardSpacing), currHand.transform.position.y,-80 + i*3);
		
			currHand.cards [i].CardObject.GetComponent<CardMover>().targetLoc = pos;
		}
		return;
	}




	// Update is called once per frame
	void Update () {
	
	}
}
