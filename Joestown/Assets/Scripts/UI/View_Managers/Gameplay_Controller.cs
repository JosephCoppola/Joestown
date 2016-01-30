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
	[SerializeField]
	private Button buildButton;

	[SerializeField]
	private GameObject buildingRoof;

	private Canvas ui_Canvas;
	private RectTransform uiCanvasRect;

	public void InitGameplayController(Canvas p_UICanvas)
	{
		ui_Canvas = p_UICanvas;
		uiCanvasRect = ui_Canvas.GetComponent<RectTransform>();
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

	public void UpdateBuildButton()
	{
		Vector3 canvasPosition = ExtensionMethods.GetCanvasSpaceFromWorld(buildingRoof, uiCanvasRect, ui_Canvas);
		buildButton.GetComponent<RectTransform>().position = new Vector3(canvasPosition.x, canvasPosition.y, canvasPosition.z);

		//Check to grey out
	}

	void Update()
	{
		UpdateBuildButton();
	}
}
