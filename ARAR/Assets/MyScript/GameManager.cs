using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
	public ARCat player;
	public Crystal crystal;
	public bool isARing;

	public ObjectPool enemyPool;
	public ObjectPool playerBulletPool;
	public ObjectPool hitEffectPool;
	public ObjectPool deathEffectPool;

	void Start()
	{
		isARing = false;
	}
}
