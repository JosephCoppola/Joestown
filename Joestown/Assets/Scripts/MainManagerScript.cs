using UnityEngine;
using System.Collections;

public class MainManagerScript : MonoBehaviour
{
	public Transform hightlight;
	public Transform unassignedMemberArea;

	public LayerMask defaultRaycastLayerMask;
	public LayerMask onReleaseRaycastLayerMask;

	private MemberScript m_selectedMember;

	void Update ()
	{
		if( m_selectedMember != null )
		{
			if( Input.GetMouseButton( 0 ) )
			{
				HandleDrag();
			}
			else if( Input.GetMouseButtonUp( 0 ) )
			{
				HandleRelease();
			}
		}
		else if( Input.GetMouseButtonDown( 0 ) )
		{
			CheckClick();
		}
	}

	private void CheckClick()
	{
		GameObject clickedObj = CheckMouseOver( defaultRaycastLayerMask );

		if( clickedObj == null )
		{
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
			/*if( m_selectedMember != null )
			{
				AssignSelectedMember( hit.collider.gameObject.GetComponent<RoomScript>() );
			}
			else
			{
				// Select room
			}*/
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
			SendMemberToTown();
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
}
