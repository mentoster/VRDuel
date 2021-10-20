using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rawLookSpeed = 1;
    public float rawLookSpeedDecreaseWhenAiming = 2;

    public float sightFollowSpeed;
    public float bulletsFollowSpeed;

    public float cameraFollowSpeed;
    public float standardFOV = 90;
    public float aimingFOV = 55;
    public float ChangeFOVSpeedFactor = 10;
    
    public float fireDelay = 0.5f;

    public int bulletMax = 10;
    public int bulletCount = 10;

    public AudioManager audio;
    public SightController sightController;
    public GameObject sight;
    public GameObject sightParent;
    public GameObject bulletsParent;
    public GameObject look;
    public GameObject maincamera;
    public BulletIndicatorController bulletsController;

    


    public GameObject currentTarget;
    bool readyToFire = true;

    bool aiming = false;

    float timer = 0;
    bool timerWorking = false;
    float timerEnd = 0;

    void Start()
    {
        
    }




    void Update()
    {
        TimerHandler();
        SightFollowingHandler();
        BulletsFollowingHandler();
        LookControlHandler();
        CameraFollowingHandler();
        FOVChangeHandler();

        TargetingHandler();






    }

    void LookControlHandler()
    {
        GameObject controlledObject = look;
        float controlSpeed = rawLookSpeed;
        if(aiming)
        {
            controlSpeed /= rawLookSpeedDecreaseWhenAiming;
        }

        if (Input.GetKey(KeyCode.W))
        {
            controlledObject.transform.rotation = Quaternion.Euler(controlledObject.transform.rotation.eulerAngles.x - controlSpeed, controlledObject.transform.rotation.eulerAngles.y, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            controlledObject.transform.rotation = Quaternion.Euler(controlledObject.transform.rotation.eulerAngles.x, controlledObject.transform.rotation.eulerAngles.y - controlSpeed, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            controlledObject.transform.rotation = Quaternion.Euler(controlledObject.transform.rotation.eulerAngles.x + controlSpeed, controlledObject.transform.rotation.eulerAngles.y, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            controlledObject.transform.rotation = Quaternion.Euler(controlledObject.transform.rotation.eulerAngles.x, controlledObject.transform.rotation.eulerAngles.y + controlSpeed, 0);
        }
    }

    void SightFollowingHandler()
    {
        Quaternion startpoint = sightParent.transform.rotation;
        Quaternion endpoint = look.transform.rotation;
        sightParent.transform.rotation = Quaternion.Slerp(startpoint, endpoint, sightFollowSpeed);

    }

    void BulletsFollowingHandler()
    {
        Quaternion startpoint = bulletsParent.transform.rotation;
        Quaternion endpoint = look.transform.rotation;
        bulletsParent.transform.rotation = Quaternion.Slerp(startpoint, endpoint, bulletsFollowSpeed);

    }

    void CameraFollowingHandler()
    {
        Quaternion startpoint = maincamera.transform.rotation;
        Quaternion endpoint = sightParent.transform.rotation;
        maincamera.transform.rotation = Quaternion.Slerp(startpoint,endpoint,cameraFollowSpeed);

    }

    void TargetingHandler()
    {
        //int layerMask = 1 << 9;
        RaycastHit hit;
        if (Physics.Raycast(maincamera.transform.position, sight.transform.position - maincamera.transform.position, out hit, Mathf.Infinity))
        {
            if (readyToFire && hit.collider.gameObject.layer == 9) { 
                sightController.SetCircleCondition(1);
                currentTarget = hit.collider.gameObject;
            } else
            {
                sightController.SetCircleCondition(0);
            }
            
        } else
        {
            sightController.SetCircleCondition(0);
        }
    }

    public void AimingEndedHandler()
    {
        sightController.SetCrossTrigger();
        readyToFire = false;
        sightController.SetCircleCondition(0);
        bulletCount--;
        bulletsController.UpdateBullets(bulletCount);
        audio.PlayShotSound();
        StartTimer(fireDelay);
        currentTarget.GetComponent<EnemyHitboxController>().Hit();
    }


    void StartTimer(float timeToGo)
    {
        timerEnd = timeToGo;
        timerWorking = true;

    }

    void TimerHandler()
    {
        timer += Time.deltaTime;
        if(timer >= timerEnd)
        {
            timerWorking = false;
            timer = 0;
            TimerCallback();
        }
    }
    void TimerCallback()
    {
        readyToFire = true;
    }

    void FOVChangeHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            aiming = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            aiming = false;
        }

        if (aiming)
        {
            maincamera.GetComponent<Camera>().fieldOfView += (aimingFOV - maincamera.GetComponent<Camera>().fieldOfView) / ChangeFOVSpeedFactor;
        }
        else
        {
            maincamera.GetComponent<Camera>().fieldOfView += (standardFOV - maincamera.GetComponent<Camera>().fieldOfView) / ChangeFOVSpeedFactor;
        }
    }
}



