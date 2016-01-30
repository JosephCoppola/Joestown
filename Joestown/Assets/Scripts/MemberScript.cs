using UnityEngine;
using System.Collections;

public class MemberScript : MonoBehaviour
{
	public const float MAX_STAMINA = 100.0f;
	public const float MAX_DEVOTION = 100.0f;
	
	public float staminaDrainRate = 1.0f;
	public float staminaRegenRate = 1.0f;
	public float devotionDrainRate = 1.0f;
	public float devotionRegenRate = 1.0f;
	
	private RoomScript m_assignedRoom;
	
	public float m_devotion = 50.0f;
	public float m_stamina = 100.0f;
	public bool m_skepical = false;
	
	void Update ()
	{
		DoRoom();
	}
	
	public void ChangeRoom( RoomScript newRoom )
	{
        RemoveFromRoom();
		m_assignedRoom = newRoom;
		
		transform.position = newRoom.transform.position;
		transform.position += new Vector3 ( Random.Range( -0.25f, 0.25f ), -0.05f, 0.0f );
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
			if( m_assignedRoom.Type == RoomScript.RoomType.HOUSING )
			{
				m_stamina += staminaRegenRate * Time.deltaTime;
				CheckStamina();
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
                    m_stamina -= staminaDrainRate * Time.deltaTime;
                    CheckStamina();
                    
                    if( m_devotion >= MAX_DEVOTION )
                    {
                        m_devotion = MAX_DEVOTION;
                        // Add to faith?
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
