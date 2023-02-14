using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RusitosCarController : MonoBehaviour
{
    Vector2 movement;
    public Animator wheelAnimator;
    public GameObject speedometer;
    public GameObject accelmeter;
    public GameObject centerMass;

    Rigidbody carBody;

    bool isAutopilot = false;

    public WheelCollider wheelFrontRight;
    public WheelCollider wheelFrontLeft;
    public WheelCollider wheelBackRight;
    public WheelCollider wheelBackLeft;

    public float accel = 500f;
    public float brakeForce = 1000f;
    public float maxTurnAngle = 30f;

    float currentAccel = 0;
    float currentBrakeForce = 0;
    float currentTurnAngle = 0;

    void Start()
    {
        carBody = GetComponent<Rigidbody>();
        carBody.centerOfMass = centerMass.transform.localPosition;

        isAutopilot = true;
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (isAutopilot)
        {
            currentAccel = accel * 1;
            accelmeter.transform.rotation = Quaternion.Euler(0, 0, -50);
        }
        else
        {
            currentAccel = accel * movement.y;
            accelmeter.transform.rotation = Quaternion.Euler(0, 0, -movement.y * 50);
        }

        currentTurnAngle = maxTurnAngle * movement.x;

        if (movement.y != 0)
        {
            isAutopilot = false;
        }

        speedometer.transform.rotation = Quaternion.Euler(
            0,
            0,
            Mathf.Clamp(-carBody.velocity.z * 2, -160, 0)
        );

        if (movement.x == -1)
        {
            wheelAnimator.SetBool("turnLeft", true);
            wheelAnimator.SetBool("turnRight", false);
        }
        if (movement.x == 0)
        {
            wheelAnimator.SetBool("turnLeft", false);
            wheelAnimator.SetBool("turnRight", false);
        }
        if (movement.x == 1)
        {
            wheelAnimator.SetBool("turnRight", true);
            wheelAnimator.SetBool("turnLeft", false);
        }

        if (Input.GetButton("Jump"))
        {
            currentBrakeForce = brakeForce;
        }
        else
        {
            currentBrakeForce = 0;
        }

        if (Input.GetButton("RotateCar"))
        {
            transform.position = new Vector3(-5, 8, transform.position.z);
            transform.rotation = Quaternion.identity;
        }

        wheelFrontLeft.motorTorque = currentAccel;
        wheelFrontRight.motorTorque = currentAccel;
        wheelBackLeft.motorTorque = currentAccel;
        wheelBackRight.motorTorque = currentAccel;

        wheelFrontLeft.brakeTorque = currentBrakeForce;
        wheelFrontRight.brakeTorque = currentBrakeForce;
        wheelBackLeft.brakeTorque = currentBrakeForce;
        wheelBackRight.brakeTorque = currentBrakeForce;

        wheelFrontLeft.steerAngle = currentTurnAngle;
        wheelFrontRight.steerAngle = currentTurnAngle;
    }
}
