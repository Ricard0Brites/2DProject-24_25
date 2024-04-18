using UnityEngine;

public class PlayerCollisionModule : MonoBehaviour
{
	[SerializeField]
	private float _PlayerKillAngle = 45.0f;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Player"))
		{
			if(Vector3.Angle(Vector3.up, (collision.collider.transform.position) - transform.position) > _PlayerKillAngle / 2)
			{
				//Kill Player
				Player PlayerReference = collision.gameObject.GetComponent<Player>();
				if (PlayerReference)
					PlayerReference.DamagePlayer();
			}
			else
			{
				//Kill self (Enemy)
				if (gameObject)
					Destroy(gameObject);
			}
		}
	}
}
