using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{

    public int speed = 10;
    public float range = 1.0f;

	[Header("Set Dynamically")]
	public GameObject parent;

	private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		Destroy(this.gameObject, range);
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.right * speed);
    }

	void ParentInfo (GameObject parentGO)
	{
		parent = parentGO;
		Physics2D.IgnoreCollision(parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
}
