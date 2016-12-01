using UnityEngine;
using System.Collections.Generic;

public enum TileType{Grass = 0,Hill = 1,Forest = 2,Mountain = 3}

public class Tile : MonoBehaviour {


    //might be better to manually declare which card is placed on the tile
    // - when card is selected, and tile is clicked, assign selected card as occupant

    [SerializeField]
    public Unit m_occupant;

    public Sprite[] m_sprites;
    public SpriteRenderer m_spriteRendChild;

    public Tile m_top;
    public Tile m_bottom;
    public Tile m_left;
    public Tile m_right;

    public Tile m_topRight;
    public Tile m_topLeft;
    public Tile m_bottomRight;
    public Tile m_bottomLeft;

	int m_tileType;

    public Collider2D m_col2D;

    //for movement costs, units can have a passive bonus that increases/decreases their
    //movement speed based on the terrain they are on
	void Awake ()
    {
        m_spriteRendChild = GetComponentInChildren<SpriteRenderer>();
        m_col2D = GetComponent<Collider2D>();
	}

	void Start()
	{
		//m_tileType = 0;
	}

    //needs to be called when the tile is clicked
    public bool IsOccupied()
    {
        if (m_occupant != null)
        {
            return true;
        }
        return false;
    }

	public SubType GetOccupantType()
	{
		return m_occupant.GetComponent<Unit> ().M_cardStats.GetSubType;
	}

    public void SetCellNeighbour(int _position, Tile _neighbour)
    {
        //top
        if(_position == 0)
        {
            m_top = _neighbour;
        }
        //bottom
        if (_position == 1)
        {
            m_bottom = _neighbour;
        }
        //left
        if (_position == 2)
        {
            m_left = _neighbour;
        }
        //right
        if (_position == 3)
        {
            m_right = _neighbour;
        }

        //top left
        if (_position == 4)
        {
            m_topLeft = _neighbour;
        }
        //bottom right
        if (_position == 5)
        {
            m_bottomRight = _neighbour;
        }
        //top right
        if (_position == 6)
        {
            m_topRight = _neighbour;
        }
        //bottom left
        if (_position == 7)
        {
            m_bottomLeft = _neighbour;
        }
    }

    public void SetTileType(int _type)
    {
        m_spriteRendChild.sprite = m_sprites[_type];
		m_tileType = _type;

        //types will have movement costs
        if (_type == 0)
        {
            //grass
        }
        if (_type == 1)
        {
            //hill
        }
        if (_type == 2)
        {
            //forest
        }
        if (_type == 3)
        {
            //mountain
        }
    }

	public float GetTileType()
	{
		return m_tileType;
	}

    public void SetTilePosition(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

	public void AttackCheck(int distanceAway,List<Tile>attackList)
	{
		if (distanceAway <= 0)
			return;

		for (int i = 0; i <= 7; i++) 
		{
			if (GetNeighbour (i) != null) {
				Tile tempTile = GetNeighbour (i);

				if (tempTile.IsOccupied () && TurnManager.instance.enemy.ownedUnits.Contains (tempTile.m_occupant))
					attackList.Add (tempTile);

				tempTile.AttackCheck (distanceAway - 1, attackList);
			}
		}
	}

	public void CheckTiles(int distanceAway)
	{
		if (distanceAway < 0)
			return;
		else 
		{
			distanceAway -= 1;
			if (!IsOccupied () && !UnitManager.m_instance.validTiles.Contains (this)) {
				UnitManager.m_instance.validTiles.Add (this);
			}
			for (int i = 0; i <= 7; i++) 
			{
				if (GetNeighbour(i) != null)
				GetNeighbour (i).CheckTiles (distanceAway);
			}
		}
	}

	public void CheckMovement(int distanceAway, List<Tile> tileList)
	{
		if (distanceAway <= 0)
			return;
				
		for (int i = 0; i <= 7; i++) {
			if (GetNeighbour (i) != null) {

				Tile tempTile = GetNeighbour (i);
				if (!tempTile.IsOccupied () && tempTile.GetTileType () != 3) {
					if (!tileList.Contains (tempTile))
						tileList.Add (tempTile);
					tempTile.CheckMovement (distanceAway - 1, tileList);
				}
			}
		}
	}

	void OnMouseDown()
	{
		//if (CardController.instance.cardWasClicked == false) {
			//Debug.Log("Here");
			//return m_col2D.gameObject;
		if (UnitManager.m_instance.m_isInPlacement && UnitManager.m_instance.validTiles.Contains(this)) {
				//TODO mountains
				if (!IsOccupied ()) {
					m_occupant = UnitManager.m_instance.PlaceUnit (this);
					Debug.Log ("Tile Type: " + GetTileType ());
				}
			}

	}

    public GameObject GetClickedTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (m_col2D.OverlapPoint (mousePosition) && CardController.instance.cardWasClicked == false) {
				//Debug.Log("Here");
				//return m_col2D.gameObject;
				if (UnitManager.m_instance.m_isInPlacement) {
					//TODO mountains
					if (!IsOccupied ()) {
						m_occupant = UnitManager.m_instance.PlaceUnit (this);
					}
				}
			}
        }
        return null;
    }

    public Tile GetNeighbour(int _pos)
    {
        if (_pos == 0)
        {
            return m_top;
        }
        if (_pos == 1)
        {
            return m_bottom;
        }
        if (_pos == 2)
        {
            return m_left;
        }
        if (_pos == 3)
        {
            return m_right;
        }
        if (_pos == 4)
        {
            return m_topLeft;
        }
        if (_pos == 5)
        {
            return m_bottomRight;
        }
        if (_pos == 6)
        {
            return m_topRight;
        }
        if (_pos == 7)
        {
            return m_bottomLeft;
        }
        return null;
    }
}
