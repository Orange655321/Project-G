using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject ButtonPlay;
    public GameObject ButtonSettings;
    public GameObject ButtonLeaderboard;
    public GameObject ButtonExit;
    public GameObject settingsButtonExit;
    public GameObject leaderboardButtonExit;
    public void PlayGame()
    {
        StartCoroutine(StartTimer());       
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        StartCoroutine(ExitTimer());
    }

    IEnumerator ExitTimer()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Exit!");
        Application.Quit();
    }

    public void OpenSettings()
    {
        StartCoroutine(OpenSettingsTimer());
    }

    IEnumerator OpenSettingsTimer()
    {
        yield return new WaitForSeconds(0.5f);
        ButtonPlay.SetActive(false);
        ButtonSettings.SetActive(false);
        ButtonLeaderboard.SetActive(false);
        ButtonExit.SetActive(false);
        settingsButtonExit.SetActive(true);
    }

    public void ExitSettings()
    {
        StartCoroutine(ExitSettingsTimer());
    }

    IEnumerator ExitSettingsTimer()
    {
        yield return new WaitForSeconds(0.5f);
        ButtonPlay.SetActive(true);
        ButtonSettings.SetActive(true);
        ButtonLeaderboard.SetActive(true);
        ButtonExit.SetActive(true);
        settingsButtonExit.SetActive(false);
    }

    public void OpenLeaderboard()
    {
        StartCoroutine(OpenLeaderboardTimer());
    }

    IEnumerator OpenLeaderboardTimer()
    {
        yield return new WaitForSeconds(0.5f);
        ButtonPlay.SetActive(false);
        ButtonSettings.SetActive(false);
        ButtonLeaderboard.SetActive(false);
        ButtonExit.SetActive(false);
        leaderboardButtonExit.SetActive(true);
    }

    public void ExitLeaderboard()
    {
        StartCoroutine(ExitLeaderboardTimer());
    }

    IEnumerator ExitLeaderboardTimer()
    {
        yield return new WaitForSeconds(0.5f);
        ButtonPlay.SetActive(true);
        ButtonSettings.SetActive(true);
        ButtonLeaderboard.SetActive(true);
        ButtonExit.SetActive(true);
        leaderboardButtonExit.SetActive(false);
    }
}
