using System;
using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static Action startGame;
    public static Action loseGame;
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    private bool started = false;
    [SerializeField] private float startRadius;
    private float startSqrRadius;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startSqrRadius = startRadius * startRadius;
        started = false;
    }

    public void LoseGame()
    {
        StartCoroutine(TimeWarpSlow());
        loseGame?.Invoke();
    }

    /// <summary>
    /// dont copy this code
    /// </summary>
    IEnumerator TimeWarpSlow()
    {
        while (Time.timeScale > 0)
        {
            Time.timeScale -= Time.deltaTime;
            yield return null;
        }

        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (!started)
        {
            CheckForMouseInZone();
        }
    }

    private void CheckForMouseInZone()
    {
        var distance = (transform.position - Utility.MouseWorldPos(transform.position.z)).sqrMagnitude;
        if (distance < startSqrRadius)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        Debug.Log("M<ASDLKasd");
        started = true;
        startGame?.Invoke();
    }
}