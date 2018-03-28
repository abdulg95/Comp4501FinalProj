using UnityEngine;
using System.Collections;

public class RtsMousePointerController : MonoBehaviour {

	public  GameObject selectedObject;
    public GameObject prevSelectedObject;
    Vector3 pos;
    public GameObject prefab;


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
                pos = hitInfo.point;

                GameObject hitObject = hitInfo.transform.gameObject;


				SelectObject (hitObject);
			} else {
				ClearSelection ();
			}
		}

        if (Input.GetKeyDown("b"))
            Instantiate(prefab, selectedObject.transform.position, Quaternion.identity);
           }

    void SelectObject(GameObject obj) {
		if(selectedObject != null) {
			if(obj == selectedObject)
				return;
            prevSelectedObject = selectedObject;
			ClearSelection();
		}

		selectedObject = obj;
        if (selectedObject.GetComponent<Collider>().name == "Terrain")
        {
            prevSelectedObject.GetComponent<MovementComponent>().currentSpeed = 10f;
            prevSelectedObject.GetComponent<MovementComponent>().MoveToSelection(pos);
        }
	}

	void ClearSelection() {
		if(selectedObject == null)
			return;

		selectedObject = null;
	}
}
