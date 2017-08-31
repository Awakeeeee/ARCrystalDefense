using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPortal : MonoBehaviour 
{
	public float startDelay;
	public float spawnInterval;

	private float spawnTimer;

	void Start()
	{
		spawnTimer = 0.0f;
		StartCoroutine(Spawning());
	}

	IEnumerator Spawning()
	{
		yield return new WaitForSeconds(startDelay);

		while(true)
		{
			spawnTimer += Time.deltaTime;
			if(spawnTimer >= spawnInterval)
			{
				Enemy newEnemy = GameManager.Instance.enemyPool.GetPooledObj().GetComponent<Enemy>() as Enemy;
				newEnemy.transform.SetParent(this.transform);
				newEnemy.Reset(-transform.forward);
				spawnTimer = 0.0f;
			}

			yield return null;
		}
	}
}
