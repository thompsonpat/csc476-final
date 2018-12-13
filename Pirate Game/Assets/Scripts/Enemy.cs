using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public GameObject explosionPrefab;
	public GameObject woodPrefab;
	public GameObject crewPrefab;

	public float startHealth = 10;
	public float speed = 2;
	private float health;

	public Image healthBar;

    // Use this for initialization
    void Start()
    {
		health = startHealth;
    }

    void FixedUpdate()
    {
		// Finds all colliders within certain radius from given point
		Collider2D[] collidersHit = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.x), 5.0f);
		foreach(var collider in collidersHit)
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
		 Debug.Log(angle);
	}
}
