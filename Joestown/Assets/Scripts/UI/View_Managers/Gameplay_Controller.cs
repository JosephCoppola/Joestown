using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gameplay_Controller : MonoBehaviour {

	[SerializeField]
	private Slider faithBar;
	[SerializeField]
	private Slider notorietyBar;
	[SerializeField]
	private Text memberCount;

	// Use this for initialization
	void Start () {

	}

	//Accepts a value between 0 and 1, sets the faith bar value
	public void SetFaithAmount(float faithAmount)
	{
		faithBar.value = faithAmount;
	}

	//Accepts a value between 0 and 1, sets the faith bar value
	public void SetNotorietyAmount(float notorietyAmount)
	{
		notorietyBar.value = notorietyAmount;
	}

	//Sets the member count text
	public void SetMemberCount(int currentMembers, int maxMembers)
	{
		memberCount.text = currentMembers + "/" + maxMembers;
	}
}
