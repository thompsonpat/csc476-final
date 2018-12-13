using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public Sprite dinghySprite;
    public Sprite hullSprite;

    [Header("Set in Inspector")]
    public int level = 1;
    public int maxHealth = 2;
    public int health;
    public float accel = 1.2f;
    public float turnSpeed = 2.5f;
    public float maxSpeed = 10f;
    public int sailsDown = 0;
    public int maxSailsDown = 3;
    public float speed = 0;

    // public bool canShoot = true;

    [Header("Inventory")]
    public int wood = 0;
    public int crew = 1;

    private Rigidbody2D rb;

    void Start()
    {
        health = maxHealth;
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

        // If 'Down'
        if (Input.GetKeyDown(KeyCode.Space)) ShootCannons("Front");
        if (Input.GetKeyDown(KeyCode.Q)) ShootCannons("LeftSide");
        if (Input.GetKeyDown(KeyCode.E)) ShootCannons("RightSide");

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            LevelUp();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(gameObject.name + " : " + other.gameObject.name + " : " + Time.time);
        if (other.tag == "Wood") wood += 1;
        if (other.tag == "Crew") crew += 1;
        Destroy(other.gameObject);
    }

    void ShootCannons(string side)
    {
        int count = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.Contains("Cannon"))
            {
                if (child.gameObject.tag == side)
                {
                    child.gameObject.SendMessage("ShootCannon");
                }
            }
        }
    }

    void LevelUp()
    {
        if (level == 1)
        {
            accel = .9f;
            turnSpeed = 2.5f;
            sailsDown = 1;
            maxSailsDown = 1;
            gameObject.GetComponent<SpriteRenderer>().sprite = hullSprite;
            gameObject.GetComponent<Rigidbody2D>().mass = 1.2f;
            level++;
        }
    }
}
