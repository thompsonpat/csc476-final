using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Prefabs and Sprites")]
    public GameObject cannonBallPrefab;
    public GameObject explosionPrefab;
    public Sprite dinghySprite;
    public Sprite dinghySpriteMed;
    public Sprite dinghySpriteLow;
    public Sprite hullSprite;
    public Sprite hullSpriteMed;
    public Sprite hullSpriteLow;
    public Sprite largeSailSprite;
    public Sprite largeSailSpriteMed;
    public Sprite largeSailSpriteLow;

    [Header("UI Elements")]
    public Image healthBar;
    public Image sailsDownBar;
    public Text crewText;
    public Text woodText;
    public Text cannonCost;
    public Text sailCost;
    public Text hullCost;
    public Button addCannonLBtn;
    public Button addCannonRBtn;
    public Button addSailBtn;
    public Button upgradeHullBtn;

    [Header("Set in Inspector")]
    public int level = 1;
    public int maxHealth = 3;
    public int health;
    public float accel = 1.2f;
    public float turnSpeed = 0.5f;
    public float maxSpeed = 10f;
    public int sailsDown = 0;
    public int maxSailsDown = 3;
    public float speed = 0;

    [Header("Resource Costs")]
    public int sailLevel = 0;
    public int sailWoodCost = 4;
    public int sailCrewCost = 2;
    public int cannonWoodCost = 2;
    public int cannonCrewCost = 2;
    public int hullWoodCost = 10;
    public int hullCrewCost = 5;

    private int maxLeftCannons = 2;
    private int maxRightCannons = 2;
    private int numLeftCannons = 0;
    private int numRightCannons = 0;

    [Header("Inventory")]
    public int wood = 0;
    public int crew = 0;

    private Rigidbody2D rb;

    void Start()
    {
        health = maxHealth;
        UpdateUIText();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // If 'Up'
        if (Input.GetKeyDown(KeyCode.W))
        {
            sailsDown += 1;
            if (sailsDown > maxSailsDown) sailsDown = maxSailsDown;
            UpdateUIText();
        }
        // If 'Down'
        if (Input.GetKeyDown(KeyCode.S))
        {
            sailsDown -= 1;
            if (sailsDown < 0) sailsDown = 0;
            UpdateUIText();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) ShootCannons("Front");
        if (Input.GetKeyDown(KeyCode.LeftArrow)) ShootCannons("LeftSide");
        if (Input.GetKeyDown(KeyCode.RightArrow)) ShootCannons("RightSide");

        if (Input.GetKeyDown(KeyCode.Q)) AddCannon("Left");
        if (Input.GetKeyDown(KeyCode.E)) AddCannon("Right");

        if (Input.GetKeyDown(KeyCode.Keypad0)) UpgradeHull();
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
        if (other.tag == "Wood")
        {
            if (health < maxHealth) health += 1;
            else wood += 1;
            UpdateBoatSprite();
            healthBar.fillAmount = (float)health / (float)maxHealth;
            Destroy(other.gameObject);
        }
        if (other.tag == "Crew")
        {
            crew += 1;
            Destroy(other.gameObject);
        }

        if (other.tag == "CannonBall")
        {
            var explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            Destroy(explosion, .15f);
            TakeDamage(1);
            Destroy(other.gameObject);
        }
        UpdateUIText();
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = (float)health / (float)maxHealth;
        if (health == 0)
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        UpdateBoatSprite();
    }

    void UpdateBoatSprite()
    {
        if (level == 1)
        {
            if ((health / maxHealth) > 0.7f) gameObject.GetComponent<SpriteRenderer>().sprite = dinghySprite;
            if ((health / maxHealth) < 0.7f) gameObject.GetComponent<SpriteRenderer>().sprite = dinghySpriteMed;
            if ((health / maxHealth) < 0.4f) gameObject.GetComponent<SpriteRenderer>().sprite = dinghySpriteLow;
        }
        if (level == 2)
        {
            if ((health / maxHealth) > 0.7f)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = hullSprite;
                this.transform.Find("SailLg").GetComponent<SpriteRenderer>().sprite = largeSailSprite;
            }
            if ((health / maxHealth) < 0.7f)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = hullSpriteMed;
                this.transform.Find("SailLg").GetComponent<SpriteRenderer>().sprite = largeSailSpriteMed;
            }
            if ((health / maxHealth) < 0.4f)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = hullSpriteLow;
                this.transform.Find("SailLg").GetComponent<SpriteRenderer>().sprite = largeSailSpriteLow;
            }
        }
    }

    void ShootCannons(string side)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.Contains("Cannon"))
            {
                if (child.gameObject.tag == side)
                {
                    if (child.gameObject.activeSelf) child.gameObject.SendMessage("ShootCannon");
                }
            }
        }
    }

    public void UpgradeHull()
    {
        if (level == 1)
        {
            accel = 15.0f;
            turnSpeed = 20.0f;
            sailsDown = 1;
            maxSailsDown = 1;
            maxHealth = 5;
            health = maxHealth;
            gameObject.GetComponent<Rigidbody2D>().mass = 5;
            gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.4f, 1);
            gameObject.GetComponent<SpriteRenderer>().sprite = hullSprite;
            this.transform.Find("Front Cannon").transform.localPosition = new Vector2(0, .4f);

            wood -= hullWoodCost;
            crew -= hullCrewCost;

            level += 1;
            UpdateUIText();
        }
    }

    public void AddSail()
    {
        if (level == 2 && crew >= sailCrewCost && wood >= sailWoodCost)
        {
            if (sailLevel == 0)
            {
                this.transform.Find("SailSm").gameObject.SetActive(true);
                sailWoodCost += 2;
                sailCrewCost += 2;
                sailLevel += 1;
            }
            if (sailLevel == 1)
            {
                this.transform.Find("SailLg").gameObject.SetActive(true);
                sailLevel += 1;
            }
            UpdateUIText();
        }
    }

    public void AddCannon(string side)
    {
        if ((crew >= cannonCrewCost) && (wood >= cannonWoodCost))
        {
            if (side == "Left" && (numLeftCannons < maxLeftCannons))
            {
                if (numLeftCannons == 0) this.transform.Find("CannonL1").gameObject.SetActive(true);
                if (numLeftCannons == 1) this.transform.Find("CannonL2").gameObject.SetActive(true);
                numLeftCannons += 1;
            }

            if (side == "Right" && (numRightCannons < maxRightCannons))
            {
                if (numRightCannons == 0) this.transform.Find("CannonR1").gameObject.SetActive(true);
                if (numRightCannons == 1) this.transform.Find("CannonR2").gameObject.SetActive(true);
                numRightCannons += 1;
            }

            // Cannon resource cost
            crew -= cannonCrewCost;
            wood -= cannonWoodCost;
            UpdateUIText();
        }
    }

    void UpdateUIText()
    {
        crewText.text = "FREE CREW: " + crew;
        woodText.text = "WOOD: " + wood;
        sailsDownBar.fillAmount = (float)sailsDown / (float)maxSailsDown;
        cannonCost.text = "Cannon: " + cannonWoodCost + " Wood, " + cannonCrewCost + " Crew";
        sailCost.text = "Sail: " + sailWoodCost + " Wood, " + sailCrewCost + " Crew";
        hullCost.text = "Hull: " + hullWoodCost + " Wood, " + hullCrewCost + " Crew";

        if (level == 1)
        {
            upgradeHullBtn.interactable = false;
            if (wood >= hullWoodCost && crew >= hullCrewCost)
            {
                upgradeHullBtn.interactable = true;
            }
            addSailBtn.interactable = false;
            addCannonLBtn.interactable = false;
            addCannonRBtn.interactable = false;

            if (crew >= cannonCrewCost && wood >= cannonWoodCost && numLeftCannons < 2)
            {
                addCannonLBtn.interactable = true;
            }

            if (crew >= cannonCrewCost && wood >= cannonWoodCost && numRightCannons < 2)
            {
                addCannonRBtn.interactable = true;
            }
        }
        if (level == 2)
        {
            upgradeHullBtn.interactable = false;
            if (crew >= sailCrewCost && wood >= sailWoodCost && sailLevel < 2)
            {
                addSailBtn.interactable = true;
            }
        }
    }

    void AddLeftCannon()
    {
        AddCannon("Left");
    }
    void AddRightCannon()
    {
        AddCannon("Right");
    }
}
