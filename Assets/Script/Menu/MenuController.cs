﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void SceneGame()
    {
        SceneManager.LoadScene("Game");

    }

    public void Exit()
    {
        print("exit");
        Application.Quit();
    }

}
