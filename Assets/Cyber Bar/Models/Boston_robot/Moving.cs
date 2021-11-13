using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private GameObject[] movePoints;
    [SerializeField] private bool decisionTimeBool;
    [SerializeField] private GameObject currentMovePoint;
    [SerializeField] private Quaternion rotation;

    [SerializeField] private LayerMask layerMask;
    private Animator animator;

    private GameObject previosMovePoint;

    [SerializeField] private bool look;

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    void Start()
    {
        ChooseMoveDirection();
        animator = GetComponent<Animator>();
    }

    public void ChooseMoveDirection() 
    {
        currentMovePoint = movePoints[Random.Range(0, movePoints.Length)];
        decisionTimeBool = false;
    }
    
    private void RayHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Хвала Небесам!!! Луч надежды снизошел на нас и соприкоснулся с Point!");
            look = true;
        }
        else
        {
            look = false;
        }
    }

    private void FixedUpdate()
    {
        RayHit();

        animator.SetBool("Rotate", true);
        Vector3 dir = currentMovePoint.transform.position - transform.position;
        rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);

        if (currentMovePoint != null)
        {
            if (look && currentMovePoint != previosMovePoint)
            {
                animator.SetBool("Rotate", false);
                transform.position = Vector3.MoveTowards(transform.position, currentMovePoint.transform.position, speed);
            }

        }

        if (decisionTimeBool)
        {
            ChooseMoveDirection();
        }

        if (Vector3.Distance(transform.position, currentMovePoint.transform.position) <= 0.5)
        {
            previosMovePoint = currentMovePoint;
            decisionTimeBool = true;
        }

    }
}
