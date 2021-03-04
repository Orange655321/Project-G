using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform bulletPoint;
    public GameObject prefabBullet;

    public float bulletForce = 8f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))//жмакай ЛКМ
        {
            shoot();
        }
    }
    void shoot()
    {
        GameObject bullet = Instantiate(prefabBullet, bulletPoint.position, bulletPoint.rotation);//создаем объект из префаба в месте поинта
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletPoint.up * bulletForce, ForceMode2D.Impulse);//отправляем пулю из ствола на...
    }
}
