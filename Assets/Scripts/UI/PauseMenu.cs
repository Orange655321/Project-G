using System.Collections;
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

    private void Start()
    {
        mSlider.GetComponent<Slider>().value = DataHolder.MusicLvl;
        sSlider.GetComponent<Slider>().value = DataHolder.SoundLvl;
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

    public void Resume()
    {
        psMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
