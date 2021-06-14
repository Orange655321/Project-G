using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCutscene2 : MonoBehaviour
{
    public List<GameObject> gates;
    void Start()
    {
        gates[0].transform.rotation = Quaternion.Euler(0, 0, -90f);
        gates[1].transform.rotation = Quaternion.Euler(0, 0, 180f);
        gates[2].transform.rotation = Quaternion.Euler(0, 0, 180f);
    }
}
