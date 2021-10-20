using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public SightController controller;
    public void AimingEndedEvent(int p)
    {
        controller.AimingEnded(p);
    }
}
