using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartNewGame()
    {
        Hero.lives = 5f;
        SceneManager.LoadScene("Hub");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("меню");
    }
}
