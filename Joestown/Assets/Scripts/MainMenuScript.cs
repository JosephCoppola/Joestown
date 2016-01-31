using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
	void Update ()
	{
		if( Input.anyKeyDown )
		{
			Application.LoadLevel( "Gameplay" );
		}
	}
}
