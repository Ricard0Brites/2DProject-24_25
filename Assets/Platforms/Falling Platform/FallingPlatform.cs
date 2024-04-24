using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
	[SerializeField] private float _timeUntilPlatformFalls = 2.0f;
	[SerializeField] private float _timePlatformFallsFor = 2.0f;
	[SerializeField] private float _timeUntilPlatformResets = 2.0f;
	[SerializeField] private float _maxMovementSpeed = 1.0f;
	private bool _isInactive = true, _shouldResetPosition;
	private Rigidbody2D _myRB = null;
	private Vector2 _initialLocation;
	private void Start()
	{
		_myRB = GetComponent<Rigidbody2D>();
		_initialLocation = transform.position;
	}
	private void FixedUpdate()
	{
		if (_shouldResetPosition)
		{
			transform.position += Vector3.up * Mathf.Clamp(_initialLocation.y - transform.position.y, -_maxMovementSpeed * Time.deltaTime, _maxMovementSpeed * Time.deltaTime);
			if (Mathf.Approximately(transform.position.y, _initialLocation.y))
			{
				_shouldResetPosition = false;
				transform.position = _initialLocation; // assures no data loss
				_isInactive = true;
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Player"))
		{
			if(_isInactive)
			{
				StartCoroutine(PlatformTimer());
				_isInactive = false;
			}
		}
	}
	private IEnumerator PlatformTimer()
	{
		yield return new WaitForSeconds(_timeUntilPlatformFalls);

		if (_myRB)
			_myRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		
		yield return new WaitForSeconds(_timePlatformFallsFor);

		if (_myRB)
			_myRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

		yield return new WaitForSeconds(_timeUntilPlatformResets);

		//reset platform
		_shouldResetPosition = true;

	}
}
