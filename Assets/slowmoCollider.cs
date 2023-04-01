using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowmoCollider : MonoBehaviour
{
    public AudioSource[] backgroundAudio;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = (Time.fixedDeltaTime / 5);
            for (int i = 0; i < backgroundAudio.Length; i++)
            {
                backgroundAudio[i].pitch = 0.5f;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = (Time.fixedDeltaTime * 5);
            for (int i = 0; i < backgroundAudio.Length; i++)
            {
                backgroundAudio[i].pitch = 1f;
            }
        }
    }
}
