using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeRoom_Controller : MonoBehaviour {

	[SerializeField]
	private DisplayScreen display_Controller;
	public DisplayScreen Display_Controller
	{
		get{ return display_Controller;}
	}
	[SerializeField]
	private Button defaultButton;
	[SerializeField]
	private Button worshipButton;
	[SerializeField]
	private Button housingButton;

	private RoomScript selectedRoom;

	public void SelectRoom(RoomScript room)
	{
		selectedRoom = room;

		if(room.roomType == RoomScript.RoomType.DEFAULT)
		{
			defaultButton.interactable = false;
		}
		else if(room.roomType == RoomScript.RoomType.WORSHIP)
		{
			worshipButton.interactable = false;
		}
		else if(room.roomType == RoomScript.RoomType.HOUSING)
		{
			housingButton.interactable = false;
		}
	}

	public void ChangeRoom(int selection)
	{
		selectedRoom.roomType = (RoomScript.RoomType)selection;
		display_Controller.SetScreenView(false);
	}

	void OnDisable()
	{
		defaultButton.interactable = true;
		worshipButton.interactable = true;
		housingButton.interactable = true;
	}
}
