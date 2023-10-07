using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIHUD : MonoBehaviour
{
    private float _timer;
    private bool _timing;
    [SerializeField] private TMP_Text timerText;
    private void Awake()
    {
        _timing = false;
        _timer = 0;
    }

    private void OnEnable()
    {
        GameManager.loseGame += LoseGame;
        GameManager.startGame += StartGame;
    }

    private void OnDisable()
    {
        GameManager.startGame -= LoseGame;
        GameManager.startGame -= StartGame;

    }

    private void StartGame()
    {
        _timing = true;
        _timer = 0;
    }

    private void Update()
    {
        if (_timing)
        {
            _timer += Time.deltaTime;
        }

        timerText.text = _timer.ToString("N1");
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void LoseGame()
    {
        _timing = false;
    }

    public float GetScore()
    {
        return _timer;
    }
}
