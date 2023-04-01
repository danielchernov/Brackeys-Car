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
    public GameObject autopilotLight;

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

    public bool carLocked = true;
    public bool notInEnding = true;

    public GameObject controls;

    void Start()
    {
        carBody = GetComponent<Rigidbody>();
        carBody.centerOfMass = centerMass.transform.localPosition;

        isAutopilot = true;
        StartCoroutine(WaitAndChange());
    }

    void FixedUpdate()
    {
        if (!carLocked)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxis("Vertical");
        }

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

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!controls.activeSelf)
                controls.SetActive(true);
            else
                controls.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.T) && movement.y == 0)
        {
            isAutopilot = !isAutopilot;
        }
        //Debug.Log(transform.rotation.y * 180);
        if (
            Input.GetButton("RotateCar")
            || transform.rotation.y * 180 > 130 && transform.rotation.y * 180 < 240 && notInEnding
            || transform.rotation.y * 180 < -130 && transform.rotation.y * 180 > -240 && notInEnding
        )
        {
            carBody.velocity = Vector3.zero;
            transform.position = new Vector3(-5, 8, transform.position.z);
            transform.rotation = Quaternion.identity;
        }

        if (isAutopilot && !autopilotLight.activeSelf)
        {
            autopilotLight.SetActive(true);
        }

        if (!isAutopilot && autopilotLight.activeSelf)
        {
            autopilotLight.SetActive(false);
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

    IEnumerator WaitAndChange()
    {
        yield return new WaitForSeconds(2);
        carLocked = !carLocked;
    }
}
