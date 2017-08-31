using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARCat : MonoBehaviour, ITrackableEventHandler
{
	public Transform cameraPareng;
	public float moveSpeed;
	public bool instantTurning;
	public float rotateSpeed;
	public float fireInterval;

	public enum CatState{
		Idle,
		Moving
	}
	public CatState currentState;

	private TrackableBehaviour tb;
	private Animator anim;
	private bool isFound;
	private Vector3 currentLook;
	private float fireTimer;
	private bool fireReady;

	void Start()
	{
		tb = GetComponentInParent<TrackableBehaviour>();
		if(tb)
		{
			tb.RegisterTrackableEventHandler(this);
		}

		anim = GetComponent<Animator>();
		isFound = false;
		currentState = CatState.Idle;
		fireTimer = 0f;
		fireReady = false;
	}

	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}

	void OnTrackingFound()
	{
		isFound = true;
	}

	void OnTrackingLost()
	{
		isFound = false;
	}

	void Update()
	{
		if(!isFound)
			return;

		Vector3 lookPoint = Vector3.zero;
		#if UNITY_5_6_OR_NEWER
		lookPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraPareng.position.y));
		#else
		lookPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, cameraPareng.position.y));
		#endif

		LookMouse(new Vector3(lookPoint.x, 0f, lookPoint.z));
		CountFire();

		if(Input.GetMouseButton(0))
		{
			Move();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			QuitMoving();
		}

		if(Input.GetButton("Attack"))
		{
			Fire();
		}
	}

	void LookMouse(Vector3 wpos)
	{
		if(!instantTurning)
		{
			currentLook = transform.position + transform.forward * 1f;
			currentLook = new Vector3(currentLook.x, 0f, currentLook.z);

			Vector3 frameLook = Vector3.MoveTowards(currentLook, wpos, rotateSpeed * Time.deltaTime);
			transform.LookAt(frameLook);
		}else
		{
			transform.LookAt(wpos);
		}

	}

	void CountFire()
	{
		if(fireTimer < fireInterval)
		{
			fireTimer += Time.deltaTime;
		}
		else
		{
			if(!fireReady)
				fireReady = true;	
		}
	}

	void Move()
	{
		if(currentState != CatState.Moving)
			currentState = CatState.Moving;
		
		anim.SetBool("isRunning", true);
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}

	void QuitMoving()
	{
		currentState = CatState.Idle;
		anim.SetBool("isRunning", false);
	}

	void Fire()
	{
		if(!fireReady)
			return;
		
		LoveBullet lb = GameManager.Instance.playerBulletPool.GetPooledObj().GetComponent<LoveBullet>() as LoveBullet;
		lb.transform.position = transform.position;
		lb.SetDirection(this.transform.forward);

		fireTimer = 0.0f;
		fireReady = false;
	}
}
