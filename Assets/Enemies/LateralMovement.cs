using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralMovement : MonoBehaviour
{
	#region Parameters
	[SerializeField]
	private float _MovementSpeed = 5.0f;
	[SerializeField]
	private string _WallTag = "Barrier";
	#endregion

	#region States
	private EMovementDirection _MovementDirection = EMovementDirection.Right;
	private bool _CanMove = true; 
	#endregion

	private void ToggleMovementDirection() 
	{
		_MovementDirection = _MovementDirection == EMovementDirection.Right ? EMovementDirection.Left : EMovementDirection.Right; 
	}
	private float GetDirectionAsFloat()
	{ 
		return _MovementDirection == EMovementDirection.Right ? 1.0f : -1.0f;
	}
	private void Move()
	{
		if(_CanMove)
			transform.position += new Vector3((_MovementSpeed * GetDirectionAsFloat()) * Time.deltaTime, 0, 0);
	}
	private void UpdateDirection()
	{
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, _MovementDirection == EMovementDirection.Right ? 0 : 180, transform.eulerAngles.z);
	}
	void FixedUpdate()
	{
		Move();
		UpdateDirection();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag(_WallTag))
		{
			ToggleMovementDirection();
		}
	}
}
