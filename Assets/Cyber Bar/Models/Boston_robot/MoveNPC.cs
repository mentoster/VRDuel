using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] movePoints;
    private int randomPoint;

    void Start()
    {
        waitTime = startWaitTime;
        randomPoint = Random.Range(0, movePoints.Length);
    }

    void Update()
    {
        transform.LookAt(movePoints[randomPoint].transform.position);
        transform.position = Vector3.MoveTowards(transform.position, movePoints[randomPoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoints[randomPoint].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomPoint = Random.Range(0, movePoints.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
