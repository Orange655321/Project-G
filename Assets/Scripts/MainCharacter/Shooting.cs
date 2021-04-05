using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject prefabBullet;
    public ParticleSystem partSys;
    private PistolSoundController pistolSC;
    public GameObject pistol;
    private AnimationController animCtrl;

    public float bulletForce = 8f;

    void Start()
    {
        animCtrl = GetComponent<AnimationController>();
        pistolSC = pistol.GetComponent<PistolSoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))//жмакай ЛКМ
        {
            shoot();
            animCtrl.ShootAnimationPlay();
            pistolSC.shootSound();
            partSys.Play();
        }
    }
    void shoot()
    {
        GameObject bullet = Instantiate(prefabBullet, firePoint.position, firePoint.rotation);//создаем объект из префаба в месте поинта
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);//отправляем пулю из ствола на...
    }
}
