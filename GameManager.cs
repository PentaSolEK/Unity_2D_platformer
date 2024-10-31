using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private void Update()
    {
        // ��������� ������� ������ Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ���� ���� ����� ��� �������, ��������� ���; �����, ���������
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
        // ������������� ����
        Time.timeScale = 0f;

        // �������� ���� �����
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        // ������������ ����
        Time.timeScale = 1f;

        // ��������� ���� �����
        pauseMenuUI.SetActive(false);
    }
}
