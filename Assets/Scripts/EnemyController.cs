using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{
    public int HP = 100;
    public bool dead = false;

    public Transform targetToRotate;
    public Transform targetToLook;
    private Animator anim;

    public bool IsDead
    {
        get
        {
            return dead;
        }
        set
        {
            dead = value;
        }
    }
    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;





    void Start()
    {
        agent.updateRotation = false;
        anim = GetComponent<Animator>();
        targetToLook = cam.transform;
    }

    // Update is called once per frame
    void Update()
    {

     

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(Rotata());
            character.Move(Vector3.zero, false, false);
        }
    }

    public void HitHandler()
    {
        HP -= 25;
        if(HP <= 0)
        {
            anim.SetTrigger("Kill");
            dead = true;
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }


    private IEnumerator Rotata()
    {
        const float threshold = 5f;
        var angle = 180f;

        //определить направление


        while (angle > threshold)
        {

        }

        yield break;
    }


    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtPosition(targetToLook.position);
        anim.SetLookAtWeight(1);
    }





}
