using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterLvl1 : MonoBehaviour
{
    public static int EnemyCount;
    public static bool EnemyKilled = true;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCount = 13;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyCount==0)
        {
            EnemyKilled = false;
        }
        Debug.Log(EnemyCount);
    }
}
