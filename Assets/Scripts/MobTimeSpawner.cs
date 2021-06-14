using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTimeSpawner : MonoBehaviour
{
    public GameObject Mob;
    public float Time_Interval;
    public int MobNumber;
    public GameObject Trigger;
    public GameObject Player;
    public List<GameObject> Places;
    public List<GameObject> gates;
    private bool Spawn_flag = false;
    private bool once = true;
    public static bool StartCut = true;
    // Update is called once per frame
    int kal = 0;
    void Start()
    {
        MobNumber *= Places.Count;
    }
    void Update()
    {
        if (Vector3.Distance(Player.transform.position, Trigger.transform.position) <= 1f)
        {
            Spawn_flag = true;
        }
        if ((Spawn_flag) && (once))
        {
            Mob_Placement();
            gates[0].transform.rotation = Quaternion.Euler(0, 0, 270f);
            gates[1].transform.rotation = Quaternion.Euler(0, 0, 90f);
            once = false;
        }
        if (StartCut && MobNumber<=0)
        {
            StartCut = false;
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
