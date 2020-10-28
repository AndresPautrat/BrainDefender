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

    // Start is called before the first frame update
    void Start()
    {
        float orto = Camera.main.orthographicSize;
        float aspect = Screen.height / Screen.width;
        float width = orto * aspect;
        print(Screen.height);
        print(Screen.width);
        print(orto);
        print(aspect);
        GameObject bullet = Instantiate(enemy, new Vector3(orto, aspect), Quaternion.identity);
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
    }

    void spawnEnemy()
    {
        float xRandom = Random.Range(-50f, 50f);
        float yRandom = Random.Range(-50f, 50f);
        
    }
}
