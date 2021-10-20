using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fader : MonoBehaviour
{

    private const string FADER_PATH = "Fader";

    [SerializeField] private Animator animator;

    private static Fader _instance;

    public static Fader instance
    {
        get
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<Fader>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public bool isFading { get; private set; }

    private Action _fadedINCallback;
    private Action _fadedOUTCallback;

    public void FadeIN(Action fadedINCallback)
    {
        if (isFading)
        {
            return;
        }
        isFading = true;
        _fadedINCallback = fadedINCallback;
        animator.SetBool("faded", true);
    }
    public void FadeOUT(Action fadedOUTCallback)
    {
        if(isFading)
        {
            return;
        }
        isFading = true;
        _fadedOUTCallback = fadedOUTCallback;
        animator.SetBool("faded", false);
    }

    private void Handle_FadeINAnimationOver()
    {
        _fadedINCallback?.Invoke();
        _fadedINCallback = null;
        isFading = false;
    }
    private void Handle_FadeOUTAnimationOver()
    {
        _fadedOUTCallback?.Invoke();
        _fadedOUTCallback = null;
        isFading = false;
    }
}
