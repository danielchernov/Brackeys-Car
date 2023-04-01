using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderEnding : MonoBehaviour
{
    voiceManager voiceManager;
    RusitosCarController carController;
    public int endingNr = 0;

    void Start()
    {
        voiceManager = GameObject.Find("VoiceManager").GetComponent<voiceManager>();
        carController = GameObject.Find("MainCar").GetComponent<RusitosCarController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (endingNr == 0)
            {
                voiceManager.endingA = true;
                carController.notInEnding = false;
            }
            else if (endingNr == 1)
            {
                voiceManager.endingB = true;
                carController.notInEnding = false;
            }
        }
    }
}
