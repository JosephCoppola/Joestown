using UnityEngine;
using System.Collections;

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

	void Awake ()
	{
		ms_instance = this;
	}

	public void GenerateNewbies( MemberScript recruiter )
	{
		int absMaxRecruits = StatManager.MemberMax - StatManager.MemberCount;

		print( recruiter.Devotion );
		print( Mathf.CeilToInt( recruiter.Devotion / 33.0f ) );
		int recruits = Random.Range( 0, Mathf.CeilToInt( recruiter.Devotion / 33.4f ) ); // Up to 3 for max devotion
		recruits = Mathf.Min( recruits, absMaxRecruits );

		print( recruits );

		for( int i = 0; i < recruits; i++ )
		{
			GameObject recruit = Instantiate( memberPrefab );
			recruit.transform.parent = unassignedMemberArea;
			recruit.transform.position = unassignedMemberArea.position - new Vector3( 0.2f * i, 0, 0 );

			MemberScript ms = recruit.GetComponent<MemberScript>();
			bool startSkept = Random.Range( 0, recruiter.Devotion ) < 10 ? true : false;
			ms.Init( Random.Range( 10.0f, recruiter.Devotion ), Random.Range( 25.0f, 100.0f ), startSkept );
		}

		recruiter.transform.position = unassignedMemberArea.position + new Vector3( 0.4f, 0, 0 );
	}
}
