using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutScene3Start : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public Hero hero;
    private bool cut_flag = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hero = collision.GetComponent<Hero>();
        if (collision.CompareTag("Player") && cut_flag && hero.C4_flag)
        {
            cut_flag = false;
            playableDirector.Play();
        }
    }
}
