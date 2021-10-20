using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shooting : MonoBehaviour
{
    [SerializeField] private Bot_GunFire Bot_Gun;
    private EnemyStat EnemyStat;
    private GameObject player;
    [SerializeField] private GameObject Spine;

    [Header("Скорострельность")]
    public float ShootSpeed;
    public float ShootTimer;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyStat = GetComponent<EnemyStat>();
        MenuScript.ready = false;
    }

    

    void Update()
    {
        if (MenuScript.ready == true && EnemyStat.health > 0)
        {
            //Spine.transform.LookAt(player.transform.position);

            if (ShootTimer <= 0)
            {
                Bot_Gun.barrelPivot.LookAt(player.transform.position);

                if (EnemyStat.health > 0 && player.GetComponent<PlayerHP>().playerhealth > 0)
                    Bot_Gun.Shoot();
            }
            else
            {
                ShootTimer -= Time.deltaTime;
            }
        }
        else
        {
            ShootTimer = ShootSpeed;
        }
    }
    //void shoot()
    //{
    //    if (ShootTimer <= 0)
    //    {
    //        Transform BulletInstance = (Transform)Instantiate(bullet, GameObject.Find("ESpawn").transform.position, Quaternion.identity);
    //        BulletInstance.GetComponent().AddForce(transform.forward * 5000);
    //        GetComponent().PlayOneShot(Fire);
    //        ShootTimer = ShootSpeed;
    //    }
    //}
}
