using System;
using UnityEngine;

public class Throwable : MonoBehaviour
{
	[SerializeField] private float _throwableLaunchingForce = 1000.0f, _killY = -100f;    

	[NonSerialized] public GameObject Parent;

	private void Update()
	{
		if (transform.position.y < _killY)
			Destroy(gameObject);
	}
	public float GetThrowableLaunchForce() { return _throwableLaunchingForce; }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Player PlayerReference;
		if (collision.gameObject == Parent || !collision.TryGetComponent<Player>(out PlayerReference))
			return;

		if (collision.CompareTag("Player"))
		{
			PlayerReference.DamagePlayer();
			PlayerReference.TriggerPlayerDamageReaction(collision.transform.position - transform.position);
		}
		//TODO: Playe Animation Here?

		Destroy(gameObject);
	}
}
