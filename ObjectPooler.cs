using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler m_current;
    public GameObject m_pooledObject;
    public int m_pooledAmount = 150;
    public bool m_willGrow = false;

    List<GameObject> m_pooledObjects;

    void Awake()
    {
        m_current = this;
    }

	void Start ()
    {
        m_pooledObjects = new List<GameObject>();
        for (int i = 0; i < m_pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(m_pooledObject);
            obj.SetActive(false);
            m_pooledObjects.Add(obj);
        }
	}

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < m_pooledObjects.Count; i++)
        {
            if (!m_pooledObjects[i].activeInHierarchy)
            {
                return m_pooledObjects[i];
            }
        }
        if (m_willGrow)
        {
            GameObject obj = (GameObject)Instantiate(m_pooledObject);
            m_pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }
}
