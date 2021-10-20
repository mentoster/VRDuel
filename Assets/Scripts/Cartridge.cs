using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float Nlifetime;

    private void FixedUpdate()
    {

        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
            lifetime = Nlifetime;
        }
    }
}
