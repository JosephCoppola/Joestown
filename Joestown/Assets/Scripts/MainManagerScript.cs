using UnityEngine;
using System.Collections;

public class MainManagerScript : MonoBehaviour
{
	public Transform hightlight;
	public Transform unassignedMemberArea;

	public BuildingScript building;

	public LayerMask defaultRaycastLayerMask;
	public LayerMask onReleaseRaycastLayerMask;

	private MemberScript m_selectedMember;

	private Vector3 m_upperBounds;
	private Vector3 m_lowerBounts;

	private float m_clickTime = 0.0f;
	private float m_scrollSpeed = 2.5f;

	private bool m_dragging = false;

	[SerializeField]
	private UI_Manager ui_Manager;

	public UI_Manager UI_Mngr
	{
		get
		{
			return ui_Manager;
		}
	}

	void Start()
	{
		EventManager.AddEventListener( "AddedFloor", OnFloorAdded );
		OnFloorAdded();

	}

	void Update ()
	{
		CheckBounds();

		if( Input.GetMouseButtonDown( 0 ) )
		{
			m_clickTime = Time.time;
			CheckClick();
		}
		else if( m_selectedMember != null )
		{
			if( Input.GetMouseButton( 0 ) && !m_dragging )
			{
				float clickLength = Time.time - m_clickTime;
				if( clickLength > 0.1f )
				{
					m_dragging = true;
				}
			}

			if( m_dragging )
			{
				HandleDrag();
			}

			if( Input.GetMouseButtonUp( 0 ) )
			{
				HandleRelease();
			}
		}
	}

	private void CheckClick()
	{
		if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		GameObject clickedObj = CheckMouseOver( defaultRaycastLayerMask );

		if( clickedObj == null )
		{
			if( m_selectedMember != null )
			{
				DeselectMember();
			}

			// Early return
			return;
		}

		if( clickedObj.tag == "Member" )
		{
			SelectMember( clickedObj.GetComponent<MemberScript>() );
            // open member ui
		}
		else if( clickedObj.tag == "Room" )
		{
			if( m_selectedMember != null )
			{
				DeselectMember();
			}

			RoomScript room = clickedObj.GetComponent<RoomScript>();
			if( room.Type != RoomScript.RoomType.SACRIFICE )
			{
				if(room.AssignedMembers.Count == 0)
				{
					ui_Manager.Gameplay_Controller.ChangeRoomController.Display_Controller.SetScreenView(true);
					ui_Manager.Gameplay_Controller.ChangeRoomController.SelectRoom(room);
				}
			}
			else
			{
				// open confirm sacrifice menu
			}
		}
	}

	private GameObject CheckMouseOver( LayerMask mask )
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		RaycastHit2D hit = Physics2D.Raycast( mousePos, Vector3.forward, 20, mask );

		if( hit.collider == null )
		{
			// early return
			return null;
		}

		return hit.collider.gameObject;
	}

	private void HandleDrag()
	{
		m_selectedMember.transform.position = Camera.main.ScreenToWorldPoint( Input.mousePosition ) + new Vector3( 0.0f, 0.0f, 10.0f );
	}

	private void HandleRelease()
	{
		if( !m_dragging )
		{
			m_selectedMember.SetDeselected();
			return;
		}

		m_dragging = false;
		GameObject overObj = CheckMouseOver( onReleaseRaycastLayerMask );

		if( overObj == null )
		{
			m_selectedMember.ResetPosition();
			DeselectMember();
			return;
		}

		if( overObj.tag == "Room" )
		{
			AssignSelectedMember( overObj.GetComponent<RoomScript>() );
		}
		else if( overObj.tag == "Town" )
		{
			if( unassignedMemberArea.childCount == 0 )
			{
				SendMemberToTown();
			}
			else
			{
				m_selectedMember.ResetPosition();
				DeselectMember();
			}
		}
	}

	private void SelectMember( MemberScript selectedMember )
	{
		m_selectedMember = selectedMember;
		selectedMember.SetSelected();

		hightlight.gameObject.SetActive( true );
		hightlight.position = selectedMember.transform.position;
		hightlight.parent = selectedMember.transform;
	}

	private void DeselectMember()
	{
		m_selectedMember.SetDeselected();
		m_selectedMember = null;

		hightlight.parent = null;
		hightlight.gameObject.SetActive( false );

		// Close UI
	}

	private void AssignSelectedMember( RoomScript room )
	{
		if( room.CanAssignMember() )
		{
			room.AssignMember( m_selectedMember );
			m_selectedMember.ChangeRoom( room );
			DeselectMember();
		}
		else
		{
			m_selectedMember.ResetPosition();
			DeselectMember();
		}
	}

	private void SendMemberToTown()
	{
		m_selectedMember.transform.parent = unassignedMemberArea;
		m_selectedMember.ResetPosition();

		m_selectedMember.GoToTown();
		DeselectMember();
	}

	private void CheckBounds()
	{
		Vector3 mousePos = Input.mousePosition;

		if( mousePos.y > Screen.height - Screen.height * 0.1f )
		{
			Camera.main.transform.position = Vector3.MoveTowards( Camera.main.transform.position, m_upperBounds, m_scrollSpeed * Time.deltaTime );
			ui_Manager.Gameplay_Controller.UpdateBuildButton();
		}
		else if( mousePos.y < Screen.height * 0.1f )
		{
			Camera.main.transform.position = Vector3.MoveTowards( Camera.main.transform.position, m_lowerBounts, m_scrollSpeed * Time.deltaTime );
			ui_Manager.Gameplay_Controller.UpdateBuildButton();
		}
	}

	private void OnFloorAdded()
	{
		m_upperBounds = new Vector3( 0, building.GetTopFloorY(), Camera.main.transform.position.z );
		m_lowerBounts = new Vector3( 0, building.GetBottomFloorY(), Camera.main.transform.position.z );
	}
}
