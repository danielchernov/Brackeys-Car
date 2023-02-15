using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public AudioSource[] backgroundAudio;
    public GameObject mouseCursor;

    void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        mouseCursor.SetActive(false);
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        if (mouseCursor != null)
            mouseCursor.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < backgroundAudio.Length; i++)
            backgroundAudio[i].UnPause();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
