using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseActivate : MonoBehaviour
{
    public AudioSource backgroundAudio;
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                backgroundAudio.Pause();
            }
            else
            {
                pauseMenu.SetActive(false);
                backgroundAudio.Play();
            }
        }
    }
}
