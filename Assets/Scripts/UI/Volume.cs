using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer mainMixer;

    public void setMusicVolume(float mVol)
    {
        mainMixer.SetFloat("musicVol", mVol);
        //DataHolder.MusicLvl = mVol;
        PlayerPrefs.SetFloat("musicVol", mVol);
    }

    public void setSoundVolume(float sVol)
    {
        mainMixer.SetFloat("sfxVol", sVol);
        //DataHolder.SoundLvl = sVol;
        PlayerPrefs.SetFloat("sfxVol", sVol);
    }
}
