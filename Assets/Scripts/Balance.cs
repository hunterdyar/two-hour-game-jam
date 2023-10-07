using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    Camera _camera;
    private Rigidbody2D _rb;

    [SerializeField] float maxSpeed;

    private bool balancing = false;

    private void Awake()
    {
        balancing = false;
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
    }
    private void OnEnable()
    {
        GameManager.startGame += StartBalancing;
    }

    private void OnDisable()
    {
        GameManager.startGame -= StartBalancing;
    }
    void Start()
    {
        _camera = Camera.main;
    }

    //todo: add to utility scripts


    public void StartBalancing()
    {
        balancing = true;
        _rb.isKinematic = false;
    }

private void FixedUpdate()
    {
        if (balancing)
        {
            //move towards mouse
            Vector2 direction = Utility.MouseWorldPos() - transform.position;
            float distance = direction.magnitude;
            direction.Normalize();
            direction = direction * Mathf.Min(distance, maxSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + direction);
        }
    }
}