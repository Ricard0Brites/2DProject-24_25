using UnityEngine;

public class PlayerCollisionModule : MonoBehaviour
{
	[SerializeField]
	private float _PlayerKillAngle = 45.0f;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(Vector3.Angle(Vector3.up, (collision.collider.transform.position) - transform.position));
		if(collision.collider.CompareTag("Player"))
		{
			if(Vector3.Angle(Vector3.up, (collision.collider.transform.position) - transform.position) > _PlayerKillAngle / 2)
			{
				//Kill Player
				if (collision.collider.gameObject)
					Destroy(collision.collider.gameObject);
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
