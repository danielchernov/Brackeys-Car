using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseActivate : MonoBehaviour
{
    public AudioSource[] backgroundAudio;
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                for (int i = 0; i < backgroundAudio.Length; i++)
                    backgroundAudio[i].Pause();
            }
            else
            {
                pauseMenu.SetActive(false);
                for (int i = 0; i < backgroundAudio.Length; i++)
                    backgroundAudio[i].UnPause();
            }
        }
    }
}
