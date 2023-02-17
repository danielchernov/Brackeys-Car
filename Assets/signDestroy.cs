using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signDestroy : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(transform.parent.gameObject, 10);
        }
    }
}
