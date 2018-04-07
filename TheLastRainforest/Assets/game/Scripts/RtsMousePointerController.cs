using UnityEngine.EventSystems;
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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            RaycastHit hitInfo;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");

            }
            else
            {
                if (Physics.Raycast(ray, out hitInfo))
                {

                    Debug.Log("Mouse is over: " + hitInfo.collider.name);
                    pos = hitInfo.point;

                    GameObject hitObject = hitInfo.transform.gameObject;

                    if (!hitObject.CompareTag("enemy"))
                    {
                        SelectObject(hitObject);
                    }
                    
                }
                else
                {
                    ClearSelection();
                }
            }
            
        }

        if (Input.GetKeyDown("b"))
            Instantiate(prefab, selectedObject.transform.position, Quaternion.identity);

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {

                Debug.Log("right clicked above: " + hitInfo.collider.name);
                Debug.Log(selectedObject.name);
                //pos = hitInfo.point;

                GameObject hitObject = hitInfo.transform.gameObject;
                if(selectedObject != null)
                {
                    if (hitObject.GetComponent<Collider>().name == "Terrain")
                    {
                        //SelectedObject.GetComponent<MovementComponent>().currentSpeed = 10f;
                        if (GameManager.instance.gold > 15)
                        {
                            selectedObject.GetComponent<AbilityComponent>().UseOn(hitInfo.point);
                            GameManager.instance.DeductGold();
                        }
                       
                        Debug.Log("Point was: " + hitInfo.point);
                    }
                    else
                    {
                        selectedObject.GetComponent<AbilityComponent>().UseOn(hitObject);
                    }

                }

                //SelectObject(hitObject);
            }

        }
    }
            
                


    void SelectObject(GameObject obj) {
		if(selectedObject != null) {
			if(obj == selectedObject)
				return;
            prevSelectedObject = selectedObject;
			ClearSelection();
		}

		selectedObject = obj;
        if (selectedObject.GetComponent<Collider>().name == "Terrain" && (prevSelectedObject != null) &&(prevSelectedObject.GetComponent<MovementComponent>()!= null ))
        {
            prevSelectedObject.GetComponent<MovementComponent>().currentSpeed = 10f;
            prevSelectedObject.GetComponent<MovementComponent>().MoveTo(pos);
            prevSelectedObject.GetComponent<MovementComponent>().ClearLeader();
            Debug.Log("attempted to move to: " + pos);
        }
	}

	void ClearSelection() {
		if(selectedObject == null)
			return;

		selectedObject = null;
	}
}
