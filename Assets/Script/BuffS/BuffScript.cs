using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffScript : MonoBehaviour
{
    [SerializeField]
    GameObject canvasTextPref;

    [SerializeField]
    int life = 100;

    float timeElapseEnemyDmg = 0;
    float timeBetweenEnemyDmg = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            timeElapseEnemyDmg += Time.deltaTime;
            if (timeElapseEnemyDmg >= timeBetweenEnemyDmg)
            {
                int dmgTaken = 20;
                dmgTaken = (int)(dmgTaken);
                displayDmgTaken(dmgTaken, Color.red);
                life -= dmgTaken;
                timeElapseEnemyDmg = 0;
                if (life <= 0)
                {
                    sendBuff(collision.tag);
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            life -= 10;
            Destroy(collision.gameObject);
        }

        if (life <= 0)
        {
            sendBuff(collision.tag);
            Destroy(this.gameObject);
        }
    }

    void sendBuff(string targetName)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Pill"); ;
        if (targetName == "Enemy")
        {
            targets = GameObject.FindGameObjectsWithTag(targetName);
        }
        for (int i = 0; i < targets.Length; i++)
        {
            if (targetName == "Enemy")
            {
                targets[i].GetComponent<AEnemieScript>().startBuff(this.name);
            }
            else if (targetName == "Bullet")
            {
                targets[i].GetComponent<PIllGreenScript>().startBuff(this.name);
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
}
