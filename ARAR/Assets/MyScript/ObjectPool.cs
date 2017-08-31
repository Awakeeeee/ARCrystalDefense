using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
	public GameObject obj;

	public int initSize;
	public bool canGrow;

	List<GameObject> pool;

	void Start()
	{
		pool = new List<GameObject>();

		for(int i = 0; i < initSize; i++)
		{
			GameObject newObj = Instantiate(obj, null) as GameObject;
			newObj.SetActive(false);
			pool.Add(newObj);
		}
	}

	public GameObject GetPooledObj(bool getInactiveObj = false)
	{
		foreach(GameObject g in pool)
		{
			if(!g.activeInHierarchy)
			{
				if(!getInactiveObj)
				{
					g.SetActive(true);	
				}

				return g;
			}
		}

		if(canGrow)
		{
			GameObject extraObj = Instantiate(obj, null);
			if(getInactiveObj)
			{
				extraObj.SetActive(false);
			}
			pool.Add(extraObj);
			return extraObj;
		}else{
			return null;
		}
	}

	public void UsePooledObj(Vector3 pos)
	{
		GameObject g = GetPooledObj();
		g.transform.position = pos;
	}
}
