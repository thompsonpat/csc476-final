using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

	public float startHealth = 10;
	private float health;

	public Image healthBar;

    // Use this for initialization
    void Start()
    {
		health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void TakeDamage(float amount)
	{
		health -= amount;
		healthBar.fillAmount = health / startHealth;

		if (health <= 0)
			Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(gameObject.name + " : " + other.gameObject.name + " : " + Time.time);

        if (other.tag == "CannonBall")
		{
			TakeDamage(1.0f);
		}

        Destroy(other.gameObject);
    }
}
