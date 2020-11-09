using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField]
    ScoreScript newscore;
    List<GameObject> conectionLinesArray = new List<GameObject>();
    [SerializeField]
    GameObject connectionLinePref;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void makeLine()
    {
        int makingConnection = PlayerPrefs.GetInt("MakingConnection", -1);
        print(makingConnection);
        if (makingConnection == 0)
        {
            PlayerPrefs.SetInt("MakingConnection", 1);
            print("chagne");
            DrawLine();
        }
        else if(makingConnection == 1)
        {
            GameObject connectionLine = GameObject.FindGameObjectWithTag("LineInProgress");
            ConnectionLineScript connectionLineScript = connectionLine.GetComponent<ConnectionLineScript>();
            connectionLineScript.makeConnection(transform.position);
            PlayerPrefs.SetInt("MakingConnection", 0);
            conectionLinesArray.Add(connectionLine);
            newscore.CountConect();
        }
    }
  
    void DrawLine()
    {
        GameObject connectionLine=Instantiate(connectionLinePref, new Vector3(0, 0, 0), Quaternion.identity);
        LineRenderer line = connectionLine.GetComponent<LineRenderer>();
        line.SetPosition(1, gameObject.transform.position);
        conectionLinesArray.Add(connectionLine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            foreach(GameObject i in conectionLinesArray)
            {
                Destroy(i);
                newscore.DestroyConect();
            }
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }

    }
}
