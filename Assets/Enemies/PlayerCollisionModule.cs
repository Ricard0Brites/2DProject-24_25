using UnityEngine;

public class PlayerCollisionModule : MonoBehaviour
{
	[SerializeField]
	private float _PlayerKillAngle = 45.0f;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Player"))
		{
			Vector3 MyLocation = transform.position;
			Vector3 PlayerLocation = collision.collider.transform.position;

			// Acos((U.V)/(||U|| * ||V||)) = U ^ V
			//this formula does not return negative angle values but in this case its acceptable

			Vector3 U = Vector3.up; // Up Vector (can be hardcoded because the game will not need any z rotation)
			Vector3 V = PlayerLocation - MyLocation;
			float AngleOfCollision = Mathf.Acos(Vector3.Dot(U, V) / (GetVectorLength(U) * GetVectorLength(V))) * 57.2958f;

			if(AngleOfCollision > _PlayerKillAngle / 2)
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
	float GetVectorLength(Vector3 A)
	{
		return Mathf.Sqrt((A.x * A.x) + (A.y * A.y) + (A.z * A.z));
	}
}
