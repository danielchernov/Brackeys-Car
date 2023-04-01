using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accelCollider : MonoBehaviour
{
    public int accelNr = 700;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<RusitosCarController>().accel = accelNr;
        }
    }
}
