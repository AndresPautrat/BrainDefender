using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    GameObject canvasTextPref;

    float timeElapseMove=0f;
    float timeMoveUpdates=2f;

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
        timeElapseMove += Time.deltaTime;
        if (timeElapseMove >= timeMoveUpdates)
        {
            timeElapseMove = 0;
            move();
        }
    }

    private void move()
    {
        List<GameObject> targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Neurons"));
        targets.AddRange(GameObject.FindGameObjectsWithTag("Pill"));
        targets.AddRange(GameObject.FindGameObjectsWithTag("Buff"));
        Vector3 closestDirection=new Vector3(0,0);
        float closestDistance = 9999999;
        for (int i = 0; i < targets.Count; i++)
        {
            Vector3 neuronPosition = targets[i].transform.position;
            float xDistance = neuronPosition.x - transform.position.x;
            float yDistance = neuronPosition.y - transform.position.y;
            float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
            if (distance < closestDistance)
            {
                float xDirection = velocity * xDistance / distance;
                float yDirection = velocity * yDistance / distance;

                closestDirection = new Vector3(xDirection, yDirection);
                closestDistance = distance;
                //gameObject.GetComponent<Rigidbody2D>().velocity = direction;
            }
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = closestDirection;
        //if (targets.Count == 0)
        //{
        //    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.name)
        {
            case "BulletA(Clone)":
                displayDmgTaken(dmgFromA);
                life -= dmgFromA;
                Destroy(collision.gameObject);
                break;
            case "BulletB(Clone)":
                displayDmgTaken(dmgFromB);
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
            case "BulletC(Clone)":
                displayDmgTaken(dmgFromC);
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
            case "BulletD(Clone)":
                displayDmgTaken(dmgFromD);
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

    }

    void displayDmgTaken(int dmgTaken)
    {
        GameObject canvasText = Instantiate(canvasTextPref, transform.position, Quaternion.identity, transform);
        canvasText.transform.GetChild(0).gameObject.GetComponent<Text>().text = dmgTaken.ToString();
        Destroy(canvasText, 0.5f);
    }

    public int getDmg(string torret)
    {
        print("dmg"+torret);
        switch (torret)
        {
            case "PillA(Clone)":
                return atack1;
            case "PillB(Clone)":
                return atack2;
            case "PillC(Clone)":
                return atack3;
            case "PillD(Clone)":
                return atack4;
        }
        return 0;
    }

    public void startBuff(string buffID)
    {
        print("buff pill");
    }

}
