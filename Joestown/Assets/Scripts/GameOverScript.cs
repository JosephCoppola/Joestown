using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour
{
	void Update ()
	{
		if( Input.anyKeyDown )
		{
			Application.LoadLevel( "MainMenu" );
        }
    }
}
