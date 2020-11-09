using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BulletA" || collision.tag == "BulletB" || collision.tag == "BulletC" || collision.tag == "BulletD")
        {
            Destroy(collision.gameObject);
        }
    }
}
