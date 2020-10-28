using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public GameObject proyectil;
    bool arrive;
 
    public void Shoot(Vector3 spawn, Vector3 target)
    {
        proyectil.transform.position = new Vector2(spawn.x, spawn.y);
        while (arrive == true)
        {
            if (target.x >= spawn.x)
            {
                proyectil.transform.position = transform.position + new Vector3(1, 0, 0);
            }
            else
            {
                proyectil.transform.position = transform.position + new Vector3(-1, 0, 0);
            }
            if (target.y >= spawn.y)
            {
                proyectil.transform.position = transform.position + new Vector3(0, 1, 0);
            }
            else
            {
                proyectil.transform.position = transform.position + new Vector3(0, -1, 0);
            }

            if (target.Equals(proyectil))
            {
                arrive = false;
            }
        }

    }

    void Update()
    {
        
    }
}
