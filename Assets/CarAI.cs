using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody carRigidbody;
    public GameObject centerMass;
    GameObject player;

    public float minDist = 2000;
    float dist = 0;

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = centerMass.transform.localPosition;
        player = GameObject.Find("MainCar");
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(0f, 0f, -1f);

        carRigidbody.AddForce(movement * speed, ForceMode.Impulse);

        dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > minDist)
        {
            Destroy(gameObject);
        }
    }
}
