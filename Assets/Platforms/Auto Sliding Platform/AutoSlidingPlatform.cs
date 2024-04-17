using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSlidingPlatform : MonoBehaviour
{
    [SerializeField] private float _TravelUnits = 2.0f, _TravelSpeed = 0.1f, _FreezeTimeAtDirectionChange = 0.5f;
	[SerializeField] private bool _PauseAtEnds = true;
	private bool _PauseMovement = false;
	private float _StartLocation = 0.0f;
	private void Start()
	{
		_StartLocation = transform.position.x;
	}
	void Update()
    {
		if(!_PauseMovement)
		{
			if (Mathf.Abs(transform.position.x - _StartLocation) > _TravelUnits)
			{
				_TravelSpeed *= -1.0f;
				_PauseMovement = _PauseAtEnds;
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
}
