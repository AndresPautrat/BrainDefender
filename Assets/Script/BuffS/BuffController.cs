using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    [SerializeField]
    GameObject[] buffes;
    int effect = -1;
    float timeElapse = 0;
    float timeBetween = 1;
    string destroyerTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapse += Time.deltaTime;
        if (timeElapse >= timeBetween)
        {
            
        }
        timeElapse += Time.deltaTime;
    }
    
    public int getEffect()
    {
        return effect;
    }

    public void setEffect(int _effect)
    {
        effect = _effect;
        timeElapse = 0;
    }
}
