using UnityEngine;
using System.Collections;

public class LerpScale : MonoBehaviour {

	public float minScale;
	public float maxScale;
	
	// Update is called once per frame
	void Update () {
		float increment = Mathf.PingPong (Time.time*3, 1.2f);
		
		increment = increment.Remap (0.0f, 1.2f, minScale, maxScale);
		
		transform.localScale = new Vector3 (increment, increment, increment);
	}
}
