using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveBullet : MonoBehaviour 
{
	public float damage;
	public float speed;
	private Vector3 direction;

	void Update()
	{
		transform.position += direction * speed * Time.deltaTime;
	}

	public void SetDirection(Vector3 dir)
	{
		transform.LookAt(this.transform.position + dir);
		direction = dir;
	}
}
