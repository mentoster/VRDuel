using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip shot1clip;
    int shot1ID;
    Audio shot1;

    public AudioClip bg1clip;
    int bg1ID;
    Audio bg1;

    public AudioClip shot2clip;
    int shot2ID;
    Audio shot2;

    private void Start()
    {
        shot1ID = EazySoundManager.PrepareSound(shot1clip);
        shot1 = EazySoundManager.GetAudio(shot1ID);
        shot2ID = EazySoundManager.PrepareSound(shot2clip);
        shot2 = EazySoundManager.GetAudio(shot2ID);
        bg1ID = EazySoundManager.PrepareSound(bg1clip,true);
        bg1 = EazySoundManager.GetAudio(bg1ID);
        bg1.Play();
    }

    public void PlayShotSound()
    {
        shot1.Stop();
        shot2.Stop();
        if (Random.Range(0,2) == 0)
        {
            shot1.Play();
        } else
        {
            shot2.Play();
        }
        shot1ID = EazySoundManager.PrepareSound(shot1clip);
        shot1 = EazySoundManager.GetAudio(shot1ID);
        shot2ID = EazySoundManager.PrepareSound(shot2clip);
        shot2 = EazySoundManager.GetAudio(shot2ID);
    }
}
