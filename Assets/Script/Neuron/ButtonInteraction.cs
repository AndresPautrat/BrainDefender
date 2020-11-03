using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        //DrawLine(new Vector3(0, 0), gameObject.transform.position, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeLine()
    {
        float lineNeuronX = PlayerPrefs.GetFloat("LineNeuronX", 999999);
        float lineNeuronY = PlayerPrefs.GetFloat("LineNeuronY", 999999);
        print(lineNeuronY);
        if (lineNeuronX > 999998)
        {
            PlayerPrefs.SetFloat("LineNeuronX", gameObject.transform.position.x);
            PlayerPrefs.SetFloat("LineNeuronY", gameObject.transform.position.y);
        }
        else
        {
            print("Created");
            DrawLine(new Vector3(lineNeuronX, lineNeuronY), gameObject.transform.position, Color.red);
            PlayerPrefs.SetFloat("LineNeuronX", 999999);
            PlayerPrefs.SetFloat("LineNeuronY", 999999);
        }
        
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        lr = myLine.GetComponent<LineRenderer>();
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
