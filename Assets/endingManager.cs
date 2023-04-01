using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingManager : MonoBehaviour
{
    public GameObject credits;
    public GameObject thanks;
    RusitosCarController carController;

    void Start()
    {
        carController = GameObject.Find("MainCar").GetComponent<RusitosCarController>();
    }

    public void TurnCredits()
    {
        credits.SetActive(true);
        carController.carLocked = true;
    }

    public void TurnThanks()
    {
        thanks.SetActive(true);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
