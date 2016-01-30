using UnityEngine;
using System.Collections;

public class BuildingScript : MonoBehaviour
{
	public Transform floors;
	public Transform roof;

	public GameObject floorsPrefab;

	private int m_numFloors = 1;
	private int m_maxFloors = 5;

	void Start ()
	{
	
	}

	void Update ()
	{
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			AddFloor();
		}
	}

	public void AddFloor()
	{
		if( m_numFloors < m_maxFloors )
		{
			GameObject floor = Instantiate( floorsPrefab );

			floor.transform.position = floors.position;
			floor.transform.position += new Vector3( 0, 0.64f * m_numFloors, 0 );
			floor.transform.parent = floors;

			roof.transform.position += new Vector3( 0, 0.64f, 0 );

			m_numFloors++;
		}
	}
}
