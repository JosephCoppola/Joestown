using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomScript : MonoBehaviour
{
	public int maxMembers = 4;

	public List<MemberScript> m_assignedMembers;

	void Start()
	{
		m_assignedMembers = new List<MemberScript>();
	}

	public void AssignMember( MemberScript memberToAssign )
	{
		if( m_assignedMembers.Count < maxMembers )
		{
			m_assignedMembers.Add( memberToAssign );
		}
	}

	public void RemoveMember( MemberScript memberToRemove )
	{
		m_assignedMembers.Remove( memberToRemove );
	}

	public bool CanAssignMember()
	{
		return m_assignedMembers.Count < maxMembers;
	}
}
