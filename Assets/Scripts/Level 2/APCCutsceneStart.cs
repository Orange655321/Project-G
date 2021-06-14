using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class APCCutsceneStart : MonoBehaviour
{
    public PlayableDirector playableDirector;
    private bool cut_flag = true;
    public BTS bts;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && cut_flag)
        {
            StartCoroutine(ShootDelay());
            cut_flag = false;
            playableDirector.Play();
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSecondsRealtime(8f);
        bts.flag = true;
    }
}
