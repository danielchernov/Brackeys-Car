using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHighway : MonoBehaviour
{
    GameObject highwayModuleOld;
    GameObject highwayModuleNew;

    public GameObject allModulesParent;
    public GameObject currentModulesParent;
    public List<GameObject> highwayModules = new List<GameObject>();
    public List<GameObject> currentModules = new List<GameObject>();

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            currentModules.Clear();

            for (int i = 0; i < currentModulesParent.transform.childCount; i++)
            {
                currentModules.Add(currentModulesParent.transform.GetChild(i).gameObject);
            }

            highwayModuleOld = currentModules[0];
            highwayModuleOld.SetActive(false);

            currentModules.RemoveAt(0);
            highwayModuleOld.transform.SetParent(allModulesParent.transform);

            highwayModules.Clear();

            for (int i = 0; i < allModulesParent.transform.childCount; i++)
            {
                highwayModules.Add(allModulesParent.transform.GetChild(i).gameObject);
            }

            highwayModuleNew = highwayModules[Random.Range(0, highwayModules.Count)];

            Vector3 newPos = new Vector3(
                currentModules[1].transform.position.x,
                currentModules[1].transform.position.y,
                currentModules[1].transform.position.z + 360
            );

            highwayModuleNew.SetActive(true);
            highwayModuleNew.transform.position = newPos;

            currentModules.Add(highwayModuleNew);
            highwayModuleNew.transform.SetParent(currentModulesParent.transform);
        }
    }
}
