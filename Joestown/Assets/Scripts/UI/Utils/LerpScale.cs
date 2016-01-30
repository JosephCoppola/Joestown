using UnityEngine;
using System.Collections;

public class LerpScale : MonoBehaviour {

	public float minScale;
	public float maxScale;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static class ExtensionMethods {
		public static float Remap (this float value, float from1, float to1, float from2, float to2) {
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}
	}
}
