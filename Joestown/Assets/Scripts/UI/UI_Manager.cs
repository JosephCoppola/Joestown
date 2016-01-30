using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UI_Manager : MonoBehaviour {

	[SerializeField]
	private Canvas ui_Canvas;
	private RectTransform canvasRectTransform;
	public Canvas UI_Canvas
	{
		get { return ui_Canvas; } 
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

	void Start()
	{
		canvasRectTransform = ui_Canvas.GetComponent<RectTransform>();
		gameplay_Controller.InitGameplayController(ui_Canvas);
	}

	public void SpawnTextBlurb(List<string> messages)
	{
		GameObject textBlurb = Instantiate (textBlurbPrefab) as GameObject;
		textBlurb.transform.SetParent (gameplay_Controller.gameObject.transform,false);
		TextBlurb textBlurbScript = textBlurb.GetComponent<TextBlurb> ();

		textBlurbScript.InitBlurb (messages);
	}
}

public static class ExtensionMethods {

	public static float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	public static Vector3 GetCanvasSpaceFromWorld(GameObject worldObject, RectTransform canvasRect, Canvas canvas)
	{
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint( Camera.main, worldObject.transform.position );      
		
		Vector3 worldPoint;
		if ( RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, screenPoint, canvas.worldCamera, out worldPoint ) )
		{          
			return worldPoint;
		}       
		
		return new Vector3 (0f,0f,0f);
	}
}
