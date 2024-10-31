using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private TextMeshProUGUI _field;
    private int _score = 0;

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
        _score += 1;
        _field.text = _score.ToString();    
    }
}
