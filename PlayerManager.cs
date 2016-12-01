using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance;

	public Hand p1Hand;
	public Hand p2Hand;

	public Player p1;
	public Player p2;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else {
			Debug.Log ("PlayerManager instance already found");
			Destroy (this);
		}
	}

	public void RemoveUnit(Unit _unit)
	{
		if (p1.ownedUnits.Contains (_unit))
			p1.ownedUnits.Remove (_unit);
		if (p2.ownedUnits.Contains (_unit))
			p2.ownedUnits.Remove (_unit);
		UnitManager.m_instance.m_units.Remove (_unit);
	}

	void Start()
	{
		p1 = new Player (p1Hand,5,"Player 1");
		p2 = new Player (p2Hand,1,"Player 2");

		HandManager.instance.currHand = p1.hand;
		HandManager.instance.SetupHand ();
		HandManager.instance.ChangeCurrentHand(p2.hand);
		HandManager.instance.SetupHand ();
	}

}
