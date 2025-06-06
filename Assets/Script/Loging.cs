using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loging : MonoBehaviour
{
    public void ButtonLog()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
    }

    public void GameExit()

    {
        Application.Quit();
    }
}
