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
        HOUSING,
		CELL,
		PUNCH,
		SACRIFICE
    }

	public GameObject defaultProps;
	public GameObject housingProps;
	public GameObject worshipProps;
	public GameObject props;

	public Sprite defaultSprite;
	public Sprite worshipSprite;

    public RoomType roomType;
	public int maxMembers = 4;

	private SpriteRenderer m_spriteRenderer;

	private List<MemberScript> m_assignedMembers;
	public List<MemberScript> AssignedMembers
	{
		get { return m_assignedMembers; }
	}

	private Transform[] memberSlots;

    public RoomType Type
    {
        get
        {
            return roomType;
        }
    }

	public Transform[] MemberSlots
	{
		get
		{
			return memberSlots;
		}
	}

	void Start()
	{
		m_assignedMembers = new List<MemberScript>();
		memberSlots = new Transform[ maxMembers ];

		Transform slotContainer = transform.FindChild( "Member Slots" );

		if( slotContainer != null && slotContainer.childCount >= maxMembers )
		{
			for( int i = 0; i < maxMembers; i++ )
            {
				memberSlots[ i ] = slotContainer.GetChild( i );
			}
		}
		else
		{
			print( "Room doesn't have member slots :(" );
			/*// If there are no slots, just space out the spawn locations
			for( int i = 0; i < maxMembers; i++ )
			{
				memberSlots[ i ] = new GameObject( "Member Slots" ).transform;
				memberSlots[ i ].parent = transform;
				memberSlots[ i ].position = new Vector3( 1 / maxMembers * i - 0.5f, 0.125f, 0 );
            }*/
        }

		m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
	{
		UpdateStats();
	}

	public void AssignMember( MemberScript memberToAssign )
	{
		if( m_assignedMembers.Count < maxMembers )
		{
			m_assignedMembers.Add( memberToAssign );

			for( int i = 0; i < memberSlots.Length; i++ )
			{
				if( memberSlots[ i ].childCount == 0 )
				{
					memberToAssign.transform.parent = memberSlots[ i ];
					memberToAssign.transform.position = memberSlots[ i ].position;
					break;
				}
			}
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

	public void Change()
	{
		Destroy( props );

		switch( roomType )
		{
			case RoomType.DEFAULT:
				props = Instantiate( defaultProps );
				m_spriteRenderer.sprite = defaultSprite;
				break;
			case RoomType.HOUSING:
				props = Instantiate( housingProps );
				m_spriteRenderer.sprite = defaultSprite;
				break;
			case RoomType.WORSHIP:
				props = Instantiate( worshipProps );
				m_spriteRenderer.sprite = worshipSprite;
				break;
			default:
				break;
		}

		props.transform.parent = transform;
		props.transform.position = transform.position;
	}
}
