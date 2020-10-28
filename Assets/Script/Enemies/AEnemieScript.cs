using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemieScript : MonoBehaviour
{
    float life = 500;
    float velocity = 1;
    // Start is called before the first frame update
    void Start()
    {
        //modificar esto
        Vector3 enemyPosition = GameObject.FindGameObjectsWithTag("Neurons")[0].transform.position;
        float xDistance = enemyPosition.x - transform.position.x;
        float yDistance = enemyPosition.y - transform.position.y;
        float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
        float xDirection = velocity * xDistance / distance;
        float yDirection = velocity * yDistance / distance;
        Vector3 direction = new Vector3(xDirection, yDirection);
        gameObject.GetComponent<Rigidbody2D>().velocity = direction;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            life -= 10;
            Destroy(collision.gameObject);
        }
    }
}
