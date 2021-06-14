using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChanger : MonoBehaviour
{
    public GameMaster_lvl2 GameMasterLvl2;
    private bool once = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && once)
        {
            GameMasterLvl2.ChangeTarget();
            once = false;
        }
    }
}
