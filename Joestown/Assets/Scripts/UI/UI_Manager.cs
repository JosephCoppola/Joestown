using UnityEngine;
using System.Collections;

public class UI_Manager : MonoBehaviour {

	private static UI_Manager instance;
	public static UI_Manager Instance
	{
		get{ return instance;}
	}

	[SerializeField]
	private Pause_Controller pause_Controller;
	public Pause_Controller Pause_Controller
	{
		get { return pause_Controller;}
	}

	[SerializeField]
	private Gameplay_Controller gameplay_Controller;
	public Gameplay_Controller Gameplay_Controller
	{
		get { return gameplay_Controller; }
	}

	[SerializeField]
	private GameObject textBlurbPrefab;

	// Use this for initialization
	void Start () {
		if(instance == null)
		{
			instance = this;
		}
	}

	public void SpawnTextBlurb(string[] messages)
	{

	}
}

public static class ExtensionMethods {
	public static float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}
