using UnityEngine;
using System.Collections;

public class BuildingScript : MonoBehaviour
{
	public Transform floors;
	public Transform roof;

	public GameObject floorsPrefab;

	private int m_numFloors = 1;

	public void AddFloor()
	{
		GameObject floor = Instantiate( floorsPrefab );

		floor.transform.position = floors.position;
		floor.transform.position += new Vector3( 0, 0.5f * m_numFloors, 0 );
		floor.transform.parent = floors;

		roof.transform.position += new Vector3( 0, 0.5f, 0 );

		m_numFloors++;
	}
}
