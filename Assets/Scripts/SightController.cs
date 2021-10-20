using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerController playerController;
    public GameObject cross;
    public GameObject circle;


    public void SetCircleCondition(int condition)
    {
        circle.GetComponent<Animator>().SetInteger("Condition", condition);
    }
    public void SetCrossTrigger()
    {
        cross.GetComponent<Animator>().SetTrigger("Shot");
    }

    public void AimingEnded(int p)
    {
        playerController.AimingEndedHandler();
    }
}
