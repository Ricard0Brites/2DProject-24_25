using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
	public int SaveIndex = -1;

	private void Start()
	{
		Button ButtonComponent = GetComponent<Button>();
		if(ButtonComponent)
		{
			ButtonComponent.onClick.AddListener(delegate
			{
				if (SaveIndex > -1)
					SaveSystem.LoadGame(SaveIndex);
			});
		}
	}
}
