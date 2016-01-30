using UnityEngine;
using System.Collections;

public class CameraScaleScript : MonoBehaviour
{
	private int lastWidth;

	void Start ()
	{
		SetCameraScale ();
	}
	
	void Update ()
	{
		// Yeah, I know. Deal with it
		if (lastWidth != Screen.width)
		{
            SetCameraScale();
		}
	}

	private void SetCameraScale()
	{
		Camera cam = Camera.main;

		float ar = 1.0f * Screen.width / Screen.height;

		float size = 6.0f / ar * 0.5f;

		cam.orthographicSize = size;

		lastWidth = Screen.width;
	}
}
