using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTarget : MonoBehaviour
{
    public GameObject ZeroPoint;
    public GameObject scoreCanvas;
    public GameObject scoreEffect;

    float distance;

    public void TakeDamage(Vector3 bulletPosition)
    {
        distance = Vector3.Distance(ZeroPoint.transform.position, bulletPosition);
        if (distance < 0.07)
        {
            Debug.Log("100");
            scoreCanvas.GetComponent<MenuScript>().Score(100);
            ScoreEffect(100);
        }
        else if (distance < 0.2 && distance > 0.07)
        {
            Debug.Log("50");
            scoreCanvas.GetComponent<MenuScript>().Score(50);
            ScoreEffect(50);
        }
        else if (distance < 0.3 && distance > 0.2)
        {
            Debug.Log("25");
            scoreCanvas.GetComponent<MenuScript>().Score(25);
            ScoreEffect(25);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        ScoreEffect(777);
            
    //}

    public void ScoreEffect(int score)
    {
        scoreEffect.GetComponent<Text>().text = score.ToString();
        scoreEffect.GetComponent<Animation>().Play();
    }
}
