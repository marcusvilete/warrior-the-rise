using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawnTest : MonoBehaviour
{
    // Start is called before the first frame update

    public RotateAroundProjTest projectilePrefab;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        SpawnRing(3, 10, Vector3.left, 2);
        SpawnRing(6, 20, Vector3.right, 3);
        SpawnRing(9, 30, Vector3.left, 4);


        anim.Play("Vulnerable");


    }

    void SpawnRing(float distance, int amount, Vector3 orientation, int holes)
    {
        int holeCount = 1;
        float angleStep = 360 / (float)amount;
        Vector3 position = new Vector3(0, distance);

        int holeStep = amount / holes;

        for (int i = 1; i <= amount; i++)
        {
            if (holeCount <= holes)
            {
                if (i == holeStep * holeCount )
                {
                    holeCount++;
                    continue;
                }
            }

            //if (holes > holeCount && (i + 1 == amount / (holes - holeCount)))
            //{
            //    holeCount++;
            //    continue;
            //}


            var proj = Instantiate(projectilePrefab, transform);
            proj.transform.localPosition = position;
            proj.transform.RotateAround(transform.position, Vector3.forward, i * angleStep);

            if (orientation == Vector3.left)
            {
                proj.Initialize(transform.position, -(angleStep * 2));
            }
            else
            {
                proj.Initialize(transform.position, angleStep * 2);
            }
        }
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
