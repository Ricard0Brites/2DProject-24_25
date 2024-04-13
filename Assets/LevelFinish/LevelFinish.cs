using System;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
	[SerializeField] private ECollectibles NecessaryObjects = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Player PlayerObject = collision.gameObject.GetComponent<Player>();
		if(PlayerObject)
		{
			if ((NecessaryObjects & PlayerObject.GetCurrentItems()) == NecessaryObjects)
			{
				Debug.Log("Valid");
			}
			else
				Debug.Log("Invalid");
		}
	}
}
