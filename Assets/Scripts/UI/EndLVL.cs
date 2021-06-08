using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLVL : MonoBehaviour
{
    public string nameLVL;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("ПАВЛОВ СЫН СОБАКИ ЕБАНОЙ");
            SceneManager.LoadScene(nameLVL);
        }
    }
}
