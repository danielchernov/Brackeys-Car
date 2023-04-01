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
    public AudioClip easterEggClip;

    public AudioSource voiceSource;
    public AudioClip[] voicesMain;
    public AudioClip[] voicesWheel;
    public AudioClip[] voicesBackpack;
    public AudioClip[] voicesLuggage;
    public AudioClip[] voicesBooks;
    public AudioClip[] voicesRadio;

    public AudioSource SFXSource;
    public AudioClip ButtonSFX1;
    public AudioClip ButtonSFX2;
    public AudioClip ItemSFX;

    public GameObject Subtitles;

    public GameObject[] highwaySigns;

    public int radioNumber = 0;
    public int easterEggCounter = 0;

    public int mainClipNumber = 0;
    public int wheelClipNumber = 0;
    public int backpackClipNumber = 0;
    public int luggageClipNumber = 0;
    public int booksClipNumber = 0;
    public int radioClipNumber = 0;

    TMP_Text subtitleText;

    public bool itemsAreBlocked = false;
    public bool mainIsBlocked = false;
    public bool neverPlayedEaster = true;

    public bool Checkpoint1 = false;
    public bool Checkpoint2 = false;
    public bool Checkpoint3 = false;
    public bool Checkpoint4 = false;
    public bool Checkpoint5 = false;

    public bool endingA = false;
    public bool endingB = false;

    public GameObject endingCurtain;

    void Start()
    {
        StartCoroutine(DoMainVoice(6, "I remember the freeway.", 0));
        itemsAreBlocked = true;
    }

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

                        SFXSource.PlayOneShot(ButtonSFX1, 1f);
                    }

                    if (
                        highlight.gameObject.name == "RadioOutliner"
                        && radioClipNumber >= 3
                        && !mainIsBlocked
                    )
                    {
                        if (!radioSource.isPlaying)
                        {
                            if (easterEggCounter >= 3)
                            {
                                if (neverPlayedEaster)
                                {
                                    StartCoroutine(DoEasterEggStuff());
                                    radioSource.clip = easterEggClip;
                                    radioSource.Play();
                                    neverPlayedEaster = false;
                                }
                                else
                                {
                                    radioSource.clip = easterEggClip;
                                    radioSource.Play();
                                }
                            }
                            else
                            {
                                radioSource.Play();
                            }
                        }
                        else if (radioSource.isPlaying)
                        {
                            if (easterEggCounter >= 3)
                            {
                                radioSource.Stop();
                            }
                            else
                            {
                                radioNumber++;
                                radioSource.clip = radioClips[radioNumber % radioClips.Length];
                                radioSource.Play();

                                if (radioNumber >= 3)
                                {
                                    radioSource.Stop();
                                    easterEggCounter++;
                                    radioNumber = 0;
                                }
                            }
                        }

                        SFXSource.PlayOneShot(ButtonSFX2, 1f);
                    }

                    // STEERING WHEEL
                    if (
                        !voiceSource.isPlaying
                        && !itemsAreBlocked
                        && highlight.gameObject.name == "SteeringWheel"
                        && wheelClipNumber < 3
                    )
                    {
                        SFXSource.PlayOneShot(ItemSFX, 1f);
                        Subtitles.SetActive(true);
                        subtitleText = Subtitles.transform
                            .GetChild(0)
                            .gameObject.GetComponent<TMP_Text>();

                        if (wheelClipNumber == 0)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "I had arrived in L.A. in times of car shortage.",
                                    3.5f,
                                    "I'd managed to buy this beat-up old Buick with more than 300k miles for a couple thousand bucks."
                                )
                            );
                        }
                        if (wheelClipNumber == 1)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "That car had taken me <i>everywhere.</i>",
                                    3,
                                    "I'd driven it from San Jacinto to Topanga, Covina to Santa Monica, the entire PCH,",
                                    6,
                                    "the steep roads of Angeles National Forest, the lonely desert ones of Joshua Tree."
                                )
                            );
                        }
                        if (wheelClipNumber == 2)
                        {
                            StartCoroutine(SpitSubtitles("Jesus, I fucking loved that car."));
                        }

                        voiceSource.clip = voicesWheel[wheelClipNumber];
                        voiceSource.Play();

                        wheelClipNumber++;

                        if (radioSource.isPlaying)
                            radioSource.volume = 0.15f;
                    }

                    // BACKPACK
                    if (
                        !voiceSource.isPlaying
                        && !itemsAreBlocked
                        && highlight.gameObject.name == "Backpack"
                        && backpackClipNumber < 3
                    )
                    {
                        SFXSource.PlayOneShot(ItemSFX, 1f);
                        Subtitles.SetActive(true);
                        subtitleText = Subtitles.transform
                            .GetChild(0)
                            .gameObject.GetComponent<TMP_Text>();

                        if (backpackClipNumber == 0)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "I carried the usual in my backpack: my laptop, my notebook, a hoodie, and headphones."
                                )
                            );
                        }
                        if (backpackClipNumber == 1)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "Usually, I was excited about packing up my backpack before a trip.",
                                    4,
                                    "This time, though, I had just thrown in the basics."
                                )
                            );
                        }
                        if (backpackClipNumber == 2)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "My notebook was filled with really bad drawings of some spots in town.",
                                    5,
                                    "Last one I'd made was of Tommy's Burgers in Beverly and Rampart.",
                                    4,
                                    "I don't know why I liked that place."
                                )
                            );
                        }

                        voiceSource.clip = voicesBackpack[backpackClipNumber];
                        voiceSource.Play();

                        backpackClipNumber++;

                        if (radioSource.isPlaying)
                            radioSource.volume = 0.15f;
                    }

                    // LUGGAGE
                    if (
                        !voiceSource.isPlaying
                            && !itemsAreBlocked
                            && highlight.gameObject.name == "Luggage1"
                            && luggageClipNumber < 3
                        || !voiceSource.isPlaying
                            && highlight.gameObject.name == "Luggage2"
                            && luggageClipNumber < 3
                    )
                    {
                        SFXSource.PlayOneShot(ItemSFX, 1f);
                        Subtitles.SetActive(true);
                        subtitleText = Subtitles.transform
                            .GetChild(0)
                            .gameObject.GetComponent<TMP_Text>();

                        if (luggageClipNumber == 0)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "I had three bags. I still had to pay for two of them at the airport."
                                )
                            );
                        }
                        if (luggageClipNumber == 1)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "I even had kitchen utensils and plates in them. Didn't want to leave anything behind."
                                )
                            );
                        }
                        if (luggageClipNumber == 2)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "Somewhere inside me lived the hope of rebuilding my place with the exact same things I had in L.A., so it would feel like I wasn't somewhere else."
                                )
                            );
                        }

                        voiceSource.clip = voicesLuggage[luggageClipNumber];
                        voiceSource.Play();

                        luggageClipNumber++;

                        if (radioSource.isPlaying)
                            radioSource.volume = 0.15f;
                    }

                    // BOOKS
                    if (
                        !voiceSource.isPlaying
                        && !itemsAreBlocked
                        && highlight.gameObject.name == "Books"
                        && booksClipNumber < 3
                    )
                    {
                        SFXSource.PlayOneShot(ItemSFX, 1f);
                        Subtitles.SetActive(true);
                        subtitleText = Subtitles.transform
                            .GetChild(0)
                            .gameObject.GetComponent<TMP_Text>();

                        if (booksClipNumber == 0)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "I had decided to take my books with me. They would help me remember my time there."
                                )
                            );
                        }
                        if (booksClipNumber == 1)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "<i>We Three Came West</i> came to mind. I'd bought it for a couple of bucks at the Velaslavasay Panorama. What a weird place."
                                )
                            );
                        }
                        if (booksClipNumber == 2)
                        {
                            StartCoroutine(
                                SpitSubtitles(
                                    "<i>The Long Goodbye</i> was also there. That was <i>not</i> the L.A. I knew."
                                )
                            );
                        }

                        voiceSource.clip = voicesBooks[booksClipNumber];
                        voiceSource.Play();

                        booksClipNumber++;

                        if (radioSource.isPlaying)
                            radioSource.volume = 0.15f;
                    }

                    // RADIO
                    if (
                        !voiceSource.isPlaying
                        && !itemsAreBlocked
                        && highlight.gameObject.name == "RadioOutliner"
                        && radioClipNumber < 3
                    )
                    {
                        StartCoroutine(DoRadioStuff());
                    }
                }

                IEnumerator DoRadioStuff()
                {
                    if (!radioSource.isPlaying)
                    {
                        if (easterEggCounter >= 2)
                        {
                            radioSource.clip = easterEggClip;
                            radioSource.Play();
                        }
                        else
                        {
                            radioSource.Play();
                        }
                    }
                    else if (radioSource.isPlaying)
                    {
                        if (easterEggCounter >= 2)
                        {
                            radioSource.Stop();
                        }
                        else
                        {
                            radioNumber++;
                            radioSource.clip = radioClips[radioNumber % radioClips.Length];
                            radioSource.Play();

                            if (radioNumber >= 3)
                            {
                                radioSource.Stop();
                                easterEggCounter++;
                                radioNumber = 0;
                            }
                        }
                    }

                    SFXSource.PlayOneShot(ButtonSFX2, 1f);

                    itemsAreBlocked = true;
                    mainIsBlocked = true;
                    yield return new WaitForSeconds(3);

                    Subtitles.SetActive(true);
                    subtitleText = Subtitles.transform
                        .GetChild(0)
                        .gameObject.GetComponent<TMP_Text>();

                    if (radioClipNumber == 0)
                    {
                        StartCoroutine(
                            SpitSubtitles("That song felt like it was made for driving.")
                        );
                    }
                    if (radioClipNumber == 1)
                    {
                        StartCoroutine(
                            SpitSubtitles(
                                "They play that one at the Dodger's Stadium after each game. That's how I heard it for the first time. "
                            )
                        );
                    }
                    if (radioClipNumber == 2)
                    {
                        StartCoroutine(
                            SpitSubtitles(
                                "How many times have I sung this song at the top of my lungs while driving down Fairfax…"
                            )
                        );
                    }

                    voiceSource.clip = voicesRadio[radioClipNumber];
                    voiceSource.Play();

                    radioClipNumber++;

                    if (radioSource.isPlaying)
                        radioSource.volume = 0.15f;

                    itemsAreBlocked = false;
                    mainIsBlocked = false;
                }
            }
        }
        if (!voiceSource.isPlaying && Subtitles.activeSelf)
        {
            Subtitles.SetActive(false);

            if (radioSource.isPlaying && radioSource.volume == 0.15f)
                radioSource.volume = 0.3f;
        }
    }

    IEnumerator DoEasterEggStuff()
    {
        itemsAreBlocked = true;
        mainIsBlocked = true;
        yield return new WaitForSeconds(3);

        Subtitles.SetActive(true);
        subtitleText = Subtitles.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        StartCoroutine(SpitSubtitles("Huh? What the hell was that?"));

        voiceSource.clip = voicesRadio[3];
        voiceSource.Play();

        if (radioSource.isPlaying)
            radioSource.volume = 0.15f;

        itemsAreBlocked = false;
        mainIsBlocked = false;
    }

    // MAIN VOICE
    IEnumerator DoMainVoice(float waitingTime, string text, int voiceClipNumber)
    {
        while (voiceSource.isPlaying || mainIsBlocked)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(waitingTime);

        while (voiceSource.isPlaying || mainIsBlocked)
        {
            yield return new WaitForSeconds(2f);
        }

        if (!voiceSource.isPlaying)
        {
            //Debug.Log(voiceClipNumber);

            if (endingB)
            {
                voiceSource.clip = voicesMain[voiceClipNumber + 4];
            }
            else
            {
                voiceSource.clip = voicesMain[voiceClipNumber];
            }

            voiceSource.Play();

            Subtitles.SetActive(true);
            subtitleText = Subtitles.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
            StartCoroutine(SpitSubtitles(text));

            voiceClipNumber++;

            if (radioSource.isPlaying)
                radioSource.volume = 0.15f;

            if (voiceClipNumber == 1)
            {
                StartCoroutine(
                    DoMainVoice(
                        3,
                        "The lights moving from the horizon to the rearview mirror.",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 2)
            {
                StartCoroutine(
                    DoMainVoice(
                        2,
                        "The cars approaching me, then leaving me behind.",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 3)
            {
                StartCoroutine(
                    DoMainVoice(
                        1,
                        "I was on my way to LAX from Ethan's house in Burbank.",
                        voiceClipNumber
                    )
                );

                Vector3 highwaySignPos = new Vector3(
                    -6,
                    6,
                    carLights[0].transform.position.z + 1000
                );
                Quaternion signRotation = Quaternion.Euler(-90, 0, 0);
                GameObject sign = Instantiate(highwaySigns[0], highwaySignPos, signRotation);
            }
            if (voiceClipNumber == 4)
            {
                StartCoroutine(
                    DoMainVoice(
                        0.5f,
                        "He'd been kind enough to let me crash on his couch for the last couple of nights, after I'd left my apartment.",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 5)
            {
                itemsAreBlocked = false;

                while (!Checkpoint1)
                {
                    yield return new WaitForSeconds(2f);
                }

                StartCoroutine(
                    DoMainVoice(
                        2,
                        "How long had I lived in Los Angeles? Was I really leaving?",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 6)
            {
                StartCoroutine(
                    DoMainVoice(
                        1,
                        "I was supposed to deliver my car to the buyer at the airport, then take my flight to Austin, and that was it.",
                        voiceClipNumber
                    )
                );
                itemsAreBlocked = true;
            }

            if (voiceClipNumber == 7)
            {
                StartCoroutine(
                    DoMainVoice(
                        0.5f,
                        "There would be someone else sitting on that leather seat, their hands on the wheel, driving down the freeway.",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 8)
            {
                StartCoroutine(
                    DoMainVoice(
                        6,
                        "Against all hopes –every single one of them based on denial–, I had finally run out of money. I just had to run back home.",
                        voiceClipNumber
                    )
                );

                itemsAreBlocked = false;

                Vector3 highwaySignPos = new Vector3(
                    -6,
                    6,
                    carLights[0].transform.position.z + 1000
                );
                Quaternion signRotation = Quaternion.Euler(-90, 0, 0);
                GameObject sign = Instantiate(highwaySigns[1], highwaySignPos, signRotation);
            }
            if (voiceClipNumber == 9)
            {
                while (!Checkpoint2)
                {
                    yield return new WaitForSeconds(2f);
                }
                StartCoroutine(
                    DoMainVoice(
                        4,
                        "I couldn't complain, though. I had a job waiting for me in Austin.",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 10)
            {
                StartCoroutine(
                    DoMainVoice(
                        1,
                        "A proper job, not some bullshit shift at an ugly ass Santa Monica bar for $12 an hour, 10 hours a day.",
                        voiceClipNumber
                    )
                );
                itemsAreBlocked = true;
            }
            if (voiceClipNumber == 11)
            {
                StartCoroutine(
                    DoMainVoice(
                        0.5f,
                        "I would also have a real house, and that was pretty much all I needed. A job and a place.",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 12)
            {
                StartCoroutine(DoMainVoice(1.5f, "Wasn't it?", voiceClipNumber));
            }
            if (voiceClipNumber == 13)
            {
                StartCoroutine(
                    DoMainVoice(
                        6,
                        "Who needed Hollywood dreams? I'd take the realistic approach instead.",
                        voiceClipNumber
                    )
                );
                itemsAreBlocked = false;
            }
            if (voiceClipNumber == 14)
            {
                StartCoroutine(
                    DoMainVoice(
                        0.5f,
                        "I'd come back to L.A. after regrouping, with a solid project.",
                        voiceClipNumber
                    )
                );
                itemsAreBlocked = true;
            }
            if (voiceClipNumber == 15)
            {
                StartCoroutine(
                    DoMainVoice(
                        1.5f,
                        "Yeah. I'd come back. I'd make a return. A new beginning.",
                        voiceClipNumber
                    )
                );

                Vector3 highwaySignPos = new Vector3(
                    -6,
                    6,
                    carLights[0].transform.position.z + 1000
                );
                Quaternion signRotation = Quaternion.Euler(-90, 0, 0);
                GameObject sign = Instantiate(highwaySigns[2], highwaySignPos, signRotation);
            }
            if (voiceClipNumber == 16)
            {
                itemsAreBlocked = false;

                while (!Checkpoint3)
                {
                    yield return new WaitForSeconds(2f);
                }

                StartCoroutine(
                    DoMainVoice(
                        5,
                        "Suddenly the thought of leaving my car in the hands of a stranger, minutes from then, shook me.",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 17)
            {
                itemsAreBlocked = true;
                StartCoroutine(
                    DoMainVoice(
                        2,
                        "But I knew it was the right thing to do. I had made up my mind. I was leaving town.",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 18)
            {
                itemsAreBlocked = false;
                while (!Checkpoint4)
                {
                    yield return new WaitForSeconds(2f);
                }
                StartCoroutine(
                    DoMainVoice(
                        1,
                        "As I saw the sign, an invisible force yanked my hands to the right. What the hell was going on with me?",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 19)
            {
                itemsAreBlocked = true;
                StartCoroutine(
                    DoMainVoice(
                        1,
                        "What about the job? Austin? The money? For Christ's sake, I was broke!",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 20)
            {
                StartCoroutine(
                    DoMainVoice(1, "Could I, maybe... stay in Los Angeles?", voiceClipNumber)
                );
            }
            if (voiceClipNumber == 21)
            {
                StartCoroutine(
                    DoMainVoice(
                        1,
                        "Even the thought of it made my heart jump. Could I really back out?",
                        voiceClipNumber
                    )
                );
            }
            if (voiceClipNumber == 22)
            {
                while (!Checkpoint5)
                {
                    yield return new WaitForSeconds(1f);
                }
                if (endingA)
                {
                    StartCoroutine(
                        DoMainVoice(1, "At the last moment, reason prevailed.", voiceClipNumber)
                    );
                }
                else if (endingB)
                {
                    StartCoroutine(
                        DoMainVoice(
                            1,
                            "As soon as I took the exit, I felt a sudden heat going up my chest. I tightened my grip around the wheel.",
                            voiceClipNumber
                        )
                    );
                }
            }
            if (voiceClipNumber == 23)
            {
                if (endingA)
                {
                    StartCoroutine(
                        DoMainVoice(
                            1,
                            "I wouldn't sacrifice my life for some stupid dream. Before I'd know it, I'd be living in a tent under the freeway.",
                            voiceClipNumber
                        )
                    );
                }
                else if (endingB)
                {
                    StartCoroutine(
                        DoMainVoice(
                            1,
                            "Fuck home. Fuck that job. Fuck having money. I didn't want a new beginning.",
                            voiceClipNumber
                        )
                    );
                }
            }
            if (voiceClipNumber == 24)
            {
                if (endingA)
                {
                    StartCoroutine(
                        DoMainVoice(
                            1,
                            "I took a deep breath and caressed the wheel.",
                            voiceClipNumber
                        )
                    );
                }
                else if (endingB)
                {
                    StartCoroutine(
                        DoMainVoice(
                            1,
                            "<i>Who</i> needed Hollywood dreams? I did.",
                            voiceClipNumber
                        )
                    );
                }
            }
            if (voiceClipNumber == 25)
            {
                if (endingA)
                {
                    StartCoroutine(DoMainVoice(1, "Time to grow up.", voiceClipNumber));
                }
                else if (endingB)
                {
                    StartCoroutine(startEnding());
                }
            }
            if (voiceClipNumber == 26)
            {
                if (endingA)
                {
                    StartCoroutine(startEnding());
                }
            }
        }
    }

    IEnumerator startEnding()
    {
        yield return new WaitForSeconds(2);
        endingCurtain.SetActive(true);
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
