using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MemberPanel_Controller : MonoBehaviour {

	[SerializeField]
	private Slider devotionSlider;
	[SerializeField]
	private Slider staminaSlider;
	[SerializeField]
	private Image skepticImage;

	private Canvas ui_Canvas;
	private RectTransform uiCanvasRect;

	private MemberScript selectedMember;
	private RectTransform rectTransfrom;

	public void InitMemberPanelController(Canvas p_UICanvas)
	{
		ui_Canvas = p_UICanvas;
		uiCanvasRect = ui_Canvas.GetComponent<RectTransform>();
		rectTransfrom = gameObject.GetComponent<RectTransform>();
	}

	public void DisplayPanel(MemberScript p_SelectedMember)
	{
		selectedMember = p_SelectedMember;
		UpdatePosition();
		gameObject.SetActive(true);
	}

	public void ClosePanel()
	{
		gameObject.SetActive(false);
	}

	private void UpdateSliderValues()
	{
		if(selectedMember != null)
		{
			devotionSlider.value = selectedMember.Devotion * .01f;
			staminaSlider.value = selectedMember.Stamina * .01f;

			if(selectedMember.Skeptical)
			{
				skepticImage.gameObject.SetActive(true);
			}
			else
			{
				skepticImage.gameObject.SetActive(false);
			}
		}
	}

	private void UpdatePosition()
	{
		if(ui_Canvas != null || uiCanvasRect != null)
		{
			Vector3 canvasPosition = ExtensionMethods.GetCanvasSpaceFromWorld(selectedMember.gameObject, uiCanvasRect, ui_Canvas);
			rectTransfrom.position = new Vector3(canvasPosition.x, canvasPosition.y + 10.0f, canvasPosition.z);
		}
	}

	void Update()
	{
		UpdateSliderValues();
		UpdatePosition();
	}
}
