using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private HingeJoint2D _hingeJoint2D;
    private Rigidbody2D _rigidbody2D;
    private bool broken;
    [SerializeField] private float angleLimit;
    // Start is called before the first frame update
    private void Awake()
    {
        _hingeJoint2D = GetComponent<HingeJoint2D>();
        broken = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = true;
        
    }

    private void OnEnable()
    {
        GameManager.startGame += StartStick;
    }

    private void OnDisable()
    {
        GameManager.startGame -= StartStick;
    }

    public void StartStick()
    {
        _rigidbody2D.isKinematic = false;
    }
    void Update()
    {   
        CheckAngles();
    }

    void CheckAngles()
    {
        if (!broken)
        {
            var angle = Vector2.Angle(Vector2.up, transform.up);
            if (angle > angleLimit)
            {
                BreakHinge();
            }
        }
    }

    void BreakHinge()
    {
        broken = true;
        _hingeJoint2D.attachedRigidbody.AddForce(Vector2.up*10,ForceMode2D.Impulse);
        _hingeJoint2D.enabled = false;
        GameManager.Instance.LoseGame();
    }
}
