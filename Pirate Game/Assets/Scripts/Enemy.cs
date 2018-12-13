using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Sprite dinghySprite;
    public Sprite dinghySpriteMed;
    public Sprite dinghySpriteLow;
    public Sprite hullSprite;
    public Sprite hullSpriteMed;
    public Sprite hullSpriteLow;

	public GameObject explosionPrefab;
    public GameObject woodPrefab;
    public GameObject crewPrefab;

    public float startHealth = 3;
    public float speed = 2;
    private int level = 1;
    private float health;
	private float range = 2.0f;

    public Image healthBar;

    // Use this for initialization
    void Start()
    {
        health = startHealth;
        if (Random.Range(0.0f, 1.0f) > 0.7f) level = 2;
		// Apply level 2 attributes
        if (level == 2)
        {
			startHealth = 5;
			health = startHealth;
			range = 3.0f;
			if (Random.Range(0.0f, 1.0f) > 0.5f) this.transform.Find("Cannon2").gameObject.SetActive(true);
			gameObject.GetComponent<Rigidbody2D>().mass = 5;
			gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.4f, 1);
            gameObject.GetComponent<SpriteRenderer>().sprite = hullSprite;
        }
    }

    void FixedUpdate()
    {
        // Finds all colliders within certain radius from given point
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), range);
        foreach (var collider in collidersHit)
        {
            if (collider.tag == "Player")
            {
                AimTowardsPlayer(collider.gameObject);
                return;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Instantiate(crewPrefab, this.transform.position + new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0), Quaternion.identity);
            Instantiate(woodPrefab, this.transform.position + new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void UpdateBoatSprite()
    {
        if (level == 1)
        {
            if ((health / startHealth) > 0.7f) gameObject.GetComponent<SpriteRenderer>().sprite = dinghySprite;
            if ((health / startHealth) < 0.7f) gameObject.GetComponent<SpriteRenderer>().sprite = dinghySpriteMed;
            if ((health / startHealth) < 0.4f) gameObject.GetComponent<SpriteRenderer>().sprite = dinghySpriteLow;
        }
        if (level == 2)
        {
            if ((health / startHealth) > 0.7f) gameObject.GetComponent<SpriteRenderer>().sprite = hullSprite;
            if ((health / startHealth) < 0.7f) gameObject.GetComponent<SpriteRenderer>().sprite = hullSpriteMed;
            if ((health / startHealth) < 0.4f) gameObject.GetComponent<SpriteRenderer>().sprite = hullSpriteLow;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CannonBall")
        {
            var explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            Destroy(explosion, .15f);
            TakeDamage(1.0f);
        }
        Destroy(other.gameObject);
    }

    void AimTowardsPlayer(GameObject player)
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        ShootCannons("RightSide");
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
}
