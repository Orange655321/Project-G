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
    private bool Spawn_flag = false;
    private bool once = true;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.transform.position, Trigger.transform.position) <= 1f)
        {
            Spawn_flag = true;
            //Debug.Log("Sraka");
        }
        if ((Spawn_flag) && (once))
        {
            //Debug.Log("yuayaa");
            Mob_Placement();
            once = false;
        }
    }
    public void Mob_Placement()
    {
        StartCoroutine(Interval());
    }
    IEnumerator Interval()
    {
        while (MobNumber != 0)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            //Debug.Log("Sraka");
            Instantiate(Mob, new Vector3(x, y, transform.position.z), transform.rotation);
            yield return new WaitForSeconds(Time_Interval);
            --MobNumber;
        }
        yield return 0;
    }
}
