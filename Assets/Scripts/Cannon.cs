using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cannon : MonoBehaviour
{
    private BoxCollider2D _spawnArea;
    [SerializeField] private Vector2 ShootDirection;

    [SerializeField]
    private float minShootForce;
    [SerializeField]

    private float maxShootForce;
    [SerializeField] private GameObject[] cannonBallPrefabs;
    [SerializeField] private float minShootDelay;
    [SerializeField] private float maxShootDelay;
    [Range(0f,1f)]
    [SerializeField] private float timeShrinkPercent;
    private float _shootDelay;
    private void Awake()
    {
        _spawnArea = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        GameManager.startGame += StartGame;
    }

    private void OnDisable()
    {
        GameManager.startGame -= StartGame;
    }

    private void StartGame()
    {
        StartCoroutine(ShootTimer());
    }
    
    IEnumerator ShootTimer()
    {
        while (gameObject.activeSelf)
        {
            _shootDelay = Random.Range(minShootDelay, maxShootDelay);
            yield return new WaitForSeconds(_shootDelay);
            ShootCanon();
        }
    }
    
    [ContextMenu("Shoot Cannon")]
    void ShootCanon()
    {
        var pos = _spawnArea.bounds.RandomInBounds();
        GameObject prefab = cannonBallPrefabs.RandomItem();
        var newObject = Instantiate(prefab, pos, Quaternion.identity);
        var newRB = newObject.GetComponent<Rigidbody2D>();
        var shootForce = Random.Range(minShootForce, maxShootForce);
        newRB.AddForce(ShootDirection*shootForce,ForceMode2D.Impulse);
        ShrinkTime();
    }

    void ShrinkTime()
    {
        minShootDelay = Mathf.Max(Time.deltaTime, minShootDelay * 1-timeShrinkPercent);
        maxShootDelay = Mathf.Max(0.05f, maxShootDelay * 1-timeShrinkPercent);
    }
}
