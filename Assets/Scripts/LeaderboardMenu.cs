using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject leaderboardMenu;
    public GameObject settingsButtonExit;
    public GameObject leaderboardButtonExit;

    public void OpenLeaderboard()
    {
        StartCoroutine(OpenLeaderboardTimer());
    }

    IEnumerator OpenLeaderboardTimer()
    {
        yield return new WaitForSeconds(0.5f);
        mainMenu.SetActive(false);
        leaderboardMenu.SetActive(true);
        leaderboardButtonExit.SetActive(true);
    }

    public void ExitLeaderboard()
    {
        StartCoroutine(ExitLeaderboardTimer());
    }

    IEnumerator ExitLeaderboardTimer()
    {
        yield return new WaitForSeconds(0.5f);
        leaderboardMenu.SetActive(false);
        leaderboardButtonExit.SetActive(false);
        mainMenu.SetActive(true);
    }
}
