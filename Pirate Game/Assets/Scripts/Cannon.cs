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
			object[] tempArray = new object[2];
			tempArray[0] = transform.parent.gameObject;
			tempArray[1] = this.gameObject;
            cannonBall.SendMessage("ParentInfo", tempArray);
			canShoot = false;
            coolDownTime = reloadTime;
        }
    }
}
