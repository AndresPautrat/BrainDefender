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

    double timeElapse = 1;
    double recoil = 1;
    double recoilBase = 1;

    [SerializeField]
    GameObject canvasTextPref;

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
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
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
}
