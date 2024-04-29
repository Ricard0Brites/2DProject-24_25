using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
	[SerializeField] private float _timeUntilPlayerCanBeDamagedAgain = 2.0f;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			Player PlayerReference = collision.GetComponent<Player>();
			if (PlayerReference)
			{
				if(!PlayerReference.IsInsideSpikes)
				{
					PlayerReference.IsInsideSpikes = true;
					PlayerReference.DamagePlayer();
				}
			}
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			Player PlayerReference = collision.GetComponent<Player>();
			if (PlayerReference)
			{
				StartCoroutine(ResetState(PlayerReference));
			}
		}
	}
	private IEnumerator ResetState(Player PlayerReference)
	{
		if (PlayerReference)
		{
			yield return new WaitForSeconds(_timeUntilPlayerCanBeDamagedAgain);
			PlayerReference.IsInsideSpikes = false;
		}
	}
}
