using UnityEngine;
using System.Collections.Generic;

public class CardFuser : MonoBehaviour {

	public static CardFuser instance;

	public GameObject fuseButton;
	public Card baseCard;
	public List<Card> modifiers;
	public Card fusedCard;
	public GameObject baseCardLoc;
	public GameObject modCardLoc;
	public GameObject resultLoc;

	public void EmptyFuser()
	{
		RemoveBase ();
		if (modifiers.Count > 0)
			RemoveModifier (modifiers [0]);
		if (modifiers.Count > 0)
			RemoveModifier (modifiers [0]);
	}

	public Card Fuse()
	{
		if (modifiers.Count == 1)
			fusedCard = new Card (baseCard, modifiers [0],null);
		else if (modifiers.Count == 2)
			fusedCard = new Card (baseCard, modifiers [0], modifiers [1]);
		fusedCard.CardObjectTransform.parent = resultLoc.transform;
		fusedCard.GetCardMover.targetLoc = resultLoc.transform.position;

		Destroy (baseCard.CardObject);
		Destroy (modifiers [0].CardObject);

		if (modifiers.Count == 2)
			Destroy (modifiers [1].CardObject);

		modifiers.Clear ();
		baseCard = null;
		ToggleFuseButton ();
		return fusedCard;
	}

	void ToggleFuseButton()
	{
		if (baseCard != null && modifiers.Count > 0)
			fuseButton.SetActive (true);
		else
			fuseButton.SetActive (false);
	}

	void Awake()
	{
		if (instance == null)
			instance = this;
		else {
			Destroy (this);
			Debug.Log ("CardFuser instance already exists");
		}
		baseCard = null;
		modifiers = new List<Card> ();
	}
	public void AddModifier(Card _modCard)
	{
		if (modifiers.Count < 2) 
			{
			HandManager.instance.RemoveCard (_modCard);
			_modCard.CardObjectTransform.parent = modCardLoc.transform;
			modifiers.Add(_modCard);
			SortModifiers ();	
			Debug.Log ("ChangedBase");
			ToggleFuseButton ();
		}
	}

	void SortModifiers()
	{
		if (modifiers.Count > 0)
			//modifiers [0].CardObjectTransform.localPosition = Vector3.zero;
			modifiers[0].GetCardMover.targetLoc = modCardLoc.transform.position;
		if (modifiers.Count > 1)
			//modifiers [1].CardObjectTransform.localPosition = new Vector3 (3, 0, 0);
			modifiers[1].GetCardMover.targetLoc = modCardLoc.transform.position + new Vector3 (3,0,0);
	}

	public void ChangeBase(Card _baseCard)
	{
		if (baseCard != null) 
		{
			HandManager.instance.PushCard (baseCard);
			baseCard.InFuser = false;
		}
		HandManager.instance.RemoveCard (_baseCard);
		_baseCard.CardObjectTransform.parent = baseCardLoc.transform;
		//_baseCard.CardObjectTransform.localPosition = Vector3.zero;
		_baseCard.GetCardMover.targetLoc = baseCardLoc.transform.position;
		_baseCard.InFuser = true;
		baseCard = _baseCard;
		Debug.Log ("ChangedBase");
		ToggleFuseButton ();
	}

	public void RemoveBase()
	{
		if (baseCard != null) 
		{
			HandManager.instance.PushCard (baseCard);
			baseCard.InFuser = false;
			baseCard = null;
		}
		HandManager.instance.SortHand ();
		ToggleFuseButton ();
	}

	public void RemoveModifier(Card _modifier)
	{
		if (modifiers.Contains(_modifier))
		{
		HandManager.instance.PushCard (_modifier);
		_modifier.InFuser = false;
		modifiers.Remove (_modifier);
		}
		HandManager.instance.SortHand ();
		SortModifiers ();
		ToggleFuseButton ();
	}

	public

	void UpdateResult()
	{
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
