using UnityEngine;
using System.Collections;

public class MemberScript : MonoBehaviour
{
	public const float MAX_STAMINA = 100.0f;
	public const float MAX_DEVOTION = 100.0f;
	public const float TOWN_TIME = 60.0f;
	
	public float staminaDrainRate = 1.0f;
	public float staminaRegenRate = 1.0f;
	public float devotionDrainRate = 1.0f;
	public float devotionRegenRate = 1.0f;
	
	private RoomScript m_assignedRoom;

	private SpriteRenderer m_spriteRenderer;
	private Collider2D m_collider;
	
	private float m_devotion = 50.0f;
	private float m_stamina = 100.0f;
	private float m_cellTime;
	private bool m_skepical = false;

	void Start()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		m_collider = GetComponent<Collider2D>();
	}

	void Update ()
	{
		DoRoom();
	}
	
	public void ChangeRoom( RoomScript newRoom )
	{
        RemoveFromRoom();
		m_assignedRoom = newRoom;

		if( m_assignedRoom.Type == RoomScript.RoomType.CELL )
		{
			m_cellTime = Random.Range( 30.0f, 120.0f );
		}

		//transform.position = newRoom.transform.position;
		//transform.position += new Vector3 ( 0, 0, -1.0f );
	}

	public void ResetPosition()
	{
		transform.position = transform.parent.position;
	}

	public void SetSelected()
	{
		m_collider.enabled = false;
		m_spriteRenderer.sortingOrder = 100;
	}

	public void SetDeselected()
	{
		m_collider.enabled = true;
		m_spriteRenderer.sortingOrder = 10;
	}

	public void GoToTown()
	{
		m_assignedRoom = null;
		m_collider.enabled = false;
		m_spriteRenderer.enabled = false;

		StartCoroutine( StollThroughTown() );
	}

	private IEnumerator StollThroughTown()
	{
		yield return new WaitForSeconds( TOWN_TIME );
		ReturnFromTown();
	}

	private void ReturnFromTown()
	{
		m_collider.enabled = true;
		m_spriteRenderer.enabled = true;
	}
    
    private void RemoveFromRoom()
    {
        if( m_assignedRoom != null )
        {
            m_assignedRoom.RemoveMember( this );
        }
    }
	
	private void DoRoom()
	{
		if( m_assignedRoom == null )
		{
			// early return
			return;
		}
		
		if( m_skepical )
		{
			if( m_assignedRoom.Type == RoomScript.RoomType.CELL )
			{
				m_cellTime -= Time.deltaTime;

				if( m_cellTime <= 0 )
				{
					m_skepical = false;
				}
			}
			else
			{
				m_devotion -= devotionDrainRate * Time.deltaTime;
				
				if( m_devotion <= 0 )
				{
                    RemoveFromRoom();
                    
                    Destroy( gameObject ); // Fade out or something...
                    // update population
				}
			}
		}
		else
		{
			switch( m_assignedRoom.Type )
			{
				case RoomScript.RoomType.DEFAULT:
					m_stamina -= staminaDrainRate * Time.deltaTime;
					CheckStamina();
					break;
				case RoomScript.RoomType.HOUSING:
					m_stamina += staminaRegenRate * Time.deltaTime;
					CheckStamina();
					break;
				case RoomScript.RoomType.WORSHIP:
					m_devotion += devotionRegenRate * Time.deltaTime;
                    m_stamina -= staminaDrainRate * Time.deltaTime * 0.5f;
                    CheckStamina();
                    
                    if( m_devotion >= MAX_DEVOTION )
                    {
                        m_devotion = MAX_DEVOTION;
                    }
					break;
				default:
					break;
			}
		}
	}
	
	private void CheckStamina()
	{
	    if( m_stamina >= MAX_STAMINA )
	    {
	    	m_stamina = MAX_STAMINA;
	    	m_skepical = false;
	    }
	    else if( m_stamina < 0 )
	    {
	    	m_stamina = 0;
	    	m_skepical = true;
	    }
	}
}
