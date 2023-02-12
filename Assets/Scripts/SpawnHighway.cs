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

            highwayModuleNew.SetActive(true);

            float zSum =
                highwayModuleNew.transform
                    .GetChild(0)
                    .GetChild(0)
                    .gameObject.GetComponent<Collider>()
                    .bounds.extents.z
                + currentModules[1].transform
                    .GetChild(0)
                    .GetChild(0)
                    .gameObject.GetComponent<Collider>()
                    .bounds.extents.z;

            Debug.Log(zSum);

            Vector3 newPos = new Vector3(
                currentModules[1].transform.position.x,
                currentModules[1].transform.position.y,
                currentModules[1].transform.position.z + zSum
            );

            highwayModuleNew.transform.position = newPos;

            currentModules.Add(highwayModuleNew);
            highwayModuleNew.transform.SetParent(currentModulesParent.transform);
        }
    }
}
