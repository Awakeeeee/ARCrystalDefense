using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour 
{
	public float fullHP;

	private float currentHP;
	private bool isDead;

	void Start()
	{
		
	}

	void OnEnable()
	{
		currentHP = fullHP;
		isDead = false;
	}

	void OnTriggerEnter(Collider other)
	{
		LoveBullet lb = other.GetComponent<LoveBullet>();
		if(lb != null)
		{
			TakeDamage(lb.damage);
		}
	}

	void TakeDamage(float damage)
	{
		if(isDead)
			return;
		
		currentHP -= damage;
		if(currentHP <= 0f)
		{
			currentHP = 0f;
			Die();
		}
		else
		{
			GameObject hitEffect = GameManager.Instance.hitEffectPool.GetPooledObj();
			hitEffect.transform.position = transform.position;
			hitEffect.SetActive(true);
		}
	}

	void Die()
	{
		isDead = true;

		GameObject dieEffect = GameManager.Instance.deathEffectPool.GetPooledObj();
		dieEffect.transform.position = transform.position;
		dieEffect.SetActive(true);

		this.gameObject.SetActive(false);
	}
}
