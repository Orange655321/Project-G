using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster_lvl2 : MonoBehaviour
{
    public List<GameObject> Doors;
    public List<GameObject> Exploded_Doors;
    public GameObject Roof;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Door_explosion()
    {
        CameraShake.Shake(1, 1);
        Destroy(Doors[0]);
        Destroy(Doors[1]);
        Exploded_Doors[0].SetActive(true);
        Exploded_Doors[1].SetActive(true);
    }
    public void Building_inside()
    {
        Roof.SetActive(false);
    }
    public void Building_outside()
    {
        Roof.SetActive(true);
    }
}
