using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemieScript : MonoBehaviour
{
    float life = 100;
    float velocity = 0.5f;

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
        move();

    }

    private void move()
    {
        int countNeurons = GameObject.FindGameObjectsWithTag("Neurons").Length;
        for(int i = 0; i< countNeurons; i++)
        {
            Vector3 neuronPosition = GameObject.FindGameObjectsWithTag("Neurons")[i].transform.position;
            float xDistance = neuronPosition.x - transform.position.x;
            float yDistance = neuronPosition.y - transform.position.y;
            float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

            if (distance < 10 )
            {
                float xDirection = velocity * xDistance / distance;
                float yDirection = velocity * yDistance / distance;

                Vector3 direction = new Vector3(xDirection, yDirection);
                gameObject.GetComponent<Rigidbody2D>().velocity = direction;
            }
        }
        if (countNeurons == 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,0.0f);
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
