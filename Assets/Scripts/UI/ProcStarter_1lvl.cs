using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ProcStarter_1lvl : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject proc;
    public GameObject control;

    IEnumerator startDelay()
    {
        control.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(5f);
        control.SetActive(false);
        proc.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        proc.SetActive(false);
        Time.timeScale = 1f;
        playableDirector.Play();
    }

    void Start()
    {
        StartCoroutine(startDelay());
    }
}
