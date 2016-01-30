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

	// Use this for initialization
	void Start () {
		if(instance == null)
		{
			instance = this;
		}
	}
}
