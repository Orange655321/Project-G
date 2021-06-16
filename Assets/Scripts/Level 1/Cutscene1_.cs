using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene1_ : MonoBehaviour
{
    [SerializeField]
    public PlayableDirector playableDirector;
    public Hero hero;
    private double lastTime;
    private bool once1 = true;
    private bool once2 = true;
    // Start is called before the first frame update

    private void Start()
    {
        hero.action_lock = true;
        StartCoroutine(Delay(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (playableDirector.time >= 3.5f && lastTime < 3.5f && once1)
        {
            once1 = false;
            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
            
            //SetSpeed(1) чтобы включить
        }
        if (!GameMasterLvl1.EnemyKilled && once2)
        {
            hero.action_lock = true;
            StartCoroutine(Delay(1));
            once2 = false;
            GameMasterLvl1.EnemyCount = 18;
            GameMasterLvl1.EnemyKilled = true;
            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
        lastTime = playableDirector.time;
    }

    IEnumerator Delay(int num)
    {
        switch(num)
        {
            case 0: yield return new WaitForSecondsRealtime(13f); hero.action_lock = false; break;
            case 1: yield return new WaitForSecondsRealtime(5.5f); hero.action_lock = false; break;
        }

        yield return 0;
    }
}
