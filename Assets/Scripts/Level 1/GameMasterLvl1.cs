using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameMasterLvl1 : MonoBehaviour
{
    public static int EnemyCount;
    public static bool EnemyKilled = true;
    public List<GameObject> Gate;
    public List<GameObject> Opened_Gate;
    public PlayableDirector playabledirector;
    public GameObject cross;
    public List<GameObject> quest_targets;
    private int target_number = 0;

    void Start()
    {
        EnemyCount = 18;
    }

    void Update()
    {
        if (EnemyCount==0)
        {
            EnemyKilled = false;
        }

        Vector3 lookDir = quest_targets[target_number].transform.position - cross.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        cross.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void ChangeTarget()
    {
        target_number++;
    }

    public void Opening()
    {
        Destroy(Gate[0]);
        Destroy(Gate[1]);
        Opened_Gate[0].SetActive(true);
        Opened_Gate[1].SetActive(true);
        playabledirector.Play();
    }
}
