using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject LeaderboardMenu;
    public GameObject mMenu;
    public GameObject playMenu;
    public GameObject mpMenu;

    public GameObject mSlider;
    public GameObject sSlider;

    public GameObject logo;

    public InputField nicknameInputText;

    private void Start()
    {
        //mSlider.GetComponent<Slider>().value = DataHolder.MusicLvl;
        //sSlider.GetComponent<Slider>().value = DataHolder.SoundLvl;
        mSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("musicVol");
        sSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("sfxVol");
        nicknameInputText.text = PlayerDataHolder.nickname;
    }
    public void PlaySurvival() //
    {
        int flag = 2;
        StartCoroutine(StartTimer(flag));       
    }

    public void PlaySingleplayer()
    {
        int flag = 1;
        StartCoroutine(StartTimer(flag));
    }

    IEnumerator StartTimer(int flag) //
    {
        yield return new WaitForSeconds(0.5f);
        switch(flag)
        {
            case 1: SceneManager.LoadSceneAsync("lvl_1"); break;
            case 2: SceneManager.LoadSceneAsync("Survival"); break;
        }
        
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
        SettingsMenu.SetActive(true);
        mMenu.SetActive(false);
    }

    public void ExitSettings()
    {
        StartCoroutine(ExitSettingsTimer());
    }

    IEnumerator ExitSettingsTimer()
    {
        yield return new WaitForSeconds(0.5f);
        mMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void OpenLeaderboard()
    {
        StartCoroutine(OpenLeaderboardTimer());
    }

    IEnumerator OpenLeaderboardTimer()
    {
        yield return new WaitForSeconds(0.5f);
        logo.SetActive(false);
        LeaderboardMenu.SetActive(true);
        mMenu.SetActive(false);
    }

    public void ExitLeaderboard()
    {
        StartCoroutine(ExitLeaderboardTimer());
    }

    IEnumerator ExitLeaderboardTimer()
    {
        yield return new WaitForSeconds(0.5f);
        logo.SetActive(true);
        mMenu.SetActive(true);
        LeaderboardMenu.SetActive(false);
        PlayerDataHolder.nickname = nicknameInputText.text;
    }

    public void OpenPlay()
    {
        StartCoroutine(OpenPlayTimer());
    }

    IEnumerator OpenPlayTimer()
    {
        yield return new WaitForSeconds(0.5f);
        playMenu.SetActive(true);
        mMenu.SetActive(false);
    }

    public void ExitPlay()
    {
        StartCoroutine(ExitPlayTimer());
    }

    IEnumerator ExitPlayTimer()
    {
        yield return new WaitForSeconds(0.5f);
        mMenu.SetActive(true);
        playMenu.SetActive(false);
    }

    public void OpenMultiplayer()
    {
        StartCoroutine(OpenMultiplayerTimer());
    }

    IEnumerator OpenMultiplayerTimer()
    {
        yield return new WaitForSeconds(0.5f);
        mpMenu.SetActive(true);
        playMenu.SetActive(false);
    }

    public void ExitMultiplayer()
    {
        StartCoroutine(ExitMultiplayerTimer());
    }

    IEnumerator ExitMultiplayerTimer()
    {
        yield return new WaitForSeconds(0.5f);
        playMenu.SetActive(true);
        mpMenu.SetActive(false);     
    }
}
