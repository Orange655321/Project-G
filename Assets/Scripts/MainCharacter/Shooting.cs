using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject prefabBullet;
    public ParticleSystem partSys;
    private PistolSoundController pistolSC;
    private AnimationController animCtrl;
    private Vector2 mosPosition;
    public Camera cam;
    public GameObject player;
    private Hero hero;

    public float bulletForce = 8f;

    void Start()
    {
        animCtrl = player.GetComponent<AnimationController>();
        pistolSC = GetComponent<PistolSoundController>();
        hero = player.GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))//жмакай ЛКМ
        {
            if (hero.itsShoot())
            {
                shoot();
                animCtrl.ShootAnimationPlay();
                pistolSC.shootSound();
                partSys.Play();
            }
            else
            {
                Debug.Log("No bullet");
            }

        }
    }
    void shoot()
    {
        mosPosition = cam.ScreenToWorldPoint(Input.mousePosition);
       
        GameObject bullet = Instantiate(prefabBullet, transform.position, transform.rotation);//создаем объект из префаба в месте поинта
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 lookDir = (mosPosition - rb.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.eulerAngles = new Vector3(0, 0, angle);
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);//отправляем пулю из ствола на...
    }
}
