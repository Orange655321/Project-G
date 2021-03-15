using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float max; // край координат
    public float minDist; // дистанция проверки на префабы вокруг
    public GameObject prefabEnemy; // префаб
    public int numberEnemy;
    public int countEnemySpawned;
    public int counterEnemy;

    private GameObject[] respawnPlace;
    void Start()
    {
        counterEnemy = 0;
        respawnPlace = GameObject.FindGameObjectsWithTag("Grass");
    }

    void Update()
    {
        if(numberEnemy != 0 && countEnemySpawned > counterEnemy)//хуета разберись с этим
        {
            Placement();
        }
    }

    public void Placement()
    {
        Collider2D[] coll;
        float x;
        float y;
        int respawnNumber;


        do
        {
            do
            {
                /* x = Random.Range(-max, max) + transform.position.x;// позиция
                 y = Random.Range(-max, max) + transform.position.y;*/
                respawnNumber = Random.Range(0, respawnPlace.Length);
                x = respawnPlace[respawnNumber].transform.position.x;
                y = respawnPlace[respawnNumber].transform.position.y;
            }
            while (Mathf.Pow(x,2) + Mathf.Pow(y, 2) < Mathf.Pow(minDist,2) && Mathf.Pow(x, 2) + Mathf.Pow(y, 2) > Mathf.Pow(max,2));

            /*coll = Physics2D.OverlapCircleAll(new Vector2(x, y), minDist * 2, prefabEnemy.layer); // берем список коллайдеров, которые есть вокруг точки
            
            foreach (Collider2D col in coll) // перебираем все найденные коллайдеры
                if (prefabEnemy.CompareTag(GetComponent<Collider2D>().tag)) check = true; // если хоть один имеет тег префаба - проверка не пройдена
            */
        }
        while (Physics2D.OverlapCircle(new Vector2(x, y), minDist, prefabEnemy.layer) != null); // выйдем только при false - когда вокруг не будет ни одного префаба
        numberEnemy--;
        Instantiate(prefabEnemy, new Vector3(x, y, transform.position.z), transform.rotation); // собственно, ставим сам префаб
        counterEnemy++;
    }

}
