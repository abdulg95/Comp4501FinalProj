using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //declare and initialize gamemanager instance to null
    public static GameManager instance = null;
    public GameObject prefab;
    private static GameObject[] gos;
    public double gold = 0;

    //define startup values here e.g hp

    void Awake() {
        //enforce singleton we only need one gamemanager
        if (instance == null && instance != this) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        gos = new GameObject[5];
        for (int i = 0; i < gos.Length; i++)
        {
            Vector3 position = new Vector3(
            10 * Random.value + prefab.transform.position.x,
            prefab.transform.position.y,
            10 * Random.value + prefab.transform.position.z
        );
            gos[i] = Instantiate(prefab, position, Quaternion.identity);
            //Debug.Log("cloning" + "" + prefab.name + "object with velocity: "+ gos[i].GetComponent<Rigidbody>().velocity);


        }


    }

    void UpdateGold()
    {
         gold++;
    }

    public  GameObject[] GetGos()
    {
        return gos;
    }

    public void DeductGold()
    {
        gold-=15;
    }




    // Use this for initialization
    void Start () {
        
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Gold: "+gold);
    }

    // Update is called once per frame
    void Update () {
        gold += 0.5;
       
	}
}
