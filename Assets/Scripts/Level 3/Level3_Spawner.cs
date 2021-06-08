using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_Spawner : MonoBehaviour
{
    private bool Spawn_flag = true;
    public GameObject Mob;
    public GameObject Place;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Spawn_flag)
        {
            for (int i=0; i<3;++i)
            {
                float x = Place.transform.position.x;
                float y = Place.transform.position.y;
                Instantiate(Mob, new Vector3(x, y, Place.transform.position.z), Place.transform.rotation);
            }
            Spawn_flag = false;
        }
    }
}
