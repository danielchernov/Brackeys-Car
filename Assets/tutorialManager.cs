using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;

    void Start()
    {
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(3);
        tutorial1.SetActive(true);
        yield return new WaitForSeconds(4);
        tutorial1.SetActive(false);
        yield return new WaitForSeconds(8);
        tutorial2.SetActive(true);
        yield return new WaitForSeconds(4);
        tutorial2.SetActive(false);
        yield return new WaitForSeconds(8);
        tutorial3.SetActive(true);
        yield return new WaitForSeconds(4);
        tutorial3.SetActive(false);
        yield return new WaitForSeconds(4);
        tutorial4.SetActive(true);
        yield return new WaitForSeconds(4);
        tutorial4.SetActive(false);
    }
}
