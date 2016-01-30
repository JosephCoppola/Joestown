using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TextBlurb : MonoBehaviour {

	private List<string> messages;
	private int currentMessageIndex;

	[SerializeField]
	private GameObject advanceArrow;
	[SerializeField]
	private Text message;

	//Pass an array for multiple messages
	public void InitBlurb(List<string> p_messages)
	{
		messages = p_messages;

		string initialMessage = messages [0];

		if(initialMessage != null)
		{
			message.text = initialMessage;
		}

		if(messages.Count > 1)
		{
			currentMessageIndex = 0;
		}
		else
		{
			currentMessageIndex = 0;
			advanceArrow.SetActive(false);
		}
	}

	public void AdvanceMessage()
	{
		currentMessageIndex++;

		if(currentMessageIndex == messages.Count)
		{
			Destroy(gameObject);
		}
		else
		{
			message.text = messages[currentMessageIndex];
		}
	}
}
