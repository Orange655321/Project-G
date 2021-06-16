using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class APCCutsceneStart : MonoBehaviour
{
    public PlayableDirector playableDirector;
    private bool cut_flag = true;
    public BTS bts;
    public Hero hero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && cut_flag)
        {
            hero.action_lock = true;
            StartCoroutine(ShootDelay());
            cut_flag = false;
            playableDirector.Play();
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSecondsRealtime(8f);
        hero.action_lock = false;
        bts.flag = true;
    }
}
