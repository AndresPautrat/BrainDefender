using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : MonoBehaviour
{
    [SerializeField]
    int life = 100; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag);
        if(collision.tag == "Bullet")
        {
            print("algo");
            life -= 10;
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Enemy")
        {
            life -= 20;
        }

        if(life <= 0)
        {
            Destroy(this.gameObject);
            sendBuff(collision.tag);
        }
    }

    void sendBuff(string targetName)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetName);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targetName == "Enemy")
            {
                targets[i].GetComponent<AEnemieScript>().startBuff(this.name);
            }
            else if(targetName == "Pill")
            {
                targets[i].GetComponent<PIllGreenScript>().startBuff(this.name);
            }
        }
    }
}
