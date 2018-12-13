using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followTarget;
    private Camera camera;

    public float dampTime = 0.15f;
    public float smooth = 1.5f;
    private Vector3 velocity = Vector3.zero;
    private float cameraZ = 0;


    void Start()
    {
        cameraZ = transform.position.z;
        camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (followTarget)
        {
            Vector3 delta = followTarget.transform.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cameraZ));
            Vector3 destination = transform.position + delta;
            destination.z = cameraZ;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.rotation = Quaternion.Euler(0, 0, followTarget.transform.eulerAngles.z);
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, followTarget.transform.eulerAngles.z), Time.deltaTime * smooth);
        }
    }
}