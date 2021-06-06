using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitSpawner : MonoBehaviour
{
    public float SpawnDistance;
    public int MobNumber;
    public GameObject Mob;
    public GameObject Player;
    void Update()
    {
        if (MobNumber !=0 && (Vector3.Distance(Player.transform.position, transform.position) <= SpawnDistance))
        {
            Mob_Placement();
            --MobNumber;
        }
    }
    public void Mob_Placement()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Instantiate(Mob, new Vector3(x, y, transform.position.z), transform.rotation);
    }
}
