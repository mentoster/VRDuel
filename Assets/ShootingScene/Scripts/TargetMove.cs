using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    public float speed = 2;

    void Update()
    {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z > 2.5f)
            speed = -speed;

        if(transform.position.z < -4f)
                speed = -speed;
    }
}
