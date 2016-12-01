using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tilemap : MonoBehaviour {


	//CardFuser
	//fusedCard

	public static Tilemap m_instance;

    [SerializeField]
    GameObject m_tile;

    List<GameObject> m_gridCells;

    List<GameObject> m_p1PlaceableTiles;
	List<GameObject> m_p2PlaceableTiles;


    int m_height, m_width;
    float m_tileOffset;

	void Awake()
	{
		if (m_instance == null) {
			m_instance = this;
		} else {
			Debug.Log ("Tilemap instance already found");
			Destroy (this);
		}
	}

	void Start ()
    {
        m_height = 13;
        m_width = 30;
        m_tileOffset = 0.5f;

        m_gridCells = new List<GameObject>();
        m_p1PlaceableTiles = new List<GameObject>();
		m_p2PlaceableTiles = new List<GameObject>();
        GenerateMap();  //call method
	}

    void GenerateMap()
    {
        GameObject cur_cell;
        GameObject other_cell;

        for (int y = 0; y < m_height; y++)
        {
            for (int x = 0; x < m_width; x++)
            {
                cur_cell = Instantiate(m_tile) as GameObject;
                Tile curCellScript = cur_cell.GetComponent<Tile>();

                //horizontal
                if (x > 0)
                {
                    other_cell = m_gridCells[m_gridCells.Count - 1];
                    Tile otherCellScript = other_cell.GetComponent<Tile>();

                    otherCellScript.SetCellNeighbour(3, curCellScript);
                    curCellScript.SetCellNeighbour(2, otherCellScript);

                    if (y > 0)
                    {
                        other_cell = m_gridCells[m_gridCells.Count - m_width - 1];
                        otherCellScript = other_cell.GetComponent<Tile>();
                        curCellScript = cur_cell.GetComponent<Tile>();

                        otherCellScript.SetCellNeighbour(6, curCellScript);
                        curCellScript.SetCellNeighbour(7, otherCellScript);
                    }
                }

                //vertical
                if (y > 0)
                {
                    other_cell = m_gridCells[m_gridCells.Count - m_width];
                    Tile otherCellScript = other_cell.GetComponent<Tile>();

                    otherCellScript.SetCellNeighbour(0, curCellScript);
                    curCellScript.SetCellNeighbour(1, otherCellScript);
                }

                if (y > 0 && y < m_height && x < m_width - 1)
                {
                    other_cell = m_gridCells[m_gridCells.Count - m_width + 1];
                    Tile otherCellScript = other_cell.GetComponent<Tile>();

                    otherCellScript.SetCellNeighbour(4, curCellScript);
                    curCellScript.SetCellNeighbour(5, otherCellScript);
                }



                curCellScript.SetTilePosition((x * m_tileOffset) + (m_tileOffset / 2), (y * m_tileOffset) - (m_tileOffset));
                curCellScript.SetTileType(Random.Range(0, 4));

				if (y == m_height / 2) {
					if (x == 0)
						UnitManager.m_instance.CreateTownCentre (curCellScript,PlayerManager.instance.p1);
					if (x == m_width - 1) {
						Unit u = UnitManager.m_instance.CreateTownCentre (curCellScript,PlayerManager.instance.p2);
					}
					}



                m_gridCells.Add(cur_cell);
                m_gridCells[m_gridCells.Count - 1].transform.SetParent(transform);
            }
        }
		this.GetComponent<CameraPan> ().ResetCam ();
    }
}
