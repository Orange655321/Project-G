using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene1_ : MonoBehaviour
{
    [SerializeField]
    public PlayableDirector playableDirector;
    private double lastTime;
    private bool once1 = true;
    private bool once2 = true;
    // Start is called before the first frame update

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
            once2 = false;
            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
        lastTime = playableDirector.time;
    }
}
