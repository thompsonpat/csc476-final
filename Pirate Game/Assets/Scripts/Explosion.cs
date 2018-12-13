using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	public Sprite[] explosionImages = new Sprite[3];

    // Use this for initialization
    void Start()
    {
		this.GetComponent<SpriteRenderer>().sprite = explosionImages[Random.Range(0, explosionImages.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
