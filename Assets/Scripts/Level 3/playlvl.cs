using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class playlvl : MonoBehaviour
{
    public GameObject proclvl;

    IEnumerator startDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        proclvl.SetActive(false);
    }

    void Start()
    {
        proclvl.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(startDelay());
    }
}
