using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class voiceManager : MonoBehaviour
{
    public Transform highlight;
    public RaycastHit raycastHit;
    public Camera carCamera;
    public Ray ray;

    public GameObject[] carLights;
    public AudioSource radioSource;
    public AudioClip[] radioClips;

    public AudioSource voiceSource;
    public AudioClip[] voices;

    public GameObject Subtitles;

    public int radioclipNumber = 0;

    public int wheelClipNumber = 0;

    TMP_Text subtitleText;

    void Update()
    {
        ray = carCamera.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.collider.transform;
            if (highlight.CompareTag("Highlightable"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (highlight.gameObject.name == "LightOutliner")
                    {
                        if (carLights[0].activeSelf)
                        {
                            carLights[0].SetActive(false);
                            carLights[1].SetActive(false);
                        }
                        else
                        {
                            carLights[0].SetActive(true);
                            carLights[1].SetActive(true);
                        }
                    }

                    if (highlight.gameObject.name == "RadioOutliner")
                    {
                        radioclipNumber++;
                        radioSource.clip = radioClips[radioclipNumber % radioClips.Length];
                        radioSource.Play();
                    }

                    if (
                        !voiceSource.isPlaying
                        && highlight.gameObject.name == "SteeringWheel"
                        && wheelClipNumber < 3
                    )
                    {
                        Subtitles.SetActive(true);
                        subtitleText = Subtitles.transform
                            .GetChild(0)
                            .gameObject.GetComponent<TMP_Text>();

                        if (wheelClipNumber == 0)
                        {
                            StartCoroutine(SpitSubtitles("blablabla", 1, "", 0.3f, "blublublu"));
                        }
                        if (wheelClipNumber == 1)
                        {
                            StartCoroutine(SpitSubtitles("dududududu", 1, "dididi", 0.5f, "MEH"));
                        }
                        if (wheelClipNumber == 2)
                        {
                            StartCoroutine(SpitSubtitles("BOBOBOBO", 0.5f, "MIAUUUU"));
                        }

                        voiceSource.clip = voices[wheelClipNumber];
                        voiceSource.Play();

                        wheelClipNumber++;

                        if (radioSource.isPlaying)
                            radioSource.volume = 0.2f;
                    }
                }
            }
            if (!voiceSource.isPlaying && Subtitles.activeSelf)
            {
                Subtitles.SetActive(false);

                if (radioSource.isPlaying && radioSource.volume == 0.2f)
                    radioSource.volume = 0.5f;
            }
        }
    }

    IEnumerator SpitSubtitles(
        string text1,
        float pause1 = 0,
        string text2 = null,
        float pause2 = 0,
        string text3 = null,
        float pause3 = 0,
        string text4 = null,
        float pause4 = 0,
        string text5 = null,
        float pause5 = 0,
        string text6 = null,
        float pause6 = 0
    )
    {
        subtitleText.text = text1;
        yield return new WaitForSeconds(pause1);
        if (text2 != null)
        {
            subtitleText.text = text2;
            yield return new WaitForSeconds(pause2);
        }
        if (text3 != null)
        {
            subtitleText.text = text3;
            yield return new WaitForSeconds(pause3);
        }
        if (text4 != null)
        {
            subtitleText.text = text4;
            yield return new WaitForSeconds(pause4);
        }
        if (text5 != null)
        {
            subtitleText.text = text5;
            yield return new WaitForSeconds(pause5);
        }
        if (text6 != null)
        {
            subtitleText.text = text6;
            yield return new WaitForSeconds(pause6);
        }
    }
}
