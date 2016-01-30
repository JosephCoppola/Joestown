using UnityEngine;
using System.Collections;

public class Pause_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void Pause()
	{
		gameObject.SetActive (true);
		Time.timeScale = 0.0f;
	}

	public void Resume()
	{
		gameObject.SetActive (false);
		Time.timeScale = 1.0f;
	}
}
