using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    public GameObject player; // тут объект игрока
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if(player != null) 
        {
            transform.position = player.transform.position + offset;
        }
    }
}