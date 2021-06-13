using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class starter : MonoBehaviour
{
    public GameObject proc3;

    IEnumerator startDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        proc3.SetActive(false);
    }

    void Start()
    {
        proc3.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(startDelay());
    }
}
