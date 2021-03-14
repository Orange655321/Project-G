using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float minX, minY; // край координат
    public float maxX, maxY; // край координат
    public float minDist; // дистанция проверки на префабы вокруг
    public GameObject prefabEnemy; // список префабов
    public Transform player; // платформа
    public string prefab_tag; // тег для префабов - ставить на все префабы
    public int numberEnemy;
    public int countEnemySpawned;
    public int counterEnemy;
    void Start()
    {
        counterEnemy = 0;
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
        bool check;
        float x = Random.Range(minX, maxX) + player.position.x;// позиция
        float y = Random.Range(minY, maxY) + player.position.y; // позиция
        do
        {
            check = false; // проверка пройдена
            coll = Physics2D.OverlapCircleAll(new Vector2(x, player.position.y), minDist); // берем список коллайдеров, которые есть вокруг точки
            numberEnemy--;
            foreach (Collider2D col in coll) // перебираем все найденные коллайдеры
                if (GetComponent<Collider2D>().tag == prefab_tag) check = true; // если хоть один имеет тег префаба - проверка не пройдена
            
        }
        while (check); // выйдем только при false - когда вокруг не будет ни одного префаба
       
        Instantiate(prefabEnemy, new Vector3(x, y, player.position.z), transform.rotation); // собственно, ставим сам префаб
        counterEnemy++;
    }

}
