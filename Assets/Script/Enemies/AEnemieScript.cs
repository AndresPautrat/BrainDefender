using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemieScript : MonoBehaviour
{
    float life = 20;
    float velocity = 0.5f;

    [SerializeField]
    int dmgFromA;
    [SerializeField]
    int dmgFromB;
    [SerializeField]
    int dmgFromC;
    [SerializeField]
    int dmgFromD;

    [SerializeField]
    int atack1;
    [SerializeField]
    int atack2;
    [SerializeField]
    int atack3;
    [SerializeField]
    int atack4;

    bool aument = true;

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

    }

    private void move()
    {
        int countNeurons = GameObject.FindGameObjectsWithTag("Neurons").Length;
        for (int i = 0; i < countNeurons; i++)
        {
            Vector3 neuronPosition = GameObject.FindGameObjectsWithTag("Neurons")[i].transform.position;
            float xDistance = neuronPosition.x - transform.position.x;
            float yDistance = neuronPosition.y - transform.position.y;
            float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

            if (distance < 10)
            {
                float xDirection = velocity * xDistance / distance;
                float yDirection = velocity * yDistance / distance;

                Vector3 direction = new Vector3(xDirection, yDirection);
                gameObject.GetComponent<Rigidbody2D>().velocity = direction;
            }
        }
        if (countNeurons == 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "BulletA":
                life -= dmgFromA;
                Destroy(collision.gameObject);
                break;
            case "BulletB":
                life -= dmgFromB;
                Destroy(collision.gameObject);

                if (aument)
                    if (gameObject.name == "Enemy3(Clone)")
                    {
                        atack3 = atack3 * 2;
                        aument = false;
                        print("duplico ataque");
                    }

                break;
            case "BulletC":
                life -= dmgFromC;
                Destroy(collision.gameObject);
                if (gameObject.name == "Enemy3(Clone)")
                {
                    Vector2 velocityCollision = collision.GetComponent<Rigidbody2D>().velocity;
                    transform.position = new Vector2(
                        transform.position.x + Mathf.Sign(transform.position.x) * Mathf.Abs(velocityCollision.x * 0.01f)
                        , transform.position.y + Mathf.Sign(transform.position.y) * Mathf.Abs(velocityCollision.y * 0.01f)
                        );
                }
                break;
            case "BulletD":
                life -= dmgFromD;
                Destroy(collision.gameObject);
                if (aument)
                    if (gameObject.name == "Enemy4(Clone)")
                    {
                        life += 50;
                        aument = false;
                    }

                break;
        }
        if (life <= 0)
        {
            Destroy(gameObject);
        }
        move();

    }

}
