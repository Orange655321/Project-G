using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster_lvl2 : MonoBehaviour
{
    public List<GameObject> Doors;
    public List<GameObject> Exploded_Doors;
    public GameObject Roof;
    public GameObject cross;
    public List<GameObject> quest_targets;
    private int target_number = 0;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Vector3 lookDir = quest_targets[target_number].transform.position - cross.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        cross.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public void ChangeTarget()
    {
        target_number++;
    }
    public void Door_explosion()
    {
        CameraShake.Shake(1, 1);
        Destroy(Doors[0]);
        Destroy(Doors[1]);
        Exploded_Doors[0].SetActive(true);
        Exploded_Doors[1].SetActive(true);
        Exploded_Doors[0].GetComponent<AudioSource>().Play();
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
