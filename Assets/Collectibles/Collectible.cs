using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
	[SerializeField] public ECollectibles Item;
	[SerializeField] public bool HasLifeCycle = false;
	[SerializeField] private float _lifeTime = 20f;

	private void Start()
	{
		if(HasLifeCycle)
		{
			StartCoroutine(InitiateLifeCycle());
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			Player PlayerReference = collision.collider.GetComponent<Player>();
			if (PlayerReference && !PlayerReference.HasThrowable)
			{
				PlayerReference.SetCollectibleCollisionLayer(1 << gameObject.layer);
				PlayerReference.TryAddItem(Item);
				if (gameObject)
					Destroy(gameObject);
			}
		}
	}
	private IEnumerator InitiateLifeCycle()
	{
		yield return new WaitForSeconds(_lifeTime);
		Destroy(gameObject);
	}
}
