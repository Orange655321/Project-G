using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2_lvl1 : MonoBehaviour
{
    public GameObject Mob;
    public float Time_Interval;
    public int MobNumber;
    public List<GameObject> Triggers;
    public GameObject Player;
    public List<GameObject> Places;
    private bool Spawn_flag = false;
    private bool once_ = true;
    // Update is called once per frame
    int kal = 0;
    void Start()
    {
        MobNumber *= Places.Count;
    }
    void Update()
    {
        if (Vector3.Distance(Player.transform.position, Triggers[0].transform.position) <= 1f)
        {
            Spawn_flag = true;
        }
        if (Vector3.Distance(Player.transform.position, Triggers[1].transform.position) <= 1f)
        {
            Spawn_flag = true;
        }
        if ((Spawn_flag) && (once_) && (!Assassination_CutsceneScript.once))
        {
            Mob_Placement();
            once_ = false;
        }
    }
    public void Mob_Placement()
    {
        foreach (GameObject SpawnPoint in Places)
        {
            StartCoroutine(Interval(SpawnPoint));
        }
    }
    IEnumerator Interval(GameObject SpawnPoint)
    {
        if (MobNumber >= 0)
        {
            do
            {
                float x = SpawnPoint.transform.position.x;
                float y = SpawnPoint.transform.position.y;
                Instantiate(Mob, new Vector3(x, y, SpawnPoint.transform.position.z), SpawnPoint.transform.rotation);
                ++kal;
                yield return new WaitForSeconds(Time_Interval);
                --MobNumber;

            } while (MobNumber >= 0);
        }
        yield return 0;
    }
}
