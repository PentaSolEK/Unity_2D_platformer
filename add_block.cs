using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class add_block : MonoBehaviour
{
    public GameObject Object1;
    public GameObject Object2;
    public GameObject Object3;
    public GameObject Object4;
    public GameObject Object5;
    public GameObject Object6;
    public GameObject Object7;
    public GameObject Object8;
    public GameObject Object9;
    public GameObject Object10;
    public Button button;
    public int counter;

    private void Start()
    {
        button.onClick.AddListener(SpawnSprite);

    }

    void SpawnSprite()
    {
        switch (counter)
        {
            case 0:
                Object1.SetActive(true);
                break;
            case 1:
                Object2.SetActive(true);
                break;
            case 2:
                Object3.SetActive(true);
                break;
            case 3:
                Object4.SetActive(true);
                break;
            case 4:
                Object5.SetActive(true);
                break;
            case 5:
                Object6.SetActive(true);
                break;
            case 6:
                Object7.SetActive(true);
                break;
            case 7:
                Object8.SetActive(true);
                break;
            case 8:
                Object9.SetActive(true);
                break;
            case 9:
                Object10.SetActive(true);
                break;

        }
        counter++;
    }
}
