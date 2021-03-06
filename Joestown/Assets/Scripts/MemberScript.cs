﻿using UnityEngine;
using System.Collections;

public class MemberScript : MonoBehaviour
{
	public const float MAX_STAMINA = 100.0f;
	public const float MAX_DEVOTION = 100.0f;
	public const float TOWN_TIME = 10.0f;

	public Sprite defaultSprite;
	public Sprite prayerSprite;
	public Sprite prisonerSprite;

	public float staminaDrainRate = 1.0f;
	public float staminaRegenRate = 1.0f;
	public float devotionDrainRate = 1.0f;
	public float devotionRegenRate = 1.0f;

	private Vector3 m_lastPos;

	private RoomScript m_assignedRoom;

	private SpriteRenderer m_spriteRenderer;
	private Collider2D m_collider;
	
	private float m_devotion = 50.0f;
	private float m_stamina = 100.0f;
	private float m_cellTime;
	private bool m_skeptical = false;

	public float Devotion
	{
		get
		{
			return m_devotion;
		}
	}

	public float Stamina
	{
		get
		{
			return m_stamina;
		}
	}

	public bool Skeptical
	{
		get
		{
			return m_skeptical;
		}
	}

	void Start()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		m_collider = GetComponent<Collider2D>();
	}

	void Update ()
	{
		DoRoom();
	}

	public void Init( float devotion, float stamina, bool skeptical )
	{
		m_devotion = devotion;
		m_stamina = stamina;
		m_skeptical = skeptical;
	}
	
	public void ChangeRoom( RoomScript newRoom )
	{
        RemoveFromRoom();
		m_assignedRoom = newRoom;

		if( m_assignedRoom.Type == RoomScript.RoomType.CELL )
		{
			m_cellTime = Random.Range( 30.0f, 120.0f );
			m_spriteRenderer.sprite = prisonerSprite;
		}
		else if( m_assignedRoom.Type == RoomScript.RoomType.WORSHIP )
		{
			m_spriteRenderer.sprite = prayerSprite;
		}
		else
		{
			m_spriteRenderer.sprite = defaultSprite;
		}
		//transform.position = newRoom.transform.position;
		//transform.position += new Vector3 ( 0, 0, -1.0f );
	}

	public void ResetPosition()
	{
		transform.position = m_lastPos;
	}

	public void SetSelected()
	{
		m_lastPos = transform.position;
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
		RemoveFromRoom();
		m_assignedRoom = null;
		m_collider.enabled = false;
		m_spriteRenderer.enabled = false;

		StartCoroutine( StollThroughTown() );
	}

	public void Sacrifice()
	{
		RemoveFromRoom();
		StatManager.MemberCount--;
		Destroy( gameObject );
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

		m_stamina -= 33.3f;
		CheckStamina();

		RecruitManager.Instance.GenerateNewbies( this );
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
		
		if( m_skeptical )
		{
			if( m_assignedRoom.Type == RoomScript.RoomType.CELL )
			{
				m_cellTime -= Time.deltaTime;

				if( m_cellTime <= 0 )
				{
					m_skeptical = false;
				}
			}
			else
			{
				m_devotion -= devotionDrainRate * Time.deltaTime;
				
				if( m_devotion <= 0 )
				{
                    RemoveFromRoom();
                    
					StatManager.MemberCount--;
                    Destroy( gameObject ); // Fade out or something...
				}
			}
		}
		else
		{
			switch( m_assignedRoom.Type )
			{
				case RoomScript.RoomType.SACRIFICE: // Fallthrough intentional
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
	    	m_skeptical = false;
	    }
	    else if( m_stamina < 0 )
	    {
	    	m_stamina = 0;
	    	m_skeptical = true;
	    }
	}
}
