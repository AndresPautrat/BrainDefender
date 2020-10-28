using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIllGreenScript : MonoBehaviour
{
    [SerializeField]
    GameObject enemie;
    [SerializeField]
    GameObject bulletPref;
    float velocity=10;

    double timeElapse=0;
    double recoil=1;

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timeElapse += Time.deltaTime;
        if (timeElapse >= recoil)
        {
            timeElapse = 0;
            shoot();
        }
        
    }

    void shoot()
    {
        GameObject bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);
        //modificar esto
        Vector3 enemyPosition = GameObject.FindGameObjectsWithTag("Enemy")[0].transform.position;
        float xDistance = enemyPosition.x - transform.position.x;
        float yDistance = enemyPosition.y - transform.position.y;
        float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
        float xDirection = velocity * xDistance / distance;
        float yDirection = velocity * yDistance / distance;
        Vector3 direction = new Vector3(xDirection, yDirection);
        bullet.GetComponent<Rigidbody2D>().velocity = direction;
    }
    
}
