using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject leaderboardMenu;
    public GameObject settingsButtonExit;
    public GameObject leaderboardButtonExit;
    public void OpenSettings()
    {
        StartCoroutine(OpenSettingsTimer());
    }

    IEnumerator OpenSettingsTimer()
    {
        yield return new WaitForSeconds(0.5f);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        settingsButtonExit.SetActive(true);
    }

    public void ExitSettings()
    {
        StartCoroutine(ExitSettingsTimer());
    }

    IEnumerator ExitSettingsTimer()
    {
        yield return new WaitForSeconds(0.5f);
        settingsMenu.SetActive(false);
        settingsButtonExit.SetActive(false);
        mainMenu.SetActive(true);
    }
}
