﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject psMenu;
    public GameObject SettingsMenu;

    public GameObject mSlider;
    public GameObject sSlider;

   // public GameObject FirePoint;
    //private Shooting script;

    private void Start()
    {
        // script = Player.GetComponent<Shooting>();
        //mSlider.GetComponent<Slider>().value = DataHolder.MusicLvl;
        //sSlider.GetComponent<Slider>().value = DataHolder.SoundLvl;
        mSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("musicVol");
        sSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("sfxVol");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    IEnumerator ResumeDelay()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        SettingsMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        psMenu.SetActive(false);
    }
    public void Resume()
    {
        StartCoroutine(ResumeDelay());
    }

    void Pause()
    {
        // script.enabled = false;
        //FirePoint.SetActive(false);
        psMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;       
    }

    public void OpenSettings()
    {
        SettingsMenu.SetActive(true);
        psMenu.SetActive(false);
    }

    public void ExitSettings()
    {
        psMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void ToMainMenu()
    {
       // script.enabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
