using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropDownMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	bool m_isOver;
	Vector2 m_startPos, m_endPos;
	RectTransform m_rect;

	public float m_speed;
	float m_startTime;
	//float m_distance;

	void Start ()
	{
		m_startPos = new Vector2 (0, 150);
		m_endPos = new Vector2 (0, 0);
		m_rect = GetComponent<RectTransform> ();
		m_isOver = false;

		m_speed = 0.1f;
		m_startTime = 0f;
	}

	void Update()
	{
		MoveElement ();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log ("Enter");
		m_startTime = 0;
		m_isOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log ("Exit");
		m_startTime = 0;
		m_isOver = false;
	}

	void MoveElement()
	{
		if (m_isOver)
		{
			//lerp to new position (expanded menu)
			m_rect.anchoredPosition = Vector2.Lerp (m_startPos, m_endPos, m_startTime += (3f * Time.deltaTime));
		}
		else
		{
			//lerp back to original position (minimised menu)
			m_rect.anchoredPosition = Vector2.Lerp (m_endPos, m_startPos, m_startTime += (3f * Time.deltaTime));
		}
	}
}
