using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
	public GameObject m_unitPrefab;
	public GameObject m_indicatorprefab;
	public GameObject m_enemyIndicatorPrefab;

	public GameObject endTurnButton;

	public bool m_isInPlacement;
	public List<Unit> m_units;
	List<GameObject> placementIndicatorList;
	public List<Tile> validTiles;
	public static UnitManager m_instance;

	public AudioClip attacksound;
	public AudioClip structureClick;
	public AudioClip unitClick;
	public AudioClip moveSound;
	public AudioClip playUnit;

	void Awake()
	{
		if (m_instance == null) {
			m_instance = this;
		} else {
			Debug.Log ("Instance Unit Manager already found");
			Destroy (this);
		}
	}

	public void RefreshUnits()
	{
		foreach (Unit u in m_units)
		{
			u.hasActed = false;
		}
	}


	public Unit CreateTownCentre(Tile homeTile, Player owner)
	{
		GameObject newUnitGO = (GameObject)Instantiate (m_unitPrefab, new Vector3(homeTile.transform.position.x,homeTile.transform.position.y,-1), Quaternion.identity);
		Unit newUnit = newUnitGO.GetComponent<Unit> ();
		newUnitGO.transform.SetParent (homeTile.gameObject.transform);
		newUnit.M_cardStats = CardController.instance.GetCardCopy ("Town Centre");
		newUnit.M_cardStats.CardObject.SetActive (false);
		newUnit.m_tile = homeTile;
		newUnit.m_tile.m_occupant = newUnit;
		newUnit.M_cardStats.InHand = false;
		m_units.Add (newUnit);
		owner.ownedUnits.Add (newUnit);
		if (owner.name == "Player 2") {
			newUnit.sr = newUnit.GetComponent<SpriteRenderer> ();
			newUnit.sr.sprite = SpriteController.instance.GetSprite ("CastlePieceRed");
		}
		return newUnit;
	}

	// Use this for initialization
	void Start ()
	{
		placementIndicatorList = new List<GameObject> ();
		m_units = new List<Unit> ();
		m_isInPlacement = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void FindValidTiles()
	{
		foreach (Unit u in TurnManager.instance.currPlayer.ownedUnits) {
			if (u.M_cardStats.GetSubType == SubType.Structure)
				u.m_tile.CheckTiles(u.M_cardStats.Range);
		}

		foreach (Tile t in validTiles) {
			GameObject go = (GameObject)Instantiate (m_indicatorprefab, t.transform.position, Quaternion.identity);
			go.transform.parent = t.transform;
			placementIndicatorList.Add(go);
		}
	}

	public Unit PlaceUnit(Tile _tile)
	{
		AudioManager.instance.Play (playUnit, 0.5f);
		GameObject newUnitGO = (GameObject)Instantiate (m_unitPrefab, new Vector3(_tile.transform.position.x,_tile.transform.position.y,-1), Quaternion.identity);
		Unit newUnit = newUnitGO.GetComponent<Unit> ();
		newUnitGO.transform.SetParent (_tile.gameObject.transform);
		newUnit.M_cardStats = CardFuser.instance.fusedCard;
		newUnit.M_cardStats.CardObject.SetActive (false);
		newUnit.m_tile = _tile;
		CardFuser.instance.fusedCard = null;
		this.m_isInPlacement = false;
		HandManager.instance.currHand.gameObject.SetActive (true);
		endTurnButton.SetActive (true);
		m_units.Add (newUnit);
		TurnManager.instance.currPlayer.ownedUnits.Add (newUnit);
		foreach (GameObject go in placementIndicatorList) {
			Destroy (go);
		}
		placementIndicatorList.Clear ();
		validTiles.Clear ();
		return newUnit;
	}
}