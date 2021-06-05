using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofScript : MonoBehaviour
{
    public GameObject Roof1;
    public GameObject Roof2;
    public GameObject Roof;
    public GameObject Player;
    // Update is called once per frame
    void Update()
    {

        if ((Vector3.Distance(Player.transform.position, Roof1.transform.position) <= 1.9f) || (Vector3.Distance(Player.transform.position, Roof2.transform.position) <= 1.1f))
        {
            Roof.SetActive(false);
        }
        else
        Roof.SetActive(true);
        
        //Debug.Log(Vector3.Distance(Player.transform.position, transform.position));
    }
}
