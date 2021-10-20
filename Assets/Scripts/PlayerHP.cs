using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem.Sample;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;

public class PlayerHP : MonoBehaviour
{
    public SteamVR_LaserPointer Laser;
    [SerializeField] public float playerhealth;
    [SerializeField] private GameObject Damage;
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject WinMenu;
    private float Timer;
    [SerializeField] private float StartTimer;
    [SerializeField] private GameObject Weapon;
    public SkeletonUIOptions skeletonUI;
    public RenderModelChangerUI Gloves;
    public GameObject Music;
    public GameObject DeadSound;
    public GameObject WinSound;

    public GameObject Hurt1;
    public GameObject Hurt2;

    public GameObject[] playerHP;

    public void Awake()
    {
        Weapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    public void Update()
    {
        if(playerhealth == 100)
        {
            playerHP[2].SetActive(false);
            Hurt1.SetActive(true);
        }
        if(playerhealth == 50)
        {
            playerHP[1].SetActive(false);
            Hurt2.SetActive(true);
        }
        if(playerhealth <= 0)
        {
            playerHP[0].SetActive(false);
            Hurt1.SetActive(true);
        }
    }

    public void TakeDamage(int damage, string HitArea)
    {
        Debug.Log(damage);

        playerhealth -= damage;

        Timer = StartTimer;

        if (playerhealth <= 0)
        {
            Laser.thickness = 0.001f;
            PlayerDead();
        }
    }

    public void PlayerDead()
    {
        Music.SetActive(false);
        DeadSound.SetActive(true);
        Damage.SetActive(true);
        skeletonUI.AnimateHandWithController();
        MenuScript.ready = false;
    }
    public void PlayerWin()
    {
        Music.SetActive(false);
        WinSound.SetActive(true);
        WinMenu.SetActive(true);
        skeletonUI.AnimateHandWithController();
        MenuScript.ready = false;
    }

}
