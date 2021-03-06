﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    double timeElapseEnemies = 0;
    double timeElapseNeurons = 0;

    double enemySpawnTime = 2.5;
    double neuronsSpawnTime = 10;

    double buffElapseTime = 20;
    double buffSpawnTime = 30;

    [SerializeField]
    GameObject[] enemyPref;

    [SerializeField]
    GameObject[] pillPrefs;

    [SerializeField]
    GameObject neuronPref;

    [SerializeField]
    Button[] pillCreationBtn;

    [SerializeField]
    Camera camPause;

    float orto;
    float aspect;
    float width;
    float score = 0;

    int creatingPill = -1;

    [SerializeField]
    GameObject[] buffPrefs;

    void Start()
    {
        orto = Camera.main.orthographicSize;
        width = orto * Camera.main.aspect;
        //for (int i =0; i < pillCreationBtn.Length; i++)
        //{
        //    pillCreationBtn[i].onClick.AddListener(() => { creatingPill = i.; });
        //}
        pillCreationBtn[0].onClick.AddListener(btnA);
        pillCreationBtn[1].onClick.AddListener(btnB);
        pillCreationBtn[2].onClick.AddListener(btnC);
        pillCreationBtn[3].onClick.AddListener(btnD);

        PlayerPrefs.SetInt("MakingConnection", 0);
    }

    void btnA()
    {
        creatingPill = 0;
    }

    void btnB()
    {
        creatingPill = 1;
    }

    void btnC()
    {
        creatingPill = 2;
    }

    void btnD()
    {
        creatingPill = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapseEnemies += Time.deltaTime;
        if (timeElapseEnemies >= enemySpawnTime)
        {
            timeElapseEnemies = 0;
            spawnEnemy();
        }

        if (creatingPill != -1)
        {
            createTower();
        }

        timeElapseNeurons += Time.deltaTime;
        if (timeElapseNeurons >= neuronsSpawnTime)
        {
            timeElapseNeurons = 0;
            spawnNeuron();
        }

        buffElapseTime += Time.deltaTime;
        if (buffElapseTime >= buffSpawnTime)
        {
            buffElapseTime = 0;
            spawnBuff();
        }
    }
    void createTower()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 worldPoint2d = new Vector2(worldPoint.x, worldPoint.y);
            GameObject Tower = Instantiate(pillPrefs[creatingPill], new Vector3(worldPoint2d.x, worldPoint2d.y), Quaternion.identity);
            creatingPill = -1;
        }
    }
    void spawnEnemy()
    {
        int startSide = Random.Range(1, 5);
        int enemyType = Random.Range(0,4);
        float xRandom = Random.Range(-width, width);
        float yRandom = Random.Range(-orto, orto);
        GameObject enemy;

        switch (startSide)
        {
            case 1:
                enemy = Instantiate(enemyPref[enemyType], new Vector3(width, yRandom), Quaternion.identity);
                break;
            case 2:
                enemy = Instantiate(enemyPref[enemyType], new Vector3(-width, yRandom), Quaternion.identity);
                break;
            case 3:
                enemy = Instantiate(enemyPref[enemyType], new Vector3(xRandom, orto), Quaternion.identity);
                break;
            case 4:
                enemy = Instantiate(enemyPref[enemyType], new Vector3(xRandom, -orto), Quaternion.identity);
                break;
        }

    }

    void spawnNeuron()
    {
        float xRandom = Random.Range(-width, width);
        float yRandom = Random.Range(-orto, orto);
        GameObject neuron = Instantiate(neuronPref, new Vector3(xRandom / 2, yRandom / 2), Quaternion.identity);
    }

    void spawnBuff()
    {
        int buffType = Random.Range(0, 4);
        float xRandom = Random.Range(-width, width);
        float yRandom = Random.Range(-orto, orto);

        GameObject enemy = Instantiate(buffPrefs[buffType], new Vector3(xRandom / 2, yRandom / 2), Quaternion.identity);
    }


    //TODO to correct menu pause in other screens 
    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Camera.main.transform.position = new Vector3(camPause.transform.position.x, camPause.transform.position.y, camPause.transform.position.z);
        }
        else
        {
            Time.timeScale = 1;
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }
    }
}