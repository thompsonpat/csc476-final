﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Set in Inspector")]
    public float accel = 1f;
    public float turnSpeed = .5f;
    public float maxSpeed = 10f;
    public int sailsDown = 0;
    public int maxSailsDown = 3;
	public float speed = 0;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // If 'Up'
        if (Input.GetKeyDown(KeyCode.W))
        {
            sailsDown += 1;
            if (sailsDown > maxSailsDown) sailsDown = maxSailsDown;
        }
        // If 'Down'
        if (Input.GetKeyDown(KeyCode.S))
        {
            sailsDown -= 1;
            if (sailsDown < 0) sailsDown = 0;
        }
    }

    void FixedUpdate()
    {
        // If 'Left/Right' pressed add torque to turn
        if (Input.GetAxis("Horizontal") != 0)
        {
            // Scale the amount you can turn based on current velocity so slower turning below max speed
            float scale = Mathf.Lerp(0f, turnSpeed, rb.velocity.magnitude / maxSpeed);

            // Axis is opposite what we want by default
            rb.AddTorque(-Input.GetAxis("Horizontal") * scale);
        }

        // Add Force
        rb.AddRelativeForce(Vector2.up * (accel * sailsDown));

        // Cap Speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

		speed = rb.velocity.magnitude;
    }
}