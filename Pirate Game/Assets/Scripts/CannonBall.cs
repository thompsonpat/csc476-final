using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{

    public int speed = 150;
    public float range = 2.5f;

    [Header("Set Dynamically")]
    public GameObject parent;
    public GameObject grandParent;

    private Rigidbody2D rb;
    private Rigidbody2D grandParentRB;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = grandParentRB.velocity;
        if (parent.tag == "Front") rb.AddForce(transform.up * speed);
        if (parent.tag == "LeftSide") rb.AddForce(-transform.right * speed);
        if (parent.tag == "RightSide") rb.AddForce(transform.right * speed);
        Destroy(this.gameObject, range);
    }

    void FixedUpdate()
    {
        
    }

    void ParentInfo(object[] parents)
    {
        grandParent = (GameObject) parents[0];
        parent = (GameObject) parents[1];
        grandParentRB = grandParent.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(grandParent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}