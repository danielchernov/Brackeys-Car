using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signCheckpoint : MonoBehaviour
{
    voiceManager voiceManager;
    public int highwayNr = 0;

    void Start()
    {
        voiceManager = GameObject.Find("VoiceManager").GetComponent<voiceManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (highwayNr == 0)
                voiceManager.Checkpoint1 = true;
            else if (highwayNr == 1)
                voiceManager.Checkpoint2 = true;
            else if (highwayNr == 2)
                voiceManager.Checkpoint3 = true;
            else if (highwayNr == 3)
                voiceManager.Checkpoint4 = true;
            else if (highwayNr == 4)
                voiceManager.Checkpoint5 = true;
        }
    }
}
