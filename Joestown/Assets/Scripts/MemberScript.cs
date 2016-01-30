using UnityEngine;
using System.Collections;

public class MemberScript : MonoBehaviour
{
	private RoomScript m_assignedRoom;

	void Start ()
	{
	
	}

	void Update ()
	{
	
	}

	public void ChangeRoom( RoomScript newRoom )
	{
		if( m_assignedRoom != null )
		{
			m_assignedRoom.RemoveMember( this );
		}
		m_assignedRoom = newRoom;

		transform.position = newRoom.transform.position;
		transform.position += new Vector3 (Random.Range( -0.25f, 0.25f ), -0.05f, -1.0f );
	}
}
