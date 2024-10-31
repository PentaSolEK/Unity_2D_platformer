using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ded : MonoBehaviour
{
    private int originMask;
    public GameObject lvlup;

    private void Awake()
    {
        originMask = Camera.main.cullingMask;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Camera.main.cullingMask = 1 << LayerMask.NameToLayer("UI");
            lvlup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Camera.main.cullingMask = originMask;
        lvlup.SetActive(false);
    }
}

