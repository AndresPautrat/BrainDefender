using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    [SerializeField]
    GameObject textObject;
    Text scoreText = null;
    private int score = 0;
    private int conect = 0;
    double timeElapseScore = 0;
    double generateScore = 1;
    void Start()
    {
        scoreText = textObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapseScore += Time.deltaTime;
        if (conect > 0)
        {
            if (timeElapseScore > generateScore)
            {
                score += 5*conect;
                scoreText.text = "SCORE:" + score;
                timeElapseScore = 0;
            }
        }
    }
    public void CountConect()
    {
        conect ++;
    }
    public void DestroyConect()
    {
        conect--;
    }
}
