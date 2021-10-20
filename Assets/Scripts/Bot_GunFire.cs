using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_GunFire : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public Transform barrelPivot;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private AI_Shooting AI_Shooting;

    [SerializeField] private AudioSource ShootSound;

    public void Shoot()
    {
        ShootSound.Play();
        Instantiate(muzzleFlash, barrelPivot.position, barrelPivot.rotation);
        Instantiate(bullet, barrelPivot.position, barrelPivot.rotation);

        AI_Shooting.ShootTimer = AI_Shooting.ShootSpeed;
    }
}
