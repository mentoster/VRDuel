using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] Guns;
    public GameObject bowSpawn;

    private void Awake()
    {
        if(GameControl.currentWeapon == 4)
            Instantiate(Guns[GameControl.currentWeapon], bowSpawn.transform.position, bowSpawn.transform.rotation);
        else
        Instantiate(Guns[GameControl.currentWeapon], transform.position, Quaternion.identity);
        //Instantiate(Guns[0], transform.position, Quaternion.identity);
        Debug.Log("Выбрано оружие" + GameControl.currentWeapon);
    }
}
