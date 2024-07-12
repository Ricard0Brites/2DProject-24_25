using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper ScoreKeeperInstance {get; private set;}
	public static ScoreKeeper Get() {  return ScoreKeeperInstance; }
	private void Awake()
	{
		if(ScoreKeeperInstance)
		{
			Destroy(gameObject);
		}
		else
		{
			ScoreKeeperInstance = this;
			DontDestroyOnLoad(ScoreKeeperInstance);
		}
	}

	public void ResetScores() {  PlayerOneVictoryCount = 0; PlayerTwoVictoryCount = 0;}

	[NonSerialized] public int PlayerOneVictoryCount = 0, PlayerTwoVictoryCount = 0;
}
