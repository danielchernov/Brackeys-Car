using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCars : MonoBehaviour
{
    public GameObject[] cars;
    GameObject player;

    Vector3 spawnPos;

    float minDist = 400;
    float dist = 0;

    void Start()
    {
        player = GameObject.Find("MainCar");
        StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar()
    {
        yield return new WaitForSeconds(Random.Range(3, 8));

        int posNumber = Random.Range(0, 3);

        if (posNumber == 0)
        {
            spawnPos = new Vector3(-17, 8, transform.position.z);
        }
        else if (posNumber == 1)
        {
            spawnPos = new Vector3(-20, 8, transform.position.z);
        }
        else
        {
            spawnPos = new Vector3(-23, 8, transform.position.z);
        }

        GameObject spawnedCar = Instantiate(
            cars[Random.Range(0, cars.Length)],
            spawnPos,
            Quaternion.Euler(0, 180, 0)
        );

        dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > minDist)
        {
            StartCoroutine(SpawnCar());
        }
    }
}
