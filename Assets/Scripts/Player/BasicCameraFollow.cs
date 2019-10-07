using UnityEngine;
using System.Collections;

public class BasicCameraFollow : MonoBehaviour 
{

	private Vector3 StartingPosition;
	public Transform FollowTarget;
	private Vector3 TargetPos;
	public float MoveSpeed;
	
	void Start()
	{
		StartingPosition = transform.position;
	}

	void Update () 
	{
		if (FollowTarget != null)
		{
			TargetPos = new Vector3(FollowTarget.position.x, FollowTarget.position.y, transform.position.z);
			Vector3 velocity = (TargetPos - transform.position) * MoveSpeed;
			transform.position = Vector3.SmoothDamp (transform.position, TargetPos, ref velocity, 1.0f, Time.deltaTime);
		}
	}
}

