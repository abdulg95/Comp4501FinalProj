using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour {

    public Texture2D tex = null;
    void OnGUI()
    {
        //Gets coordinate our object on screen
        Vector3 scrPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //Sets texture for size, for example, 100x30
        GUI.DrawTexture(new Rect(scrPos.x - 100 / 2.0f, Screen.height - scrPos.y - 30 / 2.0f, 100, 30), tex, ScaleMode.StretchToFill);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
