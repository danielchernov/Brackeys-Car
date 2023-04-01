using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;
    public GameObject tutorial5;
    public GameObject tutorial6;

    void Start()
    {
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(9);
        tutorial1.SetActive(true);
        yield return new WaitForSeconds(3);
        tutorial1.GetComponent<Animator>().SetBool("fadeOut", true);
        yield return new WaitForSeconds(9);
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);
        yield return new WaitForSeconds(3);
        tutorial3.SetActive(true);
        yield return new WaitForSeconds(3);
        tutorial2.GetComponent<Animator>().SetBool("fadeOut", true);
        yield return new WaitForSeconds(2);
        tutorial3.GetComponent<Animator>().SetBool("fadeOut", true);
        yield return new WaitForSeconds(6);
        tutorial2.SetActive(false);
        tutorial3.SetActive(false);
        tutorial4.SetActive(true);
        yield return new WaitForSeconds(2);
        tutorial5.SetActive(true);
        yield return new WaitForSeconds(2);
        tutorial6.SetActive(true);
        tutorial4.GetComponent<Animator>().SetBool("fadeOut", true);
        yield return new WaitForSeconds(1);
        tutorial5.GetComponent<Animator>().SetBool("fadeOut", true);
        yield return new WaitForSeconds(1);
        tutorial6.GetComponent<Animator>().SetBool("fadeOut", true);
        yield return new WaitForSeconds(4);
        tutorial4.SetActive(false);
        tutorial5.SetActive(false);
        tutorial6.SetActive(false);
    }
}
