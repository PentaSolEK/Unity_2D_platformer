using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private TextMeshProUGUI _field;
    public static int _score;

    private void Awake()
    {
        _field = GetComponent<TextMeshProUGUI>();

    }

    private void Start()
    {
        _field.text = _score.ToString();
    }

    public void Increase()
    {
        _score += 5;
        _field.text = _score.ToString();    
    }
}
