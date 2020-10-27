using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler : MonoBehaviour
{

    public GameObject spawnerPrefab;
    public GameObject[] spawners;
    public GameObject projectilePrefab;
    float timeToNextFire = 2f;
    public float fireRate = 1f;


    void Awake()
    {
        //fix scale:
        float screenRatio = (float)Screen.safeArea.width / Screen.safeArea.height;
        transform.localScale = transform.localScale * AspectHelper.GetScaleFactor(screenRatio);





        //Placing spawnPoints
        Vector2 lowerBound = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 upperBound = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        float units = (upperBound.x - lowerBound.x) / 8;
        lowerBound.x += units * 0.06f;

        spawners = new GameObject[8];
        for (int i = 0; i < 8; i++)
        {
            spawners[i] = Instantiate(spawnerPrefab, transform);
            spawners[i].transform.position = new Vector3(lowerBound.x + (i * units) + (spawners[i].transform.lossyScale.x / 2), -10, 0);
        }
    }

    void Update()
    {
        HandleSpawning();
    }

    public void HandleSpawning()
    {
        if (timeToNextFire <= 0.0f)
        {   
            timeToNextFire = 1 / fireRate;

            


            if (Random.Range(0, 10) > 3)
            {
                var proj = Instantiate(projectilePrefab, transform);
                proj.transform.position = spawners[Random.Range(0, 7)].transform.position;
            }
            else
            {
                int dontSpawn = Random.Range(0, 7);

                for (int i = 0; i < 8; i++)
                {
                    if (i == dontSpawn) continue;

                    var proj = Instantiate(projectilePrefab, transform);
                    proj.transform.position = spawners[i].transform.position;
                }
            }

        }
        timeToNextFire -= Time.deltaTime;
    }
}
