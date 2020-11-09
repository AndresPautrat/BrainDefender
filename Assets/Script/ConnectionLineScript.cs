using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionLineScript : MonoBehaviour
{
    LineRenderer line;
    public bool waitingSecondConnection=true;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingSecondConnection)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 worldPoint2d = new Vector2(worldPoint.x, worldPoint.y);
            line.SetPosition(0, worldPoint2d);
        }
    }

    public void makeConnection(Vector3 newPosition)
    {
        waitingSecondConnection = false;
        line.SetPosition(0, newPosition);
        gameObject.tag="Untagged";
    }
}
