using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToggleButton : MonoBehaviour
{
	[SerializeField] GameObject PlatformToToggle = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player") && PlatformToToggle)
		{
			PlatformToToggle.GetComponent<SlidingPlatform>().ToggleSlidingPlatform();
		}
	}
}
