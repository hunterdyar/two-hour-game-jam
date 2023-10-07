using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILosePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    private void Awake()
    {
        transform.SetActiveChildren(false);
    }

    private void OnEnable()
    {
        GameManager.loseGame += LoseGame;
    }

    private void OnDisable()
    {
        GameManager.loseGame -= LoseGame;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void LoseGame()
    {
        SetHighScore();
        transform.SetActiveChildren(true);
    }

    void SetHighScore()
    {
        UIHUD hud = GameObject.FindObjectOfType<UIHUD>();
        var score = hud.GetScore();
        var highSCore = PlayerPrefs.GetFloat("HighScore", 0);
        if (highSCore < score)
        {
            highSCore = score;
            PlayerPrefs.SetFloat("HighScore",highSCore);
        }

        highScoreText.text = "High Score: " + highSCore.ToString("N3");
    }
}
