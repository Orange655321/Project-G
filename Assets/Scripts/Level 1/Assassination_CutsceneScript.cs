using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Assassination_CutsceneScript : MonoBehaviour
{
    public PlayableDirector playabledirector;
    public GameObject Player;
    private Hero hero;
    public static bool once = true;
    public GameObject game_master;
    public GameObject stoper;

    private void Start()
    {
        hero = Player.GetComponent<Hero>();
    }

    void Update()
    {
        if ((Vector3.Distance(Player.transform.position, transform.position) <= 1.9f) && once)
        {
            hero.action_lock = true;
            stoper.SetActive(true);
            StartCoroutine(Delay());
            playabledirector.Play();
            once = false;
            game_master.GetComponent<GameMasterLvl1>().ChangeTarget();
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(54.5f);
        hero.action_lock = false;
        stoper.SetActive(false);
    }
}
