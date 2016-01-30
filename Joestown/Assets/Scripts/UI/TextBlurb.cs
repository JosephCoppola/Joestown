using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBlurb : MonoBehaviour {

	private string[] messages;

	[SerializeField]
	private GameObject advanceText;

	//Pass an array for multiple messages
	public void InitBlurb(string[] p_messages)
	{
		messages = p_messages;

		if(messages.Length > 1)
		{

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
