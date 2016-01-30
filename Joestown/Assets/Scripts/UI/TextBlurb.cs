using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBlurb : MonoBehaviour {

	private string[] messages;
	private int currentMessageIndex;

	[SerializeField]
	private GameObject advanceArrow;

	//Pass an array for multiple messages
	public void InitBlurb(string[] p_messages)
	{
		messages = p_messages;

		if(messages.Length > 1)
		{
			currentMessageIndex = 0;
		}
		else
		{
			advanceArrow.SetActive(false);
		}
	}

	public void AdvanceMessage()
	{
		Debug.Log ("Clicked");

	}
}
