using UnityEngine;
using System.Collections;

public class BuildingScript : MonoBehaviour
{
	public Transform floors;
	public Transform roof;

	public GameObject floorsPrefab;

	private int m_numFloors = 1;
	private float roomHeight = 0.5f;

	public void AddFloor()
	{
		GameObject floor = Instantiate( floorsPrefab );

		floor.transform.position = floors.position;
		floor.transform.position += new Vector3( 0, roomHeight * m_numFloors, 0 );
		floor.transform.parent = floors;

		roof.transform.position += new Vector3( 0, roomHeight, 0 );

		m_numFloors++;

		EventManager.TriggerEvent( "AddedFloor" );
	}

	public float GetTopFloorY()
	{
		return roomHeight * m_numFloors;
	}

	public float GetBottomFloorY()
	{
		return -roomHeight;
	}
}
