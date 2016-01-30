using UnityEngine;
using System.Collections;

public class MainManagerScript : MonoBehaviour
{
	public Transform hightlight;

	private MemberScript m_selectedMember;

	void Start ()
	{
	
	}

	void Update ()
	{
		if( Input.GetMouseButtonDown( 0 ) )
		{
			CheckClick();
		}
	}

	private void CheckClick()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		RaycastHit2D hit = Physics2D.Raycast( mousePos, Vector3.forward );

		if( hit.collider == null )
		{
			if( m_selectedMember != null )
			{
				DeselectMember();
			}

			// early return
			return;
		}

		if( hit.collider.tag == "Member" )
		{
			SelectMember( hit.collider.gameObject.GetComponent<MemberScript>() );
		}
		else if( hit.collider.tag == "Room" )
		{
			if( m_selectedMember != null )
			{
				AssignSelectedMember( hit.collider.gameObject.GetComponent<RoomScript>() );
			}
			else
			{
				// Select room
			}
		}
	}

	private void SelectMember( MemberScript selectedMember )
	{
		m_selectedMember = selectedMember;

		hightlight.gameObject.SetActive( true );
		hightlight.position = selectedMember.transform.position;
		hightlight.parent = selectedMember.transform;
	}

	private void DeselectMember()
	{
		m_selectedMember = null;

		hightlight.parent = null;
		hightlight.gameObject.SetActive( false );

		// Close UI
	}

	private void AssignSelectedMember( RoomScript room )
	{
		if( m_selectedMember != null && room.CanAssignMember() )
		{
			room.AssignMember( m_selectedMember );
			m_selectedMember.ChangeRoom( room );
			DeselectMember();
		}
	}
}
