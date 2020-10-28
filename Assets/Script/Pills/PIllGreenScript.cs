using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PIllGreenScript : MonoBehaviour
{

    private Proyectile p;
    public GameObject turret;
    public AEnemieScript enemie;


    // Update is called once per frame
    void Update()
    {
        p.Shoot(turret.transform.position, enemie.transform.position);
    }
    
}
