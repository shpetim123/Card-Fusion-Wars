using UnityEngine;
using System.Collections.Generic;

public enum CardType {Modifier,Base};
public enum SubType {Structure,Unit,Tool,Material};

public class Card {

	protected bool inHand;
	protected bool inFuser;
	protected CardType cardType;
	protected SubType subType;
	protected GameObject cardObject;
	protected Transform cardObjectTransform;
	protected CardMover cardMover;
	protected GameObject prefab;
	protected string name;
	protected Sprite cardImage;
	protected string cardText;
	protected bool prefabCard;

	protected int range;
	protected int attack;
	protected int defence;
	protected int speed;
	protected int resourceCost;
	protected int resourceGen;
	protected int researchGen;

	string structurePrefix;
	string unitPrefix;

	public Card(Card baseCard, Card mod1, Card mod2)
	{
		inHand = false;
		inFuser = false;
		cardType = CardType.Base;
		subType = baseCard.GetSubType;
		prefab = baseCard.Prefab;
		range 	= 	baseCard.Range;
		attack 	= 	baseCard.Attack;
		defence = 	baseCard.Defence;
		speed 	= 	baseCard.Speed;
		resourceCost = baseCard.ResourceCost;
		resourceGen = baseCard.ResourceGen;
		researchGen = baseCard.researchGen;
		prefabCard = false;
		string prefix1;
		string prefix2;

		if (mod1 != null) {
			range += mod1.Range;
			attack += mod1.Attack;
			defence += mod1.Defence;
			speed += mod1.Speed;

			if (subType == SubType.Structure) {
				Debug.Log ("STRUCTURE");
				prefix1 = mod1.StructurePrefix;
			} else {
				prefix1 = mod1.UnitPrefix;
				Debug.Log ("UNIT");
			}
		} 
		else
			prefix1 = "";
		
		if (mod2 != null)
		{
			range 	+= mod2.Range;
			attack  += mod2.Attack;
			defence += mod2.Defence;
			speed 	+= mod2.Speed;

			if (subType == SubType.Structure)
				prefix2 = mod2.StructurePrefix;
			else
				prefix2 = mod2.UnitPrefix;
		}
		else
			prefix2 = "";

	//	Debug.Log("prefix1: " + prefix1);
	//	Debug.Log("prefix2: " + prefix2);

		if (prefix2 == "") 
				name = prefix1 + " " + prefix2 + " " + baseCard.Name;
		else
			name = prefix1 + " " + prefix2 + "\n" + baseCard.Name;

		Debug.Log ("Name:" + name);
		cardImage = baseCard.CardImage;
		cardText = baseCard.CardText;

		cardObject = GameObject.Instantiate (prefab);
		cardObject.GetComponent<CardOnClick> ().thisCard = this;
		cardObject.GetComponent<InHandHover> ().thisCard = this;
		cardMover = cardObject.GetComponent<CardMover> ();
		Debug.Log ("card made");

	

	cardObjectTransform = cardObject.GetComponent<Transform> ();
		CardObjectTransform.position = new Vector2 (5, -3);

		UpdateCardObject ();

	}



	public Card(GameObject _prefab,CardType _type,SubType _subType, string _name, Sprite _cardImage, string _cardText,bool _prefabCard,int _range,int _attack,int _defence,int _speed,int _resourceCost, int _resourceGen,int _researchGen)
	{
		inHand = true;
		inFuser = false;
		cardType = _type;
		subType = _subType;
		prefab = _prefab;
		name = _name;
		cardImage = _cardImage;
		cardText = _cardText;
		prefabCard = _prefabCard;
		range = _range;
		attack = _attack;
		defence = _defence;
		speed = _speed;
		resourceCost = _resourceCost;
		resourceGen = _resourceGen;
		researchGen = _researchGen;
		if (prefabCard)
			cardObject = prefab;
		else {
			cardObject = GameObject.Instantiate (prefab);
			cardObject.GetComponent<CardOnClick> ().thisCard = this;
			cardObject.GetComponent<InHandHover> ().thisCard = this;
			cardMover = cardObject.GetComponent<CardMover> ();
			Debug.Log ("card made");
			
		}

		cardObjectTransform = cardObject.GetComponent<Transform> ();


		UpdateCardObject ();

		unitPrefix = null;
		structurePrefix = null;

	}

	public Card (GameObject _prefab, CardType _type, SubType _subType, string _name, Sprite _cardImage, string _cardText, bool _prefabCard, int _range,int _attack,int _defence,int _speed,int _resourceCost, int _resourceGen,int _researchGen,string _structurePrefix,string _unitPrefix) 
	{
		Debug.Log ("SPESHUL CONSTRUCTOR");
		inHand = true;
		inFuser = false;
		cardType = _type;
		subType = _subType;
		prefab = _prefab;
		name = _name;
		cardImage = _cardImage;
		cardText = _cardText;
		prefabCard = _prefabCard;
		range = _range;
		attack = _attack;
		defence = _defence;
		speed = _speed;
		resourceCost = _resourceCost;
		resourceGen = _resourceGen;
		researchGen = _researchGen;
		if (prefabCard)
			cardObject = prefab;
		else {
			cardObject = GameObject.Instantiate (prefab);
			cardObject.GetComponent<CardOnClick> ().thisCard = this;
			cardObject.GetComponent<InHandHover> ().thisCard = this;
			cardMover = cardObject.GetComponent<CardMover> ();
			Debug.Log ("card made");

		}

		cardObjectTransform = cardObject.GetComponent<Transform> ();


		UpdateCardObject ();

		structurePrefix = _structurePrefix;
		Debug.Log (StructurePrefix);
		unitPrefix = _unitPrefix;
		Debug.Log (UnitPrefix);

	}
	public Card MakeCopy()
	{
		Card newCard = new Card (prefab, cardType, subType, name, cardImage, cardText, false, range, attack, defence, speed,resourceCost,resourceGen,researchGen);

		if (unitPrefix != null) {
			newCard.UnitPrefix = unitPrefix;
			newCard.StructurePrefix = structurePrefix;
		}
		return newCard;
	}

	void UpdateCardObject()
	{
		List<GameObject> children = new List<GameObject> ();
		foreach (Transform t in cardObject.transform) {
			children.Add (t.gameObject);
		}
		foreach (GameObject c in children) {
			switch (c.name) 
			{
			case "cardpic":
				c.GetComponent<SpriteRenderer> ().sprite = cardImage;
				break;
			case "name":
				c.GetComponent<TextMesh> ().text = name;
				break;
			case "desc":
				c.GetComponent<TextMesh> ().text = cardText;
				break;
			case "type":
				c.GetComponent<TextMesh> ().text = cardType.ToString();
				break;
			case "defence":
				c.GetComponent<TextMesh> ().text = defence.ToString ();
				break;
			case "attack":
				c.GetComponent<TextMesh> ().text = attack.ToString ();
				break;
			case "costText":
				c.GetComponent<TextMesh> ().text = resourceCost.ToString ();
				break;
			}
		}
	}

	public GameObject CardObject {
		get {
			return cardObject;
		}
	}

	public GameObject Prefab {
		get {
			return prefab;
		}
	}

	public string Name {
		get {
			return name;
		}
	}

	public Sprite CardImage {
		get {
			return cardImage;
		}
	}

	public string CardText {
		get {
			return cardText;
		}
	}

	public Transform CardObjectTransform {
		get {
			return cardObjectTransform;
		}
	}

	public CardType GetCardType {
		get {
			return cardType;
		}
	}

	public SubType GetSubType {
		get {
			return subType;
		}
	}

	public bool InFuser {
		get {
			return inFuser;
		}
		set {
			inFuser = value;
		}
	}
		
	public bool InHand {
		get {
			return inHand;
		}
		set {
			inHand = value;
		}
	}




	public string StructurePrefix {
		get {
			return structurePrefix;
		}
		set {
			structurePrefix = value;
		}
	}

	public string UnitPrefix {
		get {
			return unitPrefix;
		}
		set {
			unitPrefix = value;
		}
	}

	public CardMover GetCardMover {
		get {
			return cardMover;
		}
	}

	public int Range {
		get {
			return range;
		}
		set {
			range = value;
			UpdateCardObject ();
		}
	}

	public int Attack {
		get {
			return attack;
		}
		set {
			attack = value;
			UpdateCardObject ();
		}
	}

	public int Defence {
		get {
			return defence;
		}
		set {
			defence = value;
			UpdateCardObject ();
		}
	}

	public int Speed {
		get {
			return speed;
		}
		set {
			speed = value;
			UpdateCardObject ();
		}
	}

	public int ResourceCost {
		get {
			return resourceCost;
		}
		set {
			resourceCost = value;
		}
	}

	public int ResourceGen {
		get {
			return resourceGen;
		}
		set {
			resourceGen = value;
		}
	}

	public int ResearchGen {
		get {
			return researchGen;
		}
		set {
			researchGen = value;
		}
	}
}
