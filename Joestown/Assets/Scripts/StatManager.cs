using UnityEngine;
using System.Collections;

public class StatManager : MonoBehaviour
{
	private static StatManager ms_instance;

	public Gameplay_Controller ui;

	private float faith = 50.0f;
	private float maxFaith = 100.0f;
	private float notoriety = 0.0f;
	private int memberCount = 0;
	private int maxMemberCount = 5;
	
	public static float Faith
	{
		get
		{
			return ms_instance.faith;
		}
		set
		{
			ms_instance.faith = value;
			if( ms_instance.faith >= ms_instance.maxFaith )
			{
				ms_instance.faith = ms_instance.maxFaith;
				EventManager.TriggerEvent("OnFullFaith");
			}
			ms_instance.ui.SetFaithAmount( ms_instance.faith / ms_instance.maxFaith );
		}
	}

	public static float MaxFaith
	{
		get
		{
			return ms_instance.maxFaith;
		}
		set
		{
			ms_instance.maxFaith = value;
			ms_instance.ui.SetFaithAmount( ms_instance.faith / ms_instance.maxFaith );
		}
	}
	
	public static float Notoriety
	{
		get
		{
			return ms_instance.notoriety;
		}
		set
		{
			ms_instance.notoriety = value;
			ms_instance.ui.SetNotorietyAmount( ms_instance.notoriety * 0.01f );

			if( ms_instance.notoriety >= 100 )
			{
				Application.LoadLevel( "GameOver" );
			}
		}
	}
	
	public static int MemberCount
	{
		get
		{
			return ms_instance.memberCount;
		}
		set
		{
			ms_instance.memberCount = value;
			ms_instance.ui.SetMemberCount( ms_instance.memberCount, ms_instance.maxMemberCount );

			if( ms_instance.memberCount <= 0 )
			{
				Application.LoadLevel( "GameOver" );
			}
		}
	}

	public static int MemberMax
	{
		get
		{
			return ms_instance.maxMemberCount;
		}
		set
		{
			ms_instance.maxMemberCount = value;
			ms_instance.ui.SetMemberCount( ms_instance.memberCount, ms_instance.maxMemberCount );
		}
	}

	void Awake ()
	{
		ms_instance = this;
	}
	
	void Start()
	{
		ms_instance.ui.SetFaithAmount( ms_instance.faith * 0.01f );
		ms_instance.ui.SetNotorietyAmount( ms_instance.notoriety * 0.01f );
		ms_instance.ui.SetMemberCount( ms_instance.memberCount, ms_instance.maxMemberCount );
	}
}
