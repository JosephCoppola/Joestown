using UnityEngine;
using System.Collections;

public class SacrificeScript : MonoBehaviour
{
	private float m_faithGain = 20.0f;
	private RoomScript m_sacrificeRoom;

	void Start()
	{
		m_sacrificeRoom = GetComponent<RoomScript>();
	}

	public void PerformSacrifice()
	{
		int numSacd = 0;

		float totalDevotion = 0.0f;

		for( int i = 0; i < m_sacrificeRoom.MemberSlots.Length; i++ )
		{
			if( m_sacrificeRoom.MemberSlots[ i ].childCount > 0 )
			{
				MemberScript ms = m_sacrificeRoom.MemberSlots[ i ].GetChild( 0 ).GetComponent<MemberScript>();
				ms.Sacrifice();

				totalDevotion = ms.Devotion;

				numSacd++;
			}
		}

		totalDevotion /= numSacd;

		StatManager.Faith += numSacd * m_faithGain * ( totalDevotion * 0.01f );
	}
}
