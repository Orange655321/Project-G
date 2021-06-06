using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class SWAT_Spawner : MonoBehaviour
{
    public PlayableDirector playabledirector;
    public GameObject swat;
    private int swatnumber = 5;
    private bool flg = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playabledirector.time > 7f && flg)
        {
            flg = false;
            StartCoroutine(LETSGO());
        }
    }
    IEnumerator LETSGO()
    {
        for (int i = 0; i < swatnumber; ++i)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            Instantiate(swat, new Vector3(x, y, transform.position.z), transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }
        yield return 0;
    }
}

