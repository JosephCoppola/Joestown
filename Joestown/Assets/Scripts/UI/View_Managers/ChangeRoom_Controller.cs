using UnityEngine;
using System.Collections;

public class ChangeRoom_Controller : MonoBehaviour {

	[SerializeField]
	private DisplayScreen display_Controller;
	public DisplayScreen Display_Controller
	{
		get{ return display_Controller;}
	}

	private RoomScript selectedRoom;

	public void SelectRoom(RoomScript room)
	{
		selectedRoom = room;
	}

	public void ChangeRoom(int selection)
	{
		selectedRoom.roomType = (RoomScript.RoomType)selection;
		display_Controller.SetScreenView(false);
	}
}
