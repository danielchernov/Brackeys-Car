using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RusitosCarController : MonoBehaviour
{
    Vector2 movement;
    public Animator wheelAnimator;

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

    void Start() { }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        currentAccel = accel * movement.y;
        currentTurnAngle = maxTurnAngle * movement.x;

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
