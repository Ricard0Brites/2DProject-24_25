using System.Collections;
using UnityEngine;

public class SlidingPlatform : MonoBehaviour
{
    [SerializeField] private float _TravelUnits = 2.0f, _TravelSpeed = 0.1f, _FreezeTimeAtDirectionChange = 0.5f;
	[SerializeField] private bool _PauseAtEnds = true, _IsInTogglingMode = false;
	private Transform _oldPlayerParent = null;
	private GameObject _playerRef = null;
	private bool _PauseMovement = false;
	private float _StartLocation = 0.0f;
	private void Start()
	{
		_StartLocation = transform.position.x;
		transform.position -= Vector3.right * _TravelUnits;
		_TravelSpeed *= _IsInTogglingMode ? -1.0f : 1.0f;
	}
	private void FixedUpdate()
	{
		if (!_PauseMovement)
		{
			if (Mathf.Abs(transform.position.x - _StartLocation) > _TravelUnits)
			{
				_TravelSpeed *= -1.0f;
				_PauseMovement = _PauseAtEnds;
				if (!_IsInTogglingMode)
					StartCoroutine(UnfreezePlatform());
			}
			transform.position += Vector3.right * _TravelSpeed * Time.deltaTime;
		}
	}
	private IEnumerator UnfreezePlatform()
	{
		yield return new WaitForSeconds(_FreezeTimeAtDirectionChange);
		_PauseMovement = false;
	}
	public void ToggleSlidingPlatform()
	{
		if(_IsInTogglingMode && _PauseMovement)
			_PauseMovement = !_PauseMovement;
	}
	public bool IsPlatformMoving()
	{
		return !_PauseMovement;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Player"))
		{
			AttachPlayerToSelf(collision.collider.gameObject);
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			DetachPlayer();
		}
	}
	private void AttachPlayerToSelf(GameObject InPlayer)
	{
		if (!InPlayer)
			return;
		_playerRef = InPlayer;
		_oldPlayerParent = InPlayer.transform.parent;
		InPlayer.transform.parent = transform;
	}
	private void DetachPlayer()
	{
		if (!_playerRef)
			return;
		_playerRef.transform.parent = _oldPlayerParent;
	}
}
