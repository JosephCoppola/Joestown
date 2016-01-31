using UnityEngine;
using System.Collections;

public class SacrificeScript : MonoBehaviour
{
	private float m_faithGain = 10.0f;
	private RoomScript m_sacrificeRoom;

	void Start()
	{
		m_sacrificeRoom = GetComponent<RoomScript>();
	}

	public void PerformSacrifice()
	{
		int numSacd = 0;
		for( int i = 0; i < m_sacrificeRoom.MemberSlots.Length; i++ )
		{
			if( m_sacrificeRoom.MemberSlots[ i ].childCount > 0 )
			{
				Destroy( m_sacrificeRoom.MemberSlots[ i ].GetChild( 0 ).gameObject );
				numSacd++;
			}
		}

		StatManager.Faith += numSacd * m_faithGain;
	}
}
