using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public GameObject cannonBallPrefab;

    public bool canShoot;
    public int reloadTime = 200;
    public int coolDownTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (coolDownTime > 0) coolDownTime -= 1;
        if (coolDownTime == 0) canShoot = true;
    }

    void ShootCannon()
    {
        if (canShoot)
        {
            GameObject cannonBall = Instantiate(cannonBallPrefab, transform.position, transform.rotation);
            cannonBall.SendMessage("ParentInfo", transform.parent.gameObject);
			canShoot = false;
            coolDownTime = reloadTime;
        }
    }
}
