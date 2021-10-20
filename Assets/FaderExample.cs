using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class FaderExample : MonoBehaviour
{
    private const string SCENE_0 = "Main Menu";
    private const string SCENE_1 = "Tutorial";
    private const string SCENE_2 = "Western";

    private bool _isLoading;

    private static FaderExample _instance;

    //private void Awake()
    //{
    //    if (_instance != null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    _instance = this;
    //    DontDestroyOnLoad(gameObject);
    //}
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    LoadScene(SCENE_0);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    LoadScene(SCENE_1);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    LoadScene(SCENE_2);
        //}
    }

    public void LoadScene()
    {
        if (_isLoading)
        {
            return;
        }
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            StartCoroutine(LoadSceneRoutine(0));
        }
        else
        {
            StartCoroutine(LoadSceneRoutine(GameControl.currentLocation + 1));
        }
    }
    private IEnumerator LoadSceneRoutine(int sceneName)
    {
        _isLoading = true;

        var waitFading = true;
        Fader.instance.FadeIN(() => waitFading = false);

        while (waitFading)
        {
            yield return null;
        }

        var async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;

        waitFading = true;
        Fader.instance.FadeOUT(() => waitFading = false);

        while (waitFading)
        {
            yield return null;
        }
        _isLoading = false;

        Destroy(gameObject);
    }
}
