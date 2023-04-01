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
        yield return new WaitForSeconds(Random.Range(2, 7));

        spawnPos = new Vector3(
            Random.Range(-23, -17),
            8,
            transform.position.z - Random.Range(-50, 0)
        );

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
