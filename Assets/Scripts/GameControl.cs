using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject RightButtonW;
    [SerializeField] private GameObject LeftButtonW;
    [SerializeField] private GameObject RightButtonL;
    [SerializeField] private GameObject LeftButtonL;

    public static int currentWeapon = 0;
    public static int currentLocation = 0;

    [SerializeField] private GameObject[] locations;
    [SerializeField] private GameObject[] weapons;

    [SerializeField] private Text Weapon;
    [SerializeField] private Text Location;

    private void Update()
    {
        if (RightButtonW.activeSelf||LeftButtonW.activeSelf)
        {
            weapons[currentWeapon].SetActive(true);
            for(int i=0; i<weapons.Length; i++)
            {
                if (i != currentWeapon)
                {
                    weapons[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
        }
        if (RightButtonL.activeSelf || LeftButtonL.activeSelf)
        {
            locations[currentLocation].SetActive(true);
            for (int i = 0; i < locations.Length; i++)
            {
                if (i != currentLocation)
                {
                    locations[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < locations.Length; i++)
            {
                locations[i].SetActive(false);
            }
        }

        switch (currentWeapon)
        {
            case 0:
                Weapon.text = "- Deagle";
                break;
            case 1:
                Weapon.text = "- Colt";
                break;
            case 2:
                Weapon.text = "- TT";
                break;
            case 3:
                Weapon.text = "- Luger";
                break;
            case 4:
                Weapon.text = "- Bow";
                break;
        }
        switch (currentLocation)
        {
            case 0:
                Location.text = "- Space";
                break;
            case 1:
                Location.text = "- Western";
                break;
        }
    }
    public void SelectWeapon()
    {
        if (locations[currentLocation].activeSelf || RightButtonL.activeSelf || LeftButtonL.activeSelf)
        {
            RightButtonL.SetActive(false);
            LeftButtonL.SetActive(false);
        }
        RightButtonW.SetActive(true);
        LeftButtonW.SetActive(true);
    }
    public void SelectLocation()
    {
        if (RightButtonW.activeSelf || LeftButtonW.activeSelf)
        {
            RightButtonW.SetActive(false);
            LeftButtonW.SetActive(false);
        }
        RightButtonL.SetActive(true);
        LeftButtonL.SetActive(true);
    }

    public void RightNextW()
    {
        currentWeapon = (currentWeapon+1)%weapons.Length;
    }
    public void LeftNextW()
    {
        if (currentWeapon > 0)
        {
            currentWeapon--;
        }
        else
        {
            currentWeapon = weapons.Length;
        }
    }
    public void RightNextL()
    {
        currentLocation = (currentLocation + 1) % locations.Length;
    }
    public void LeftNextL()
    {
        if (currentLocation>0)
        {
            currentLocation--;
        }
        else
        {
            currentLocation = locations.Length;
        }
    }
    //public void PlayButton()
    //{
    //    SceneManager.LoadScene(currentLocation + 1);
    //}
}
