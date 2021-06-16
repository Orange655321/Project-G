using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Load_Final_Cutscene_LVL3 : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public Hero hero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hero.action_lock = true;
            playableDirector.Play();
        }
    }
}
