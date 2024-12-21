using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool pause = false;
    private void Update()
    {
        // Проверяем нажатие кнопки Esc
        if (Input.GetKeyDown(KeyCode.Escape) && Hero.IsAlive())
        {
            // Если меню паузы уже открыто, закрываем его; иначе, открываем
            if (pause)
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
        pause = true;
        Time.timeScale = 0f;

        // Включаем меню паузы
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        // Возобновляем игру
        pause = false;
        Time.timeScale = 1f;

        // Выключаем меню паузы
        pauseMenuUI.SetActive(false);
    }
}
