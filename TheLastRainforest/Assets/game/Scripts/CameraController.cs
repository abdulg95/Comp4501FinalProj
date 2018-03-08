using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	//speed at which camera moves
	public float panspeed = 40f;
	public static float panBorderThickness = 15f;
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		//move forward
		if (IsMousePositionWithinBoundaries ())
		{
			if(Input.mousePosition.y >= Screen.height - panBorderThickness)
			{
				pos.z += panspeed * Time.deltaTime;
			}
			//move backward
			if(Input.mousePosition.y <= panBorderThickness)
			{
				pos.z -= panspeed * Time.deltaTime;
			}
			//move right
			if(Input.mousePosition.x >= Screen.width - panBorderThickness)
			{
				pos.x += panspeed * Time.deltaTime;
			}
			//move left
			if(Input.mousePosition.x <= panBorderThickness)
			{
				pos.x -= panspeed * Time.deltaTime;
			}

		}

		if(Input.GetKey("w") )
		{
			pos.z += panspeed * Time.deltaTime;
		}
		//move backward
		if(Input.GetKey("s"))
		{
			pos.z -= panspeed * Time.deltaTime;
		}
		//move right
		if(Input.GetKey("d") )
		{
			pos.x += panspeed * Time.deltaTime;
		}
		//move left
		if(Input.GetKey("a") )
		{
			pos.x -= panspeed * Time.deltaTime;
		}
		transform.position = pos;
	}

	public static bool IsMousePositionWithinBoundaries()
	{
		if (
			(Input.mousePosition.x < panBorderThickness && Input.mousePosition.x > -5) ||
			(Input.mousePosition.x > (Screen.width - panBorderThickness) && Input.mousePosition.x < (Screen.width + 5)) ||
			(Input.mousePosition.y < panBorderThickness && Input.mousePosition.y > -5) ||
			(Input.mousePosition.y > (Screen.height - panBorderThickness) && Input.mousePosition.y < (Screen.height + 5))
		)
		return true; else return false;
	}
}

