using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
	[SerializeField]
	private ECollectibles _Item;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) //hardcoded because its a default tag and will not change unless unity version is updated
		{
			Player PlayerReference = collision.GetComponent<Player>();
			if (PlayerReference)
			{
				PlayerReference.TryAddItem((int)_Item);
				if(gameObject) 
					Destroy(gameObject);
			}
		}
	}
}
