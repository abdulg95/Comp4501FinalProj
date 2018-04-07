using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //declare and initialize gamemanager instance to null
    public static GameManager instance = null;
    public GameObject prefab;
    public GameObject leaderPrefab;
    private static GameObject[] gos;
    private static GameObject leader;
    public double gold = 0;
    public int spawnTimer=0;
    public int dificultyModifier=1;

    //declare targets for the builder and the spawner tree.
    public GameObject builderTarget;
    public GameObject spawnerTarget;

    public List<GameObject> Leaders;

    public void setBuilderTarget(GameObject target)
    {
        builderTarget = target;
    }
    public void setSpawnerTarget(GameObject target)
    {
        spawnerTarget = target;
    }

    void Awake() {
        //enforce singleton we only need one gamemanager
        if (instance == null && instance != this) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        spawnBaddies();


    }
    void spawnBaddies()
    {
        leader = Instantiate(leaderPrefab, new Vector3(332.0f, 0.1f, 139), Quaternion.identity);
        Leaders.Add(leader);
        gos = new GameObject[10];
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i] = Instantiate(prefab, new Vector3(i * 2.0F + 330, 0.1f, 136), Quaternion.identity);
            gos[i].GetComponent<MovementComponent>().MoveTo(leader);
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
        
        spawnTimer++;
        if(spawnTimer == 1000)
        {

            spawnBaddies();
            spawnTimer = 0;
        }
        Leaders.RemoveAll(item => item == null);
	}
}
