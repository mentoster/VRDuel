using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Bspeed;
    [SerializeField] private float Slowspeed;
    private float lifetime;
    [SerializeField] private float Nlifetime;
    [SerializeField] private int damage;
    [SerializeField] private ParticleSystem particle_System;

    public GameObject decal;
    Vector3 lastPos;

    private void Awake()
    {
        lifetime = Nlifetime;
    }

    private void Start()
    {
        lastPos = transform.position;
        particle_System.Play();
    }

    private void FixedUpdate()
    {

        transform.Translate(Vector3.forward * Bspeed * Time.deltaTime);
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
            lifetime = Nlifetime;
        }
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Linecast(lastPos, transform.position, out hit))
        {
            if (hit.collider.tag == "Decal")
            {
                GameObject d = Instantiate<GameObject>(decal);

                d.transform.position = hit.point + hit.normal * 0.001f;
                d.transform.rotation = Quaternion.LookRotation(-hit.normal);
                Destroy(d, 10);
                Destroy(gameObject);
            }
        }
        lastPos = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "Enemy":
                Debug.Log(other.name);
                other.GetComponentInParent<EnemyStat>().TakeDamage(damage, other.name);
                Destroy(this.gameObject);
                break;
            case "Player":
                other.GetComponent<PlayerHP>().TakeDamage(damage, other.name);
                Destroy(this.gameObject);
                break;
            case "Bottle":
                other.GetComponent<Bottle_Destroy>().TakeDamage();
                Destroy(this.gameObject);
                break;

        }
        if (other.CompareTag("SlowArea") && gameObject.CompareTag("EnemyBulet"))
        {
            Bspeed = Slowspeed;
            particle_System.Play();
        }
    }
}
