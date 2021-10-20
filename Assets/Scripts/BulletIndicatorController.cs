using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletIndicatorController : MonoBehaviour
{
    public GameObject bullet;
    public Camera cam;

    public int bulletCount;
    public int currentBullets;

    public GameObject[] bullets;

    public float travelFactor = 0.02f;
    public float spacingFactor = 0.03f;

    public Vector3 pos = new Vector3(-16,-9,0);
    public Vector3 pos2 = new Vector3(16, 9, 0);

    void Start()
    {

        bullets = new GameObject[bulletCount];

        for(int i = 0; i < bulletCount; i++)
        {
            bullets[i] = Instantiate(bullet, transform);
            bullets[i].transform.position += new Vector3(i * spacingFactor, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = pos + pos2 * (cam.fieldOfView * travelFactor);
    }

    public void UpdateBullets(int newBullets)
    {
        currentBullets = newBullets;
        for (int i = 0; i < bulletCount; i++)
        {
            if (newBullets - 1 >= i)
            {
                bullets[i].GetComponent<BulletController>().BulletOn();
            }
            else
            {
                bullets[i].GetComponent<BulletController>().BulletOff();
            }
        }
    }
}
