using UnityEngine;

public class LateralMovement : MonoBehaviour
{
	
	[SerializeField]
	private float _movementSpeed = 5.0f;
	[SerializeField]
	private string _WallTag = "Barrier";

	private bool _canMove = true;
	private bool _direction = true;
	private Rigidbody2D _rB;

	private void Start()
	{
		_rB = GetComponent<Rigidbody2D>();
	}
	private void ToggleMovementDirection() 
	{
		_direction = !_direction;
		transform.right = -transform.right;
	}
	private void Move()
	{
		if (!_canMove)
			return;
		if(_rB)
			_rB.velocity = new Vector2((_movementSpeed * (_direction ? 1 : -1)), _rB.velocity.y);
	}
	private void Update()
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
