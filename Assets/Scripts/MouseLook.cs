using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0;
    float yRotation = 0;

    bool mouseLocked = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(WaitAndChange());
    }

    void Update()
    {
        if (!mouseLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -60, 50);

            yRotation += mouseX;
            yRotation = Mathf.Clamp(yRotation, -90, 140);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }

    IEnumerator WaitAndChange()
    {
        yield return new WaitForSeconds(1);
        mouseLocked = !mouseLocked;
    }
}
