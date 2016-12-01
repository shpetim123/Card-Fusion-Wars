using UnityEngine;
using System.Collections.Generic;

public class CardController : MonoBehaviour {

	public bool cardWasClicked;
	public static  CardController instance;
	public GameObject cardPrefab;
	Dictionary<string,Card>cardDictionary;
	List<Card> stoneAgeList;
	List<Card> ironAgeList;
	List<Card> middleAgeList;
	List<Card> modernAgeList;
	public AudioClip cardEffect;
	public float cardTargetRadius;

	void OnMouseDown()
	{
		cardWasClicked = false;
	}

	void OnMouseUp()
	{
		Debug.Log ("MOUSE UP");
		cardWasClicked = false;
	}

	void Awake()
	{
		if (instance == null)
			instance = this;
		else {
			Destroy (this);
			Debug.Log ("Cardcontroller instance already exists");
		}

		cardDictionary = new Dictionary<string, Card> ();
		cardTargetRadius = 0.01f;
		ironAgeList = new List<Card> ();
		middleAgeList = new List<Card> ();
		modernAgeList = new List<Card> ();
		stoneAgeList = new List<Card> ();
		CreateCards ();
	}

	public Card GetRandomCard()
	{
		List<Card> cardList = GetValidList ();
		int randNum = Random.Range (0, cardList.Count);
		return cardList [randNum].MakeCopy();
	}

	public Card GetRandomModifier()
	{
		List<Card> cardList = GetValidList ();

		int randNum = Random.Range (0, cardList.Count);
		while (cardList [randNum].GetCardType != CardType.Modifier) {
			randNum = Random.Range (0, cardList.Count);
		}

		return cardList [randNum].MakeCopy();
	}



	public Card GetRandomBase()
	{
		List<Card> cardList = GetValidList ();

		int randNum = Random.Range (0, cardList.Count);
		while (cardList [randNum].GetCardType != CardType.Base) {
			randNum = Random.Range (0, cardList.Count);
		}

		return cardList [randNum].MakeCopy();
	}

	public List<Card> GetValidList()
	{
		if (TurnManager.instance.currPlayer.age == "Stone Age")
			return stoneAgeList;
		else if (TurnManager.instance.currPlayer.age == "Iron Age")
			return ironAgeList;
		else if (TurnManager.instance.currPlayer.age == "Middle Ages")
			return middleAgeList;
		else if (TurnManager.instance.currPlayer.age == "Modern Age")
			return middleAgeList;
		else
			return stoneAgeList;
	}

	// Use this for initialization
	void Start () {
		
	}




	public Card GetCardCopy(string _cardName)
	{
		Debug.Log (_cardName);



		if (cardDictionary.ContainsKey (_cardName)) {
			Card currentCard = cardDictionary [_cardName];
			Card newCard = currentCard.MakeCopy ();
			return newCard;
		} 
		else 
		{
			Debug.Log ("Error creating card copy");
			return null;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	void CreateCards()
	{
		cardDictionary.Add(
			"CaveMan",
			new Card(
				cardPrefab,
				CardType.Base,
				SubType.Unit,
				"CaveMan",
				SpriteController.instance.GetSprite("CaveMan"),
				"THIS IS CAVEMAN,\n UGG",
				true,
				1,
				1,
				2,
				2,
				2,
				0,
				0
			));
		stoneAgeList.Add (cardDictionary ["CaveMan"]);
		ironAgeList.Add (cardDictionary ["CaveMan"]);

		cardDictionary.Add(
			"Spear",
			new Card(
				cardPrefab,
				CardType.Modifier,
				SubType.Tool,
				"Spear",
				SpriteController.instance.GetSprite("Spear"),
				"+1 Range \n+1 Damage\n+1 Defence",
				true,
				1,
				1,
				1,
				0,
				1,
				0,
				0,
				"Spiky",
				"Speary"
			));
		stoneAgeList.Add (cardDictionary ["Spear"]);

		cardDictionary.Add(
			"Sling",
			new Card(
				cardPrefab,
				CardType.Modifier,
				SubType.Tool,
				"Sling",
				SpriteController.instance.GetSprite("Sling"),
				"+1 Range \n+2 Damage",
				true,
				1,
				2,
				0,
				0,
				1,
				0,
				0,
				"Slinger",
				"Slinger"
			));
		stoneAgeList.Add (cardDictionary ["Sling"]);

		cardDictionary.Add(
			"Club",
			new Card(
				cardPrefab,
				CardType.Modifier,
				SubType.Tool,
				"Club",
				SpriteController.instance.GetSprite("Club"),
				" \n+3 Damage",
				true,
				0,
				3,
				0,
				0,
				1,
				1,
				0,
				"Defended",
				"Clubby"

			));
		stoneAgeList.Add (cardDictionary ["Club"]);

		cardDictionary.Add(
			"Goat",
			new Card(
				cardPrefab,
				CardType.Base,
				SubType.Unit,
				"Goat",
				SpriteController.instance.GetSprite("Goat"),
				"This goat makes\na good meat\nshield",
				true,
				1,
				0,
				3,
				3,
				2,
				0,
				0
			));
		stoneAgeList.Add (cardDictionary ["Goat"]);

		cardDictionary.Add(
			"Hunting Camp",
			new Card(
				cardPrefab,
				CardType.Base,
				SubType.Structure,
				"Hunting Camp",
				SpriteController.instance.GetSprite("StoneAgeHut"),
				"Gathers Resources",
				true,
				2,
				0,
				3,
				0,
				1,
				1,
				0
			));
		stoneAgeList.Add (cardDictionary ["Hunting Camp"]);

		cardDictionary.Add(
			"Oracles Hut",
			new Card(
				cardPrefab,
				CardType.Base,
				SubType.Structure,
				"Oracles Hut",
				SpriteController.instance.GetSprite("StoneAgeHut2"),
				"Does Research",
				true,
				2,
				0,
				3,
				0,
				1,
				0,
				2
			));
		stoneAgeList.Add (cardDictionary ["Oracles Hut"]);
		ironAgeList.Add (cardDictionary ["Oracles Hut"]);

		cardDictionary.Add (
			"Town Centre",
			new Card (
				cardPrefab,
				CardType.Base,
				SubType.Structure,
				"Town Centre",
				SpriteController.instance.GetSprite ("Town"),
				"Your base of\noperations",
				true,
				3,
				2,
				30,
				0,
				0,
				4,
				4
			));

		cardDictionary.Add (
			"Wheel",
			new Card (
				cardPrefab,
				CardType.Modifier,
				SubType.Tool,
				"Wheel",
				SpriteController.instance.GetSprite ("PrimitiveWheel"),
				"+1 speed",
				true,
				0, //range
				0, //attack
				0, //defence
				1, //speed
				2, //cost
				0, //resource gen
				0, //research gen
				"Mobile",
				"Wheeled"
			));
		ironAgeList.Add(cardDictionary["Wheel"]);

		cardDictionary.Add (
			"Peasant",
			new Card (
				cardPrefab,
				CardType.Base,
				SubType.Unit,
				"Peasant",
				SpriteController.instance.GetSprite ("Peasant"),
				"A step up\nfrom cave man",
				true,
				2, //range
				3, //attack
				4, //defence
				2, //speed
				4, //cost
				0, //gen
				0 //research
			));
		ironAgeList.Add(cardDictionary["Peasant"]);	
		middleAgeList.Add (cardDictionary ["Peasant"]);

		cardDictionary.Add (
			"Bow",
			new Card (
				cardPrefab,
				CardType.Modifier,
				SubType.Tool,
				"Bow",
				SpriteController.instance.GetSprite ("Bow"),
				"+1 Range\n+3 Attack",
				true,
				1, //range
				3, //attack
				0, //defence
				0, //speed
				2, //cost
				0, //gen
				0, //research
				"Archery",
				"Archer"
			));
		ironAgeList.Add(cardDictionary["Bow"]);	

		cardDictionary.Add (
			"Mushrooms",
			new Card (
				cardPrefab,
				CardType.Modifier,
				SubType.Material,
				"Mushrooms",
				SpriteController.instance.GetSprite ("Mushrooms"),
				"+1 Attack\n-1 Defence\n +1 Speed",
				true,
				0, //range
				1, //attack
				-1, //defence
				1, //speed
				0, //cost
				0, //gen
				0, //research
				"Swampy",
				"High"
			));
		ironAgeList.Add(cardDictionary["Mushrooms"]);	

		cardDictionary.Add (
			"Farm",
			new Card (
				cardPrefab,
				CardType.Base,
				SubType.Structure,
				"Farm",
				SpriteController.instance.GetSprite ("Farm"),
				"Generate more\nresources than a\nHunting Camp",
				true,
				1, //range
				0, //attack
				6, //defence
				0, //speed
				3, //cost
				2, //gen
				0 //research
			));
		ironAgeList.Add(cardDictionary["Farm"]);
		middleAgeList.Add (cardDictionary ["Farm"]);
		modernAgeList.Add (cardDictionary ["Farm"]);

		cardDictionary.Add (
			"Scout",
			new Card (
				cardPrefab,
				CardType.Base,
				SubType.Unit,
				"Scout",
				SpriteController.instance.GetSprite ("Malitian"),
				"Runs fast",
				true,
				1, //range
				1, //attack
				8, //defence
				4, //speed
				4, //cost
				0, //gen
				0 //research
			));
		ironAgeList.Add(cardDictionary["Scout"]);
		middleAgeList.Add (cardDictionary ["Scout"]);

		cardDictionary.Add (
			"Sword",
			new Card (
				cardPrefab,
				CardType.Modifier,
				SubType.Tool,
				"Sword",
				SpriteController.instance.GetSprite ("IronSword"),
				"+5 attack",
				true,
				10, //range
				5, //attack
				0, //defence
				0, //speed
				4, //cost
				0, //gen
				0, //research
				"Sharp",
				"Sworded"
			));
		ironAgeList.Add(cardDictionary["Sword"]);

		cardDictionary.Add (
			"Library",
			new Card (
				cardPrefab,
				CardType.Base,
				SubType.Structure,
				"Library",
				SpriteController.instance.GetSprite ("Library"),
				"A  bit smarter\nthan an oracle",
				true,
				0, //range
				0, //attack
				8, //defence
				0, //speed
				8, //cost
				0, //gen
				6 //research
			));
		ironAgeList.Add (cardDictionary["Library"]);
		modernAgeList.Add (cardDictionary["Library"]);

		cardDictionary.Add (
			"Knight",
			new Card (
				cardPrefab,
				CardType.Base,
				SubType.Unit,
				"Knight",
				SpriteController.instance.GetSprite ("MedivalKnight"),
				"Slow, but\npowerful",
				true,
				1, //range
				5, //attack
				12, //defence
				1, //speed
				10, //cost
				0, //gen
				0//research
			));
		middleAgeList.Add (cardDictionary["Knight"]);

		cardDictionary.Add (
			"Crossbow",
			new Card (
				cardPrefab,
				CardType.Modifier,
				SubType.Tool,
				"Crossbow",
				SpriteController.instance.GetSprite ("Crossbow"),
				"+2 Range\n+5 Damage\n-1 Defence",
				true,
				2, //range
				5, //attack
				-1, //defence
				0, //speed
				12, //cost
				0, //gen
				0, //research
				"Heavy",
				"Crossbow"
			));
		middleAgeList.Add (cardDictionary["Crossbow"]);

		cardDictionary.Add (
			"Improved Wheel",
			new Card (
				cardPrefab,
				CardType.Modifier,
				SubType.Tool,
				"Improved Wheel",
				SpriteController.instance.GetSprite ("Wheel2"),
				"+2 Speed",
				true,
				0, //range
				0, //attack
				0, //defence
				2, //speed
				9, //cost
				0, //gen
				0, //research
				"Caravan",
				"Speedy"
			));
		middleAgeList.Add (cardDictionary["Improved Wheel"]);

		cardDictionary.Add (
			"WatchTower",
			new Card (
				cardPrefab,
				CardType.Base,
				SubType.Structure,
				"WatchTower",
				SpriteController.instance.GetSprite ("WatchTower"),
				"A stationary\ndefence",
				true,
				3, //range
				5, //attack
				15, //defence
				0, //speed
				14, //cost
				0, //gen
				0 //research
			));
		middleAgeList.Add (cardDictionary["WatchTower"]);

		cardDictionary.Add (
			"Soldier",
			new Card (
				cardPrefab,
				CardType.Base,
				SubType.Unit,
				"Soldier",
				SpriteController.instance.GetSprite ("MedivalSolider"),
				"Slow, but\npowerful",
				true,
				1, //range
				5, //attack
				8, //defence
				2, //speed
				12, //cost
				0, //gen
				0//research
			));
		middleAgeList.Add (cardDictionary["Soldier"]);
		
		cardDictionary.Add (
			"Claymore",
			new Card (
				cardPrefab,
				CardType.Modifier,
				SubType.Tool,
				"Claymore",
				SpriteController.instance.GetSprite ("Claymore"),
				"+1 Range\n+7 Attack",
				true,
				1, //range
				6, //attack
				-1, //defence
				0, //speed
				14, //cost
				0, //gen
				0, //research
				"Serrated",
				"Big-Sword"
			));
		middleAgeList.Add (cardDictionary["Claymore"]);

		cardDictionary.Add (
			"Stone",
			new Card (
				cardPrefab,
				CardType.Modifier,
				SubType.Material,
				"Stone",
				SpriteController.instance.GetSprite ("Stone"),
				"+2 Defence",
				true,
				0, //range
				0, //attack
				2, //defence
				0, //speed
				1, //cost
				0, //gen
				0, //research
				"Stone",
				"Rock Hard"
			));
		stoneAgeList.Add (cardDictionary["Stone"]);

		cardDictionary.Add (
			"Scale Armor",
			new Card (
				cardPrefab,
				CardType.Modifier,
				SubType.Material,
				"Scale Armor",
				SpriteController.instance.GetSprite ("ScaleArmour"),
				"+4 Defence",
				true,
				0, //range
				0, //attack
				4, //defence
				0, //speed
				3, //cost
				0, //gen
				0, //research
				"Scaly",
				"Armored"
			));
		ironAgeList.Add (cardDictionary["Scale Armor"]);

		cardDictionary.Add (
			"Plate Armor",
			new Card (
				cardPrefab,
				CardType.Modifier,
				SubType.Material,
				"Plate Armor",
				SpriteController.instance.GetSprite ("FullPlateArmour"),
				"+7 Defence",
				true,
				0, //range
				0, //attack
				7, //defence
				0, //speed
				10, //cost
				0, //gen
				0, //research
				"Plated",
				"Encased"
			));
		middleAgeList.Add (cardDictionary["Plate Armor"]);



	}


}
