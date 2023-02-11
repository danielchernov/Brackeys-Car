using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public AudioSource backgroundAudio;

    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        backgroundAudio.Play();
    }
}
