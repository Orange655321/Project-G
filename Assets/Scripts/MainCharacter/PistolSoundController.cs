using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolSoundController : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip shootFX;
    public AudioClip reloadFX;

    public void shootSound()
    {
        myFX.PlayOneShot(shootFX);
    }
    public void reloadSound()
    {
        myFX.PlayOneShot(reloadFX);
    }
}
