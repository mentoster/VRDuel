using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private PlayerHP PlayerHP;
    [SerializeField] private SteamVR_LaserPointer Laser;

    public static bool ready;
    public static int difficulty = 0;
    [SerializeField] private int easy=1;
    [SerializeField] private int medium=2;
    [SerializeField] private int hard=3;
    [SerializeField] private Text Difficulty;
    public Text scoreText;
    private int scoreCounter = 0;

    private int BotsCount;
    [SerializeField] private List<GameObject> Bots;
    private int BotsHP = 0;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            ready = true;
        }
        else
        {
            ready = false;
        }

        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            switch (difficulty)
            {
                case 0:
                    for (int i = 0; i < easy; i++)
                    {
                        Bots[i].SetActive(true);
                    }
                    BotsCount = easy;
                    break;
                case 1:
                    for (int i = 0; i < medium; i++)
                    {
                        Bots[i].SetActive(true);
                    }
                    BotsCount = medium;
                    break;
                case 2:
                    for (int i = 0; i < hard; i++)
                    {
                        Bots[i].SetActive(true);
                    }
                    BotsCount = hard;
                    break;
            }
        }    
    }
    private void Update()
    {
        Debug.Log("Можно ли стрелять "+ ready);

        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            for (int i = 0; i < BotsCount; i++)
            {
                BotsHP += Bots[i].GetComponent<EnemyStat>().health;
            }
            if (BotsHP <= 0)
            {
                Laser.thickness = 0.001f;
                PlayerHP.PlayerWin();
            }
            for (int i = 0; i < BotsCount; i++)
            {
                BotsHP -= Bots[i].GetComponent<EnemyStat>().health;
            }
        }
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            switch (difficulty)
            {
                case 0:
                    Difficulty.text = "- Cowboy";
                    break;
                case 1:
                    Difficulty.text = "- Sheriff";
                    break;
                case 2:
                    Difficulty.text = "- Killer";
                    break;
            }
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void AreYouReady()
    {
        ready = true;
        Laser.thickness = 0;

    }
    public void SelectEasy()
    {
        difficulty = 0;
        Debug.Log("Выбрана легкая сложность");
    }
    public void SelectMedium()
    {
        difficulty = 1;
        Debug.Log("Выбранна средняя сложность");
    }
    public void SelectHard()
    {
        difficulty = 2;
        Debug.Log("Выбранна сложная сложность");
    }

    public void Score(int addScore)
    {
        scoreCounter += addScore;
        scoreText.text = scoreCounter.ToString();
    }
}
