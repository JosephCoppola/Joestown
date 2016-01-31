using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecruitManager : MonoBehaviour
{
	private static RecruitManager ms_instance;

	public static RecruitManager Instance
	{
		get
		{
			return ms_instance;
		}
	}

	public GameObject memberPrefab;
	public Transform unassignedMemberArea;

	private MainManagerScript m_mainMngr;

	void Awake ()
	{
		ms_instance = this;
	}

	void Start()
	{
		m_mainMngr = GetComponent<MainManagerScript>();

		InitPopulation();
	}

	public void GenerateNewbies( MemberScript recruiter )
	{
		int absMaxRecruits = StatManager.MemberMax - StatManager.MemberCount;

		int recruits = Random.Range( 0, Mathf.CeilToInt( recruiter.Devotion / 25.0f ) ); // Up to 3 for max devotion
		recruits = Mathf.Min( recruits, absMaxRecruits );

		for( int i = 0; i < recruits; i++ )
		{
			GameObject recruit = Instantiate( memberPrefab );
			recruit.transform.parent = unassignedMemberArea;
			recruit.transform.position = unassignedMemberArea.position - new Vector3( 0.2f * i, 0, 0 );

			MemberScript ms = recruit.GetComponent<MemberScript>();
			bool startSkept = Random.Range( 0, recruiter.Devotion ) < 10 ? true : false;
			ms.Init( Random.Range( 10.0f, recruiter.Devotion ), Random.Range( 25.0f, 100.0f ), startSkept );
			StatManager.MemberCount++;
		}

		recruiter.transform.position = unassignedMemberArea.position + new Vector3( 0.4f, 0, 0 );

		List<string> notification = new List<string>();
		string text = "You follower returned with " + recruits + " new recruit" + ( ( recruits != 1 ) ? "s" : "" );
		notification.Add( text );
		m_mainMngr.UI_Mngr.SpawnTextBlurb( notification );
	}

	private void InitPopulation()
	{
		for( int i = 0; i < 4; i++ )
		{
			GameObject recruit = Instantiate( memberPrefab );
			recruit.transform.parent = unassignedMemberArea;
			recruit.transform.position = unassignedMemberArea.position - new Vector3( 0.2f * ( i - 1 ), 0, 0 );
			
			MemberScript ms = recruit.GetComponent<MemberScript>();
			ms.Init( 50, 100, false );
            StatManager.MemberCount++;
        }
	}
}
