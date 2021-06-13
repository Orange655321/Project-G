using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ProcStarter : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject proc;

    IEnumerator startDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        proc.SetActive(false);
        playableDirector.Play();
    }

    void Start()
    {
        proc.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(startDelay());
    }
}
