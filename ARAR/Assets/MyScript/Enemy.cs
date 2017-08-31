using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	public enum EnemyState
	{
		Idle,
		MovingOutPortal,
		MovingTowradsCrystal,
		Attacking
	}
	public EnemyState currentState;
	public float moveOutPortalTime;
	public float moveOutPortalSpeed;
	public float moveTowardsCrystalSpeed;
	public float stopDistance;

	private Animator anim;
	private float moveOutPortalTimer;
	private float currentSpeed;
	private Vector3 faceDir;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void OnEnable()
	{
		currentState = EnemyState.Idle;
	}

	void Update()
	{
		IdleStateUpdate();
		MoveOutPortalStateUpdate();
		MoveTowardsCrystalStateUpdate();
		AttackingStateUpdate();

		Move();
	}

	void Move()
	{
		if(currentState == EnemyState.MovingOutPortal || currentState == EnemyState.MovingTowradsCrystal)
		{
			transform.position += faceDir * currentSpeed * Time.deltaTime;
		}
	}

	void IdleStateUpdate()
	{
		if(currentState != EnemyState.Idle)
			return;

		anim.SetBool("isWalking", true);
		currentState = EnemyState.MovingOutPortal;
		transform.LookAt(transform.position + faceDir);
	}

	void MoveOutPortalStateUpdate()
	{
		if(currentState != EnemyState.MovingOutPortal)
			return;
		
		moveOutPortalTimer += Time.deltaTime;
		if(moveOutPortalTimer >= moveOutPortalTime)
		{
			currentSpeed = moveTowardsCrystalSpeed;
			Vector3 diffVec = GameManager.Instance.crystal.transform.position - transform.position;
			faceDir = diffVec.normalized;
			currentState = EnemyState.MovingTowradsCrystal;
			transform.LookAt(transform.position + faceDir);
		}
	}

	void MoveTowardsCrystalStateUpdate()
	{
		if(currentState != EnemyState.MovingTowradsCrystal)
			return;

		float distance = Vector3.Distance(transform.position, GameManager.Instance.crystal.transform.position);
		if(distance <= stopDistance)
		{
			anim.SetBool("isWalking", false);
			anim.SetBool("isAttacking", true);
			currentState = EnemyState.Attacking;
		}
	}

	void AttackingStateUpdate()
	{
		if(currentState != EnemyState.Attacking)
			return;
		
	}

	///reset enemy when spawn it
	public void Reset(Vector3 _initFaceDir)
	{
		transform.localPosition = Vector3.zero;
		faceDir = _initFaceDir;

		moveOutPortalTimer = 0.0f;
		currentSpeed = moveOutPortalSpeed;
	}
}
