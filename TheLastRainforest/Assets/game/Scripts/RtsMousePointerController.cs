using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RtsMousePointerController : MonoBehaviour {
	RaycastHit hit;
	private float raycastlength = 500;
	public GameObject Target;

	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, raycastlength)) {
			Debug.Log(hit.collider.name);
			if (hit.collider.name == "Terrain") {
				//when cliclked instantiate object
				if (Input.GetMouseButtonDown (1)) 
				{
					GameObject TargetObject = Instantiate (Target,hit.point,Quaternion.identity) as GameObject;
					TargetObject.name = "Target Instantiated";
				}
				Target.transform.position = hit.point;
			}
		}
		Debug.DrawRay (ray.origin, ray.direction * raycastlength, Color.yellow);
	}
}
