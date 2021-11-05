using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isArrow = false;
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
        if(isArrow == false)
        lifetime = Nlifetime;
    }

    private void Start()
    {
        if (isArrow == false)
        {
            lastPos = transform.position;
            particle_System.Play();
        }

    }

    private void FixedUpdate()
    {
        if (isArrow == false)
        {
            transform.Translate(Vector3.forward * Bspeed * Time.deltaTime);
            lifetime -= Time.deltaTime;

            if (lifetime <= 0)
            {
                Destroy(this.gameObject);
                lifetime = Nlifetime;
            }
        }
    }

    void Update()
    {
        if (isArrow == false)
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

    }

    void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "Enemy":
                other.GetComponentInParent<EnemyStat>().TakeDamage(damage, other.name);
                Destroy(this.gameObject);
                break;
            case "Player":
                other.GetComponent<PlayerHP>().TakeDamage(damage, other.name);
                Destroy(this.gameObject);
                break;
            case "Bottle":
                other.GetComponent<Explosion>().TakeDamage();
                Destroy(this.gameObject);
                break;
            case "Bounce":
                other.GetComponentInParent<Glass_Bounce>().TakeDamage();
                Destroy(this.gameObject);
                break;
            case "Dog":
                other.GetComponent<Explosion>().TakeDamage();
                Destroy(this.gameObject);
                break;
            case "TargetTrigger":
                other.GetComponent<TargetTrigger>().TakeDamage();
                Destroy(this.gameObject);
                break;
            case "CircleTarget":
                other.GetComponent<CircleTarget>().TakeDamage(gameObject.transform.position);
                Destroy(this.gameObject);
                break;


        }
        if (other.CompareTag("SlowArea") && gameObject.CompareTag("EnemyBulet") && isArrow == false)
        {
            Bspeed = Slowspeed;
            particle_System.Play();
        }
    }
}
