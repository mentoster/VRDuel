using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    private Animator animator;
    public LayerMask layerMask;
    private bool look;

    public Transform[] movePoints;
    private int randomPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        waitTime = startWaitTime;
        randomPoint = Random.Range(0, movePoints.Length);
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, movePoints[randomPoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoints[randomPoint].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                animator.SetBool("Rotate", false);
                randomPoint = Random.Range(0, movePoints.Length);
                waitTime = startWaitTime;
            }
            else
            {
                animator.SetBool("Rotate", true);
                waitTime -= Time.deltaTime;
                transform.Rotate(0, 5, 0);
                //transform.LookAt(movePoints[randomPoint].transform.position);
            }
        }
    }
}
