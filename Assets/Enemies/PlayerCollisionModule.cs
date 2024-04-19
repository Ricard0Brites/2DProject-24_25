using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisionModule : MonoBehaviour
{
	[SerializeField] private float _PlayerKillAngle = 45.0f;
	[SerializeField] private float _invunerabilityTime = 2.0f;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Player"))
		{
			if(Vector3.Angle(Vector3.up, (collision.collider.transform.position) - transform.position) > _PlayerKillAngle / 2)
			{
				//Damage Player
				Player PlayerReference = collision.gameObject.GetComponent<Player>();
				if (PlayerReference)
				{
					PlayerReference.DamagePlayer();
					PlayerReference.TriggerPlayerDamageReaction(collision.otherCollider.transform.position - collision.transform.position);
					StartCoroutine(Invunerability(collision.collider));
				}
			}
			else
			{
				//Kill self (Enemy)
				if (gameObject)
					Destroy(gameObject);
			}
		}
	}
	
	private IEnumerator Invunerability(Collider2D Collider)
	{
		SpriteRenderer SR = GetComponent<SpriteRenderer>();
		if (SR)
			SR.color = new Color(0, 0, 0, 1);

		int OriginalReceiveMask = 0, OriginalSendMask = 0, OriginalExcludeMask = 0;
		if (Collider)
		{
			if (Collider.forceReceiveLayers == -1)
			{
				OriginalReceiveMask = Collider.forceReceiveLayers;
				Collider.forceReceiveLayers = (1 << gameObject.layer) ^ 0x7FFFFFFF; // 0x7FFFFFFF is a full signed int 
			}
			else
			{
				OriginalReceiveMask = Collider.forceReceiveLayers;
				Collider.forceReceiveLayers &= ~(1 << gameObject.layer);
			}

			if (Collider.forceSendLayers == -1)
			{
				OriginalSendMask = Collider.forceSendLayers;
				Collider.forceSendLayers = (1 << gameObject.layer) ^ 0x7FFFFFFF; // 0x7FFFFFFF is a full signed int 
			}
			else
			{
				OriginalSendMask = Collider.forceSendLayers;
				Collider.forceSendLayers &= ~(1 << gameObject.layer);
			}

			if (Collider.excludeLayers == -1)
			{
				OriginalExcludeMask = Collider.excludeLayers;
				Collider.excludeLayers = (1 << gameObject.layer) ^ 0x7FFFFFFF;
			}
			else
			{
				OriginalExcludeMask = Collider.excludeLayers;
				Collider.excludeLayers |= (1 << gameObject.layer);
			}
		}

		yield return new WaitForSeconds(_invunerabilityTime);

		if (SR)
			SR.color = new Color(1, 0, 0, 1);
		if (Collider)
		{
			Collider.forceReceiveLayers = OriginalReceiveMask;
			Collider.forceSendLayers = OriginalSendMask;
			Collider.excludeLayers = OriginalExcludeMask;
		}
	}
}