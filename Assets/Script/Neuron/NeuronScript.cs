using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronScript : MonoBehaviour
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
        if(collision.tag == "Enemy")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,0.0f);
        }
        
    }
}
