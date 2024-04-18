using UnityEngine;

public class LateralMovement : MonoBehaviour
{
	
	[SerializeField]
	private float _MovementSpeed = 5.0f;
	[SerializeField]
	private string _WallTag = "Barrier";

	private bool _CanMove = true;
	private bool _Direction = true;

	private void ToggleMovementDirection() 
	{
		_Direction = !_Direction;
		//transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, _Direction ? 0 : 180, transform.rotation.eulerAngles.z));
		transform.right = -transform.right;
	}
	private void Move()
	{
		if (_CanMove)
			transform.position += new Vector3((_MovementSpeed * (_Direction ? 1 : -1)) * Time.deltaTime, 0, 0);
		// change with velocity RB
	}
	void FixedUpdate()
	{
		Move();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag(_WallTag))
		{
			ToggleMovementDirection();
		}
	}
}
