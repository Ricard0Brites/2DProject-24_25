using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] List<GameObject> TutorialUI;
    [SerializeField] float ShowTutorialForSeconds = 25;

    void Awake()
    {
        //if there are no saves
        if(!(SaveSystem.GetNumberOfSaves() > 0))
        {
            //create save
            SaveSystem.Save(true);

            StartCoroutine(RemoveTutorial());
        }
        else
        {
            //check save for value
            SaveData Data = SaveSystem.GetSavedValues(0);
            if (Data.HasSeenTutorial)
            {
                DestroyTutorialElements();
            }
        }
    }

	private IEnumerator RemoveTutorial()
	{
        yield return new WaitForSeconds(ShowTutorialForSeconds);
        DestroyTutorialElements();
    }

    private void DestroyTutorialElements()
    {
		if (TutorialUI.Count > 0)
		{
			foreach (GameObject GO in TutorialUI)
			{
				if (GO)
				{
					while (GO.transform.childCount > 0)
						DestroyImmediate(GO.transform.GetChild(0).gameObject);
					Destroy(GO);
				}
			}
			TutorialUI.Clear();
		}
	}
}
