using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    Text scoreText = null;
    private int score = 0;
    private int conect = 0;
    double timeElapseScore = 0;
    double generateScore = 1;
    void Start()
    {
        scoreText = GameObject.Find("TextScore").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapseScore += Time.deltaTime;
        if (conect > 0)
        {
            if (timeElapseScore > generateScore)
            {
                score += 20;
                scoreText.text = "SCORE:" + score;
                print(score);
                timeElapseScore = 0;
            }
        }
    }
    public void CountConect(int con)
    {
        conect = con;
    }
}
