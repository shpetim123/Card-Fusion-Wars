using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {

    Transform m_camTrans;
	float m_camSpeed;
	public float m_horizPosMax, m_vertPosMax;
	public float m_horizNegMax, m_vertNegMax;

	void Start ()
    {
        m_camTrans = GetComponent<Transform>();
		m_camSpeed = 10;

		ResetCam ();
	}

	public void ResetCam()
	{
		m_camTrans.position = new Vector3 (-0.95f, 2.3f, 0);

		//positive cap
		m_horizPosMax = transform.position.x + 5;
		m_vertPosMax = transform.position.y + 5;

		//negative cap
		m_horizNegMax = transform.position.x - 5;
		m_vertNegMax = transform.position.y - 5;
	}

    void Update()
    {
		PanCamera ();
    }

	void PanCamera()
	{
		//right mouse press
		if (Input.GetMouseButton (1))
		{
			Debug.Log (transform.position);
			transform.Translate(new Vector2(Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y")) * m_camSpeed * Time.deltaTime);
			//implement caps
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, m_horizNegMax, m_horizPosMax), Mathf.Clamp (transform.position.y, m_vertNegMax, m_vertPosMax), 0);
		}
	}
}
