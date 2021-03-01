using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    //private AudioSource audioSrc;
    //private float musicVolume = 1f;

    public AudioMixer mainMixer;

    // Start is called before the first frame update
    void Start()
    {
        //audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //audioSrc.volume = musicVolume;
    }

    public void setMusicVolume(float mVol)
    {
        //musicVolume = vol;
        mainMixer.SetFloat("musicVol", mVol);
    }

    public void setSoundVolume(float sVol)
    {
        mainMixer.SetFloat("sfxVol", sVol);
    }
}
