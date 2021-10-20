using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class Gun_Fire : MonoBehaviour
{
    private PlayerHP PlayerHP;
    public SteamVR_Action_Boolean fireAction;
    public GameObject bullet;
    public Transform barrelPivot;
    public float shottingSpeed;
    public GameObject muzzleFlash;
    public float fireRate = 1f;
    private float nextFire = 0f;
    public int clip = 7;
    private int bulletCount;

    public GameObject cartridge;
    public Transform cartridgePivot;

    public GameObject CountBullet;

    private Interactable interactable;

    public AudioSource ShootSound;
    public AudioSource ZeroBullet;
    public AudioSource ReloadBullet;

    public GameObject[] clips;
    private int clipCount;

    private GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        bulletCount = clip;
        clipCount = clips.Length;
        Debug.Log("Кол-во обойм " + clipCount);
    }

    void Awake()
    {
        PlayerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
        spawner = GameObject.FindGameObjectWithTag("SpawnWeapon");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.attachedToHand != null)
        {
            CountBullet.GetComponent<Text>().text = bulletCount.ToString();
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if (fireAction[source].stateDown && Time.time > nextFire && bulletCount != 0 && MenuScript.ready == true)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
            }

            else if (fireAction[source].stateDown && Time.time > nextFire && (bulletCount == 0 || MenuScript.ready == false) && clipCount == 0)
            {
                ZeroBullet.Play();
            }

            else if (bulletCount == 0 && MenuScript.ready == true && clipCount == 0)
            {
                PlayerHP.PlayerDead();
            }

            /*Only for Non VR Developer*/
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire && bulletCount != 0 && MenuScript.ready == true)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire && (bulletCount == 0 || MenuScript.ready == false))
            {
                ZeroBullet.Play();
            }
            /*Only for Non VR Developer*/

            if (Vector3.Angle(transform.up, Vector3.up) > 100 && bulletCount == 0)
            {
                if (clipCount > 0)
                {
                    Reload(clipCount);
                }
                else
                {
                    Debug.Log("Не хватает обойм");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Plane")
        {
            gameObject.transform.position = spawner.transform.position;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }    
    }

    void Reload(int curentClip)
    {
        bulletCount = clip;
        ReloadBullet.Play();
        clips[curentClip-1].SetActive(false);
        clipCount -= 1;
    }

    void Shoot()
    {
        ShootSound.Play();
        Instantiate(muzzleFlash, barrelPivot.position, barrelPivot.rotation);
        Instantiate(bullet, barrelPivot.position, barrelPivot.rotation);

        bulletCount--;

        Rigidbody cartridgeRB = Instantiate(cartridge, cartridgePivot.position, cartridgePivot.rotation).GetComponent<Rigidbody>();
        float x = (0.5f + Random.Range(-0.1f, 0.1f));
        float y = 0.5f;
        Vector3 dir = (transform.up * y + transform.right * x + transform.forward * -y) * 20;
        cartridgeRB.AddForce(dir, ForceMode.Impulse);
    }
}
