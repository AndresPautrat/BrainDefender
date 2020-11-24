using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AEnemieScript : MonoBehaviour
{
    float life = 20;
    float velocity = 0.5f;

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

    float timeElapseMove = 0f;
    float timeMoveUpdates = 2f;

    //variables buff
    int applyBuff = -1;
    float velocityBuff = 1;
    float atackBuff = 1;
    float defenseBuff = 1;
    bool poisonAttackBuff = false;
    bool velocityActivate = false;


    float timeElapseEnemyDmg = 0;
    float timeBetweenEnemyDmg = 1;

    float timeElapseBuff = 0f;
    float timeDurationBuff = 10f;

    bool aument = true;

    bool poisoned = false;
    float timeElapsePoison = 0f;
    float timeDurationPoison = 0f;
    float timeElapsePoisonTick = 0f;
    float timeDurationPoisonTick = 2f;
    int poisonDPS = 0;

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

        if (applyBuff != -1)
        {
            timeElapseBuff += Time.deltaTime;
            if (timeElapseBuff >= timeDurationBuff)
            {
                timeElapseBuff = 0;
                applyBuff = -1;
                defenseBuff = 1;
                atackBuff = 1;
                velocityBuff = 1;
                poisonAttackBuff = false;
            }
        }

        if (poisoned)
        {
            timeElapsePoison += Time.deltaTime;
            if (timeElapsePoison >= timeDurationPoison)
            {
                poisoned = false;
                timeElapsePoison = 0f;
                timeDurationPoison = 0f;
            }
            timeElapsePoisonTick += Time.deltaTime;
            if (timeElapsePoisonTick >= timeDurationPoisonTick)
            {
                timeElapsePoisonTick = 0f;
                life -= poisonDPS;
                displayDmgTaken(poisonDPS, new Color(0.5f, 0.2f, 0.8f));
            }
        }
    }

    private void move()
    {
        List<GameObject> targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Neurons"));
        targets.AddRange(GameObject.FindGameObjectsWithTag("Pill"));
        targets.AddRange(GameObject.FindGameObjectsWithTag("Buff"));
        Vector3 closestDirection = new Vector3(0, 0);
        float closestDistance = 9999999;
        for (int i = 0; i < targets.Count; i++)
        {
            Vector3 neuronPosition = targets[i].transform.position;
            float xDistance = neuronPosition.x - transform.position.x;
            float yDistance = neuronPosition.y - transform.position.y;
            float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
            if (distance < closestDistance)
            {
                float xDirection = (velocity * velocityBuff)* xDistance / distance;
                float yDirection = (velocity * velocityBuff) * yDistance / distance;

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
        if (collision.tag == "Bullet")
        {
            BulletFeatures features = collision.GetComponent<BulletFeatures>();
            int dmgTaken = features.getDmg(name);
            switch (collision.name)
            {
                case "BulletA(Clone)":
                    break;
                case "BulletB(Clone)":
                    if (aument)
                        if (gameObject.name == "Enemy3(Clone)")
                        {
                            atack3 = atack3 * 2;
                            aument = false;
                            print("duplico ataque");
                        }

                    break;
                case "BulletC(Clone)":
                    if (gameObject.name == "Enemy3(Clone)")
                    {
                        knockBack(collision);
                    }
                    break;
                case "BulletD(Clone)":
                    if (aument)
                        if (gameObject.name == "Enemy4(Clone)")
                        {
                            life += 50;
                            displayDmgTaken(50, Color.green);
                            aument = false;
                        }

                    break;
            }
            displayDmgTaken(features.getDmg(name), Color.red);
            life -= (int)(dmgTaken/defenseBuff);
            Destroy(collision.gameObject);

            if (life <= 0)
            {
                Destroy(gameObject);
            }

            if (features.getKnockBack())
            {
                knockBack(collision);
            }
            if (features.getPoison())
            {
                poison(10, 2);
            }
        }

        if (collision.tag == "Pill")
        {
            PIllGreenScript pill = collision.GetComponent<PIllGreenScript>();
            if (pill.getKnockBack())
            {
                knockBackPill();
            }
        }
    }

    void displayDmgTaken(int dmgTaken, Color color)
    {
        GameObject canvasText = Instantiate(canvasTextPref, transform.position, Quaternion.identity, transform);
        Text dmgText = canvasText.transform.GetChild(0).gameObject.GetComponent<Text>();
        dmgText.text = dmgTaken.ToString();
        dmgText.color = color;
        Destroy(canvasText, 0.5f);
    }

    public int getDmg(string torret)
    {
        switch (torret)
        {
            case "PillA(Clone)":
                return (int)(atack1 * atackBuff);
            case "PillB(Clone)":
                return (int)(atack2 * atackBuff);
            case "PillC(Clone)":
                return (int)(atack3 * atackBuff);
            case "PillD(Clone)":
                return (int)(atack4 * atackBuff);
        }
        return 0;
    }

    public void startBuff(string buffID)
    {
        print("buff Enemy");
        switch (buffID)
        {
            case "Buff1(Clone)":
                applyBuff = 1;
                velocityActivate = true;
                velocityBuff *= 1.2f;
                move();
                break;
            case "Buff2(Clone)":
                applyBuff = 1;
                atackBuff = 1.15f;
                break;
            case "Buff3(Clone)":
                applyBuff = 1;
                defenseBuff = 1.3f;
                break;
            case "Buff4(Clone)":
                applyBuff = 1;
                poisonAttackBuff = true;
                break;

        }


    }

    void knockBack(Collider2D collision)
    {
        Vector2 velocityCollision = collision.GetComponent<Rigidbody2D>().velocity;
        transform.position = new Vector2(
            transform.position.x + Mathf.Sign(transform.position.x) * Mathf.Abs(velocityCollision.x * 0.01f)
            , transform.position.y + Mathf.Sign(transform.position.y) * Mathf.Abs(velocityCollision.y * 0.01f)
            );
    }

    void knockBackPill()
    {
        print("hola me muero");
        Vector2 velocityCollision = this.GetComponent<Rigidbody2D>().velocity;
        transform.position = new Vector2(
            transform.position.x - velocityCollision.x * 0.1f
            , transform.position.y - velocityCollision.y * 0.1f
            );
    }

    void poison(float time, int dmgPerSecond)
    {
        timeDurationPoison = time;
        poisonDPS = dmgPerSecond;
        poisoned = true;
    }
}
