using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //declare and initialize gamemanager instance to null
    public static GameManager instance = null;
    public GameObject prefab;
    private static GameObject[] gos;

    //define startup values here e.g hp

    void Awake() {
        //enforce singleton we only need one gamemanager
        if (instance == null && instance != this) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        gos = new GameObject[10];
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i] = Instantiate(prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
            Debug.Log("cloning" + "" + prefab.name + "object");


        }


    }

    public  GameObject[] GetGos()
    {
        return gos;
    }




	// Use this for initialization
	void Start () {
        
    }

	// Update is called once per frame
	void Update () {

	}
}
