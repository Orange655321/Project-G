using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Assassination_CutsceneScript : MonoBehaviour
{
    public PlayableDirector playabledirector;
    public GameObject Player;
    private bool once = true;

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(Player.transform.position, transform.position) <= 1.9f) && once)
        {
            playabledirector.Play();
            once = false;
        }
    }
}
