using UnityEngine;

public class LevelFinish : MonoBehaviour
{
	[SerializeField] private ECollectibles NecessaryObjects = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Player>(out Player _playerObject))
		{
			if ((NecessaryObjects & _playerObject.GetCurrentItems()) == NecessaryObjects)
			{
				Debug.Log("Valid");
			}
			else
				Debug.Log("Invalid");
		}
	}
}
