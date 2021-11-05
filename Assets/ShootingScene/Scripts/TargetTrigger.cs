using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    public float speed = 1;
    public float time = 30;
    public GameObject scoreCanvas;

    public void TakeDamage()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        scoreCanvas.GetComponent<MenuScript>().Score(75);
    }

    public void Update()
    {
        if (transform.rotation.x < 0)
        {
            time -= Time.deltaTime;

            if (time < 0)
                transform.Rotate(Vector3.right * Time.deltaTime * speed);
        }
        else
            time = 30;

    }
}
