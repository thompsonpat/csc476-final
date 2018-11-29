using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
	private GameObject woodPrefab;

    void Start()
    {
		InvokeRepeating("LaunchProjectile", 2.0f, 0.3f);
    }

    void Update()
    {

    }

	void SpawnWood()
	{

	}
}
