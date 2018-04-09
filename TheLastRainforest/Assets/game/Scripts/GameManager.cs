﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public bool gameOver = false;

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
        leader = Instantiate(leaderPrefab);
        int rand = Random.Range(0, 130);
        leader.GetComponent<NavMeshAgent>().Warp(new Vector3(430.0f, 0.0f, 140 + rand));
        //Debug.Log("leader position is: " + leader.transform.position);
        leader.GetComponent<NavMeshAgent>().destination = GameObject.FindGameObjectWithTag("goal").transform.position;
        //Debug.Log("path status: " + leader.GetComponent<NavMeshAgent>().pathStatus);
        Leaders.Add(leader);
        gos = new GameObject[10];
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i] = Instantiate(prefab, new Vector3(i * 2F + 430, 0.1f, 136+ rand), Quaternion.identity);
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
        gameOver = false;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Gold: "+gold);
        if (gameOver)
        {
            GUI.Label(new Rect(500, 500, 200, 200), "GAMEOVER");
        }
    }

    // Update is called once per frame
    void Update () {
        gold += 0.5;
        
        spawnTimer++;
        if(spawnTimer == 1000)
        {
            for(int i = 0; i < dificultyModifier; i++)
            {
                spawnBaddies();
            }
            
            spawnTimer = 0;
            dificultyModifier++;
        }
        Leaders.RemoveAll(item => item == null);
        if (gameOver) {
            Application.Quit();
        }
	}
}
