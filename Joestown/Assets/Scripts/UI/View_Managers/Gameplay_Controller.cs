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
	private ChangeRoom_Controller changeRoomController;
	public ChangeRoom_Controller ChangeRoomController
	{
		get { return changeRoomController;}
	}
	
	[SerializeField]
	private SpriteRenderer buildingRoof;
	[SerializeField]
	private BuildingScript buildingScript;

	private Canvas ui_Canvas;
	private RectTransform uiCanvasRect;

	void Awake()
	{
		EventManager.AddEventListener("OnResize", UpdateBuildButton);
	}

	public void InitGameplayController(Canvas p_UICanvas)
	{
		ui_Canvas = p_UICanvas;
		uiCanvasRect = ui_Canvas.GetComponent<RectTransform>();
		UpdateBuildButton();
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
		if(ui_Canvas != null || uiCanvasRect != null)
		{
			Vector3 canvasPosition = ExtensionMethods.GetCanvasSpaceFromWorld(buildingRoof.gameObject, uiCanvasRect, ui_Canvas);
			buildButton.transform.parent.GetComponent<RectTransform>().position = new Vector3(canvasPosition.x, canvasPosition.y + 30.0f, canvasPosition.z);
		}

		//Check to grey out
	}

	public void BuildButtonSelect()
	{
		LerpScale buttonHover = buildButton.gameObject.AddComponent<LerpScale>();
		buttonHover.maxScale = 1.2f;
		buttonHover.minScale = 1.0f;
	}

	public void BuildButtonDeselect()
	{
		Destroy(buildButton.gameObject.GetComponent<LerpScale>());
	}

	public void BuildButtonClick()
	{
		buildingScript.AddFloor();
		UpdateBuildButton();
	}

}
