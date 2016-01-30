using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomScript : MonoBehaviour
{
	public const float BASE_FAITH_GAIN = 1.0f;
	public const float BASE_NOTORIETY_GAIN = 1.0f;

    public enum RoomType
    {
        DEFAULT,
        WORSHIP,
        HOUSING
    }

    public RoomType roomType;
	public int maxMembers = 4;

	private List<MemberScript> m_assignedMembers;

	//private float m_currTime = 0.0f;
	//private float m_updateInterval = 1.0f;

    public RoomType Type
    {
        get
        {
            return roomType;
        }
    }

	void Start()
	{
		m_assignedMembers = new List<MemberScript>();
	}

	void Update()
	{
		UpdateStats();

		/*if( m_currTime >= m_updateInterval )
		{
			UpdateStats();
			m_currTime = 0;
		}

		m_currTime += Time.deltaTime;*/
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

	public void UpdateStats()
	{
		int count = m_assignedMembers.Count;
		switch( roomType )
		{
			case RoomType.DEFAULT:
				StatManager.Notoriety += -BASE_NOTORIETY_GAIN * count * Time.deltaTime;
				break;
			case RoomType.WORSHIP:
				StatManager.Faith += BASE_FAITH_GAIN * count * Time.deltaTime;
				StatManager.Notoriety += BASE_NOTORIETY_GAIN * count * Time.deltaTime;
				break;
			default:
				break;
		}
	}
}
