using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleLifeTime : MonoBehaviour
{
    public float lifetime;
    public float Nlifetime;

    private void Update()
    {
        //transform.Translate(Vector3.right * Bspeed * Time.deltaTime);

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
            lifetime = Nlifetime;
        }
    }
}
