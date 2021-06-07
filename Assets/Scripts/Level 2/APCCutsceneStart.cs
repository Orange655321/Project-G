using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class APCCutsceneStart : MonoBehaviour
{
    public PlayableDirector playableDirector;
    private bool cut_flag = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && cut_flag)
        {
            cut_flag = false;
            playableDirector.Play();
        }
    }
}
