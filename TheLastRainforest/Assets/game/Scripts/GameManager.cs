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
        gos = new GameObject[10];
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i] = Instantiate(prefab, new Vector3(i * 2.0F +330, 0, 136), Quaternion.identity);
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
