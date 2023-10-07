using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour
{
    private FixedJoint2D _fixedJoint2D;
    private bool glued = false;
    private void Awake()
    {
        glued = false;
        _fixedJoint2D = GetComponent<FixedJoint2D>();
        _fixedJoint2D.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!glued)
        {
            _fixedJoint2D.connectedBody = col.rigidbody;
            _fixedJoint2D.enabled = true;
            glued = true;
        }
    }
}
