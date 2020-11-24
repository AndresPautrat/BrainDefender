using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PIllGreenScript : MonoBehaviour
{
    [SerializeField]
    int life;
    [SerializeField]
    GameObject bulletPref;
    float velocity = 10;

    float timeElapseEnemyDmg = 0;
    float timeBetweenEnemyDmg = 1;

    float timeElapseBuff = 0;
    float timeBetweenBuff = 10;

    double timeElapse = 1;
    double recoil = 1;
    double recoilBase = 1;

    int applyingBuff = -1;

    float defenseBuff = 1;
    float atackBuff = 1;
    float velocityBuff = 1;
    bool knockBackBullet = false;
    bool knockBackPill = false;
    bool poison = false;

    [SerializeField]
    GameObject canvasTextPref;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeElapseBuff += Time.deltaTime;
        if (timeElapseBuff >= timeBetweenBuff)
        {
            timeElapseBuff = 0;
            applyingBuff = -1;
        }
            timeElapse += Time.deltaTime;
        if (timeElapse >= recoil)
        {
            timeElapse = 0;
            List<GameObject> enemies = new List<GameObject>();
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Buff"));
            if (enemies.Count > 0)
            {
                GameObject targetEnemy = enemies[0];
                behaviour(targetEnemy);
            }
        }
    }

    void shoot(GameObject enemy, float speed)
    {
        GameObject bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);
        Vector3 enemyPosition = enemy.transform.position;


        float xDistance = enemyPosition.x - transform.position.x;
        float yDistance = enemyPosition.y - transform.position.y;
        float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
        float xDirection = speed * xDistance / distance;
        float yDirection = speed * yDistance / distance;
        Vector3 direction = new Vector3(xDirection, yDirection);
        bullet.GetComponent<Rigidbody2D>().velocity = direction;
    }

    void behaviour(GameObject enemy)
    {

        switch (gameObject.name)
        {
            case "PillA(Clone)":
                if (enemy.name == "Enemy2(Clone)")
                    recoil = recoilBase / 2;
                else
                    recoil = recoilBase;

                if (enemy.name == "Enemy1(Clone)")
                    velocity = 10f / 2;
                else
                    velocity = 10f;


                break;
            case "PillB(Clone)":
                if (enemy.name == "Enemy1(Clone)")
                {
                    GameObject bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);
                    Vector3 enemyPosition = enemy.transform.position;
                    float xDistance = enemyPosition.x - transform.position.x;
                    float yDistance = enemyPosition.y - transform.position.y;
                    float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
                    float xDirection = 1.3f * velocity * xDistance / distance;
                    float yDirection = 1.3f * velocity * yDistance / distance;
                    Vector3 direction = new Vector3(xDirection, yDirection);
                    bullet.GetComponent<Rigidbody2D>().velocity = direction;
                }
                break;
            case "PillC(Clone)":

                if (enemy.name == "Enemy2(Clone)")
                {
                    enemy.GetComponent<Rigidbody2D>().velocity = enemy.GetComponent<Rigidbody2D>().velocity * 2;

                }

                break;
            case "PillD(Clone)":
                break;
        }
        shoot(enemy, velocity);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        timeElapseEnemyDmg += Time.deltaTime;
        if (timeElapseEnemyDmg >= timeBetweenEnemyDmg)
        {
            print(gameObject.name);
            int dmgTaken = collision.gameObject.GetComponent<AEnemieScript>().getDmg(gameObject.name);
            dmgTaken = (int)(dmgTaken*defenseBuff);
            displayDmgTaken(dmgTaken);
            life -= dmgTaken;
            timeElapseEnemyDmg = 0;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void displayDmgTaken(int dmgTaken)
    {
        GameObject canvasText = Instantiate(canvasTextPref, transform.position, Quaternion.identity, transform);
        canvasText.transform.GetChild(0).gameObject.GetComponent<Text>().text = dmgTaken.ToString();
        Destroy(canvasText, 0.5f);
    }

    public void startBuff(string buffID)
    {
        switch (buffID)
        {
            case "Buff1(Clone)":
                applyingBuff = 1;
                velocityBuff *= 1.1f;
                break;
            case "Buff2(Clone)":
                applyingBuff = 2;
                knockBackBullet = true;
                atackBuff = 1.1f;
                break;
            case "Buff3(Clone)":
                applyingBuff = 3;
                knockBackPill = true;
                defenseBuff = 1.2f;
                break ;
            case "Buff4(Clone)":
                applyingBuff = 4;
                atackBuff = 0.9f;
                poison = true;
                break;
        }
    }
}
