using UnityEngine;
using System.Collections;

public class RtsMousePointerController : MonoBehaviour {

	public  GameObject selectedObject;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hitInfo;

			if (Physics.Raycast (ray, out hitInfo)) {

				Debug.Log ("Mouse is over: " + hitInfo.collider.name);

				GameObject hitObject = hitInfo.transform.gameObject;


				SelectObject (hitObject);
			} else {
				ClearSelection ();
			}
		}

	}

	void SelectObject(GameObject obj) {
		if(selectedObject != null) {
			if(obj == selectedObject)
				return;

			ClearSelection();
		}

		selectedObject = obj;
	}

	void ClearSelection() {
		if(selectedObject == null)
			return;

		selectedObject = null;
	}
}
