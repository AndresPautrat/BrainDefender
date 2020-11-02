using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    double timeElapse = 0;
    double enemySpawnTime;
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    GameObject tower;

    float orto;
    float aspect;
    float width;
    // Start is called before the first frame update
    void Start()
    {
        orto = Camera.main.orthographicSize;
        aspect = Screen.height / Screen.width;
        width = orto * aspect;
        enemySpawnTime = 2.5;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapse += Time.deltaTime;
        if (timeElapse >= enemySpawnTime)
        {
            timeElapse = 0;
            spawnEnemy();
        }
        createTower();
    }
    void createTower()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 worldPoint2d = new Vector2(worldPoint.x, worldPoint.y);
        if (Input.GetMouseButton(0))
        {
            GameObject Tower = Instantiate(tower, new Vector3(worldPoint2d.x, worldPoint2d.y), Quaternion.identity);
        }
        print(worldPoint2d);
    }
    void spawnEnemy()
    {
        float xRandom = Random.Range(-width, width);
        float yRandom = Random.Range(-orto, orto);
        GameObject Enemy = Instantiate(enemy, new Vector3(xRandom, yRandom), Quaternion.identity);
    }
}
