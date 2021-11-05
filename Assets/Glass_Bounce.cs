using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass_Bounce : MonoBehaviour
{

    public void TakeDamage()
    {
        gameObject.transform.rotation = new Quaternion(Random.Range(1f, 30f), Random.Range(1f, 30f), Random.Range(1f, 30f), Random.Range(1f, 30f));
        gameObject.GetComponent<Rigidbody>().AddForce(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 1f, ForceMode.Impulse);
    }
}
