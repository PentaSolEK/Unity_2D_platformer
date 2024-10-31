using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private void Update()
    {
        // Проверяем нажатие кнопки Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Если меню паузы уже открыто, закрываем его; иначе, открываем
            if (pauseMenuUI.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    void PauseGame()
    {
        // Останавливаем игру
        Time.timeScale = 0f;

        // Включаем меню паузы
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        // Возобновляем игру
        Time.timeScale = 1f;

        // Выключаем меню паузы
        pauseMenuUI.SetActive(false);
    }
}
