using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour
	where T : Component
{
	private static T instance;

	public static T Instance
	{
		get {
			if (!instance)
			{
				instance = FindObjectOfType<T>();
				if (!instance)
				{
					Debug.LogWarning("Create singleton error. No such object of in Scene." + "....." + typeof(T).ToString());
				}
			}
			return instance;
		}
	}
}
