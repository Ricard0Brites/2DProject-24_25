using UnityEngine;
using UnityEngine.UI;
public class MatchManager : MonoBehaviour
{
    public static GameObject GameOverContainer = null;
    public static Text ScoreText = null;
    public static void OnPlayerWin(bool IsSecondaryPlayer)
    {
        ScoreKeeper SK = ScoreKeeper.Get();

        if(!SK)
            return;

        if(GameOverContainer)
            GameOverContainer.SetActive(true);

		Time.timeScale = 0;

        if(IsSecondaryPlayer)
            SK.PlayerTwoVictoryCount++;
        else
            SK.PlayerOneVictoryCount++;

		if (ScoreText)
			ScoreText.text = SK.PlayerOneVictoryCount.ToString() + " - " + SK.PlayerTwoVictoryCount.ToString();
	}
}
