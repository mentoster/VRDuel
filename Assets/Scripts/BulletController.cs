using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject bulletOn;
    public GameObject bulletOff;

    public void BulletOn()
    {
        bulletOn.GetComponent<SpriteRenderer>().enabled = true;
        bulletOff.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void BulletOff()
    {
        bulletOff.GetComponent<SpriteRenderer>().enabled = true;
        bulletOn.GetComponent<SpriteRenderer>().enabled = false;
    }
}
