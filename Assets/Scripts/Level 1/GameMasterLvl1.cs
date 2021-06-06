using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterLvl1 : MonoBehaviour
{
    public static int EnemyCount;
    public static bool EnemyKilled = true;
    public List<GameObject> Gate;
    public List<GameObject> Opened_Gate;
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
        //Debug.Log(EnemyCount);
    }

    public void Opening()
    {
        Destroy(Gate[0]);
        Destroy(Gate[1]);
        Opened_Gate[0].SetActive(true);
        Opened_Gate[1].SetActive(true);
    }
}
