using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float minX, minY; // край координат
    public float maxX, maxY; // край координат
    public float minDist; // дистанция проверки на префабы вокруг
    public GameObject prefabEnemy; // список префабов
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
        float x;
        float y;


        do
        {
            do
            {
                x = Random.Range(minX, maxX) + transform.position.x;// позиция
            }
            while (Mathf.Abs(x - transform.position.x) < minDist * 2);

            do
            {
                y = Random.Range(minY, maxY) + transform.position.y;// позиция
            }
            while (Mathf.Abs(y - transform.position.y) < minDist * 2);
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
