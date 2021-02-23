using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject leaderboardMenu;
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


}
