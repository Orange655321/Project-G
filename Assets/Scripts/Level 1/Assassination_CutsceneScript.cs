using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Assassination_CutsceneScript : MonoBehaviour
{
    public PlayableDirector playabledirector;
    public GameObject Player;
    public static bool once = true;
    public GameObject game_master;

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(Player.transform.position, transform.position) <= 1.9f) && once)
        {
            playabledirector.Play();
            once = false;
            game_master.GetComponent<GameMasterLvl1>().ChangeTarget();
        }
    }
}
