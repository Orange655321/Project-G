using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject psMenu;
    public GameObject SettingsMenu;

    public GameObject hero;
    private MouseFollow heroMouseScript;

    private void Start()
    {
       heroMouseScript = hero.GetComponent<MouseFollow>();
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
        heroMouseScript.enabled = true;
        psMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        heroMouseScript.enabled = false;
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
