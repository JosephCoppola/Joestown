using UnityEngine;
using System.Collections;

public class DisplayScreen : MonoBehaviour {

	public void SetScreenView(bool active)
	{
		gameObject.SetActive(active);
	}
}
