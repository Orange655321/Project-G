﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;

public class HeroCoop : Unit, IPunObservable
{
    public PhotonView photonView;
    private Vector3 offset;

    [SerializeField]
    private int speed = 5;
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int armor = 0;
    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int pistolBullet = 0;
    private int pistolBulletInClip = 0;
    [SerializeField]
    private float pistolBulletForce = 8f;
    [SerializeField]
    private int AKBullet = 0;
    private int AKBulletInClip = 0;
    [SerializeField]
    private float AKBulletForce = 9f;
    [SerializeField]
    private int shotgunBullet = 0;
    private int shotgunBulletInClip = 0;
    [SerializeField]
    private float shotgunBulletForce = 7f;
    [SerializeField]
    private int sniperBullet = 0;
    private int sniperBulletInClip = 0;
    [SerializeField]
    private int sniperBulletDamage = 75;
    [SerializeField]
    private float attackRange = 0.25f;
    [SerializeField]
    private int attackDamage = 200;


    private Text healthText;

    private Text armorText;

    private Text scoreText;

    private Text ammoText;

    private Rigidbody2D rb;
    private Vector2 mousePosition;

    private Camera cam;
    private AnimationController animCtrl;

    private GameObject dieCanvas;
    [SerializeField]
    private LayerMask enemyLayers;

    private bool isInvulnerability;
    private GameMasterCoop GM;
    public bool Claws_flag = false;
    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;
    public GameMasterLvl1 GameMasterLvl1;
    private bool DeathFlag = false;


    public Transform firePoint;
    private bool[] isWeapon;
    private Weapon isWhatWeapon;// 0 - пистолет, 1 - АК, 2 - дробовик сделай енам
    private float rateOfFireAK = 0.1f;
    private float rateOfFirePistol = 0.6f;
    private float rateOfFireShotgun = 1.3f;
    private float rateOfFireSniper = 2f;
    private float rateOfAttack = 5f;
    private float nextAttackTime;
    private float nextFireAKTime;
    private float nextFirePistolTime;
    private float nextFireShotgunTime;
    private float nextFireSniperTime;
    private float distanceSniperRifle = 50f;


    [SerializeField]
    public GameObject prefabPistolBullet;
    public GameObject prefabAKBullet;
    public GameObject prefabShootgunBullet;
    public ParticleSystem partSys;
    private PistolSoundController pistolSC;
    public GameObject[] spriteChoiceWeapon;


    enum Weapon
    {
        Pistol,
        AK,
        Shotgun,
        SniperRifle
    }
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animCtrl = GetComponent<AnimationController>();
        GM = GameObject.Find("GameManager").GetComponent<GameMasterCoop>();
        dieCanvas = GameObject.Find("DieCanvas");
        dieCanvas.SetActive(false);
        healthText = GameObject.Find("Health").GetComponent<Text>();
        armorText = GameObject.Find("Shield").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        ammoText = GameObject.Find("ammo").GetComponent<Text>();
        spriteChoiceWeapon[0] = GameObject.Find("PistolSelected");
        spriteChoiceWeapon[1] = GameObject.Find("AKSelected");
        spriteChoiceWeapon[2] = GameObject.Find("ShotgunSelected");
        spriteChoiceWeapon[3] = GameObject.Find("SniperRifleSelected");
        for (int i = 0; i < spriteChoiceWeapon.Length; ++i)
        {
            spriteChoiceWeapon[i].SetActive(false);
        }
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        spriteRend = GetComponent<SpriteRenderer>();
        isInvulnerability = false;
        matBlink = Resources.Load("MCblink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
        pistolSC = GetComponent<PistolSoundController>();
    }

    void Start()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
        offset = cam.transform.position - transform.position;
        photonView = GetComponent<PhotonView>(); 
        isWeapon = new bool[] { true, true, true, false }; // 0- пистолет, 1- АК, 2- дробовик, 3 - винтовка снайперсая
        pistolBulletInClip = 17;
        pistolBullet = 34 - pistolBulletInClip;
        AKBulletInClip = 30;
        AKBullet = 60 - AKBulletInClip;
        shotgunBulletInClip = 5;
        shotgunBullet = 20 - shotgunBulletInClip;
        sniperBulletInClip = 1;
        sniperBullet = 10 - sniperBulletInClip;
        isWhatWeapon = 0;
        spriteChoiceWeapon[(int)isWhatWeapon].SetActive(true);
        nextFireAKTime = Time.time;
        nextAttackTime = Time.time;
        nextFirePistolTime = Time.time;
        nextFireShotgunTime = Time.time;
        nextFireSniperTime = Time.time;
        ammoText.text = pistolBulletInClip + " / " + pistolBullet;
       

    }
    void LateUpdate()
    {
        //if (!photonView.IsMine) return;

        cam.transform.position = transform.position + offset;
        
    }
    void Update()
    {
        //if (!photonView.IsMine) return;
        
        if (Input.GetKeyUp(KeyCode.Space) && Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + rateOfAttack;
            animCtrl.KnifeAnimationOn();
            StartCoroutine(Delay());
            knifeAttack();
        }

        if (isWhatWeapon != Weapon.Pistol && Input.GetKeyUp(KeyCode.Alpha1) && isWeapon[0])
        {
            animCtrl.Switcher();
            animCtrl.SwitchToPistol();
            spriteChoiceWeapon[(int)isWhatWeapon].SetActive(false);
            isWhatWeapon = Weapon.Pistol;
            firePoint.localPosition = new Vector3(0.14f, 0.5f);
            ammoText.text = pistolBulletInClip + " / " + pistolBullet;
            spriteChoiceWeapon[(int)isWhatWeapon].SetActive(true);
        }
        else if (isWhatWeapon != Weapon.AK && Input.GetKeyUp(KeyCode.Alpha2) && isWeapon[1])
        {
            animCtrl.Switcher();
            animCtrl.SwitchToAK47();
            spriteChoiceWeapon[(int)isWhatWeapon].SetActive(false);
            isWhatWeapon = Weapon.AK;
            firePoint.localPosition = new Vector3(0.2f, 0.5f);
            ammoText.text = AKBulletInClip + " / " + AKBullet;
            spriteChoiceWeapon[(int)isWhatWeapon].SetActive(true);
        }
        else if (isWhatWeapon != Weapon.Shotgun && Input.GetKeyUp(KeyCode.Alpha3) && isWeapon[2])
        {
            animCtrl.Switcher();
            animCtrl.SwitchToShotgun();
            spriteChoiceWeapon[(int)isWhatWeapon].SetActive(false);
            isWhatWeapon = Weapon.Shotgun;
            firePoint.localPosition = new Vector3(0.2f, 0.5f);
            ammoText.text = shotgunBulletInClip + " / " + shotgunBullet;
            spriteChoiceWeapon[(int)isWhatWeapon].SetActive(true);
        }
        else if (isWhatWeapon != Weapon.SniperRifle && Input.GetKeyUp(KeyCode.Alpha4) && isWeapon[3])
        {
            animCtrl.Switcher();
            animCtrl.SwitchToSniper();
            spriteChoiceWeapon[(int)isWhatWeapon].SetActive(false);
            isWhatWeapon = Weapon.SniperRifle;
            firePoint.localPosition = new Vector3(0.2f, 0.5f);
            ammoText.text = sniperBulletInClip + " / " + sniperBullet;
            spriteChoiceWeapon[(int)isWhatWeapon].SetActive(true);
        }
        if (Input.GetButton("Fire1") && isWhatWeapon == Weapon.AK && Time.time > nextFireAKTime)
        {
            if (itsShoot())
            {
                nextFireAKTime = Time.time + rateOfFireAK;
                shootAK();
                rateOfFireAK = 0.1f;
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            switch (isWhatWeapon)
            {
                case Weapon.Pistol:
                    if (Time.time > nextFirePistolTime && itsShoot())
                    {
                        nextFirePistolTime = Time.time + rateOfFirePistol;
                        shootPistol();
                        rateOfFirePistol = 0.4f;
                    }
                    break;
                case Weapon.Shotgun:
                    if (Time.time > nextFireShotgunTime && itsShoot())
                    {
                        nextFireShotgunTime = Time.time + rateOfFireShotgun;
                        shootShotgun();
                        rateOfFireShotgun = 1.3f;
                    }
                    break;
                case Weapon.SniperRifle:
                    if (Time.time > nextFireSniperTime && itsShoot())
                    {
                        nextFireSniperTime = Time.time + rateOfFireSniper;
                        shootSniperRifle();
                    }
                    break;

            }
        }
        healthText.text = "" + health;
        armorText.text = "" + armor;
        scoreText.text = "" + score;
        if(!DeathFlag)
        Move();
        if (Input.GetKey("space") && DeathFlag)  // если нажата клавиша Esc (Escape)
        {
            base.Die();
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.35f);
        switch (isWhatWeapon)
        {
            case Weapon.Pistol:
                //animCtrl.KnifeAnimationOff();
                animCtrl.SwitchToPistol();
                break;
            case Weapon.AK:
                animCtrl.Switcher();
                animCtrl.SwitchToAK47();
                break;
            case Weapon.Shotgun:
                animCtrl.Switcher();
                animCtrl.SwitchToShotgun();
                break;
        }

    }
    public void AddToScore(int cost)
    {
        score += cost;
    }

    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveInput.Normalize();

            animCtrl.RunAnimationOn();

            rb.velocity = moveInput * speed;
        }
        else
        {
            animCtrl.RunAnimationOff();
        }
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //угол между вектором от объекта к мыше и осью х
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    [PunRPC]
    public override void TakeDamage(int damage)
    {
        if (!isInvulnerability)
        {
            if (armor == 0)
            {
                health -= damage;
                healthText.text = "" + health;
            }
            else
            {
                armor -= damage;
                if (armor <= 0)
                {
                    StartCoroutine(Invulnerability());
                    StartCoroutine(Blinking());
                    armor = 0;
                    armorText.text = "" + armor;
                }
            }

            if (health <= 0)
            {
                Die();
            }
        }
    }
    public override void Die()
    {
        animCtrl.DeathAnimationPlay();
        StartCoroutine(DeathTimer());
        destrHero();
    }
    //костыль для использования статчного метода 
    public static void destrHero()
    {
        GameManager.leave();
    }
    IEnumerator DeathTimer()
    {
        isInvulnerability = true;
        //gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        DeathFlag = true;
        yield return new WaitForSeconds(1.2f);
        dieCanvas.SetActive(true);
        base.Die();

        yield return 0;

}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemsCoop item = collision.gameObject.GetComponent<ItemsCoop>();
        PhotonView pi = item.photonView;
        if (item)
        {
            switch (item.itemType)
            {
                case ItemsCoop.ItemType.MedKit:
                    if (health != 200)
                    {
                        health = item.healing(health);
                        if (!pi.IsMine) pi.RPC("RemoveItem", RpcTarget.MasterClient);
                        else item.RemoveItem();
                    }
                    break;
                case ItemsCoop.ItemType.ShieldPack:
                    if (armor != 100)
                    {
                        armor = item.shielding(armor);
                        if (!pi.IsMine) pi.RPC("RemoveItem", RpcTarget.MasterClient);
                        else item.RemoveItem();
                    }
                    break;
                case ItemsCoop.ItemType.PistolBulletPack:
                    if (pistolBullet != 272)
                    {
                        pistolBullet = item.getPistolBullet(pistolBullet);
                        if (!pi.IsMine) pi.RPC("RemoveItem", RpcTarget.MasterClient);
                        else item.RemoveItem();
                    }
                    break;
                case ItemsCoop.ItemType.Pistol:
                    if (isWeapon[0])
                    {
                        pistolBullet = item.getPistolBullet(pistolBullet + 7);
                    }
                    else
                    {
                        isWeapon[0] = true;
                    }
                    if (!pi.IsMine) pi.RPC("RemoveItem", RpcTarget.MasterClient);
                    else item.RemoveItem();
                    break;
                case ItemsCoop.ItemType.AK:
                    if (isWeapon[1])
                    {
                        AKBullet = item.getAKBullet(AKBullet + 15);
                    }
                    else
                    {
                        isWeapon[1] = true;
                    }
                    if (!pi.IsMine) pi.RPC("RemoveItem", RpcTarget.MasterClient);
                    else item.RemoveItem();
                    break;
                case ItemsCoop.ItemType.Shotgun:
                    if (isWeapon[2])
                    {
                        shotgunBullet = item.getsShotgunBullet(shotgunBullet + 2);
                    }
                    else
                    {
                        isWeapon[2] = true;
                    }
                    if (!pi.IsMine) pi.RPC("RemoveItem", RpcTarget.MasterClient);
                    else item.RemoveItem();
                    break;
                case ItemsCoop.ItemType.Claws:
                    Claws_flag = true;
                    if (!pi.IsMine) pi.RPC("RemoveItem", RpcTarget.MasterClient);
                    else item.RemoveItem();
                    break;            
            }
        }
        
    }
    public bool itsShoot()
    {
        if (DeathFlag)
        {
            return false;
        }
        switch (isWhatWeapon)
        {
            case Weapon.Pistol:
                if (pistolBullet > 0 || pistolBulletInClip > 0)
                {
                    --pistolBulletInClip;

                    if (pistolBulletInClip == 0)
                    {
                        rateOfFirePistol *= 2;
                        if (pistolBullet > 17)
                        {
                            pistolBulletInClip = 17;
                        }
                        else
                        {
                            pistolBulletInClip = pistolBullet;
                        }
                        pistolBullet -= pistolBulletInClip;
                    }
                    ammoText.text = pistolBulletInClip + " / " + pistolBullet;
                    return true;
                }
                else
                {
                    Debug.Log("No bullet pistol");
                    return false;
                }
            case Weapon.AK:
                if (AKBulletInClip > 0 || AKBullet > 0)
                {
                    --AKBulletInClip;
                    if (AKBulletInClip == 0)
                    {
                        rateOfFireAK *= 10f;
                        if (AKBullet > 30)
                        {
                            AKBulletInClip = 30;
                        }
                        else
                        {
                            AKBulletInClip = AKBullet;
                        }
                        AKBullet -= AKBulletInClip;
                    }
                    ammoText.text = AKBulletInClip + " / " + AKBullet;
                    return true;
                }

                else
                {
                    Debug.Log("No bullet AK");
                    return false;
                }
            case Weapon.Shotgun:
                if (shotgunBulletInClip > 0 || shotgunBullet > 0)
                {
                    --shotgunBulletInClip;
                    if (shotgunBulletInClip == 0)
                    {
                        rateOfFireShotgun *= 1.5f;
                        if (shotgunBullet > 5)
                        {
                            shotgunBulletInClip = 5;
                        }
                        else
                        {
                            shotgunBulletInClip = shotgunBullet;
                        }
                        shotgunBullet -= shotgunBulletInClip;
                    }
                    ammoText.text = shotgunBulletInClip + " / " + shotgunBullet;
                    return true;
                }
                else
                {
                    Debug.Log("No bullet shotgun");
                    return false;
                }
            case Weapon.SniperRifle:
                if (sniperBulletInClip > 0 || sniperBullet > 0)
                {
                    --sniperBulletInClip;
                    if (sniperBulletInClip == 0)
                    {
                        if (sniperBullet > 1)
                        {
                            sniperBulletInClip = 1;
                        }
                        else
                        {
                            sniperBulletInClip = sniperBullet;
                        }
                        sniperBullet -= sniperBulletInClip;
                    }
                    ammoText.text = sniperBulletInClip + " / " + sniperBullet;
                    return true;
                }
                else
                {
                    Debug.Log("No bullet sniper");
                    return false;
                }
        }
        return false;
    }

    private void knifeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    private void shootSniperRifle()
    {
        //Ray2D ray = new Ray2D(transform.position, mousePosition);
        RaycastHit2D[] raycasts = Physics2D.RaycastAll(transform.position, mousePosition, distanceSniperRifle, enemyLayers);
        foreach(RaycastHit2D enemy in raycasts) 
        {
            enemy.collider.GetComponent<Enemy>().TakeDamage(sniperBulletDamage);
        }
    }
    private void shootPistol()
    {
        GameObject bullet = PhotonNetwork.Instantiate(prefabPistolBullet.name, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * pistolBulletForce, ForceMode2D.Impulse);
        animCtrl.ShootAnimationPlay();
        pistolSC.shootSound();
        partSys.Play();
    }

    private void shootAK()
    {
        GameObject bullet = PhotonNetwork.Instantiate(prefabAKBullet.name, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * AKBulletForce, ForceMode2D.Impulse);
        animCtrl.ShootAnimationPlay();
        pistolSC.shootSound();
        partSys.Play();
    }

    private void shootShotgun()
    {
        GameObject bullet;
        Rigidbody2D rb;
        for (int i = 0; i < 5; ++i)
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 10f + 5f * (float)i);
            bullet = PhotonNetwork.Instantiate(prefabShootgunBullet.name, firePoint.position, firePoint.rotation);
            rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.transform.up * shotgunBulletForce/2, ForceMode2D.Impulse);
        }
        firePoint.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
        animCtrl.ShootAnimationPlay();
        pistolSC.shootSound();
        partSys.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && Claws_flag && collision.CompareTag("Gate"))
        {
            GameMasterLvl1.Opening();
        }
    }   

    IEnumerator Invulnerability()
    {
        isInvulnerability = true;
        yield return new WaitForSeconds(2.16f);
        isInvulnerability = false;
        yield return null;
    }
    IEnumerator Blinking()
    {
        for (int i = 0; i < 6; ++i)
        {
            spriteRend.material = matBlink;
            yield return new WaitForSeconds(0.180f);
            spriteRend.material = matDefault;
            yield return new WaitForSeconds(0.180f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        { stream.SendNext(score); }
        else { score = (int)stream.ReceiveNext(); }
        throw new System.NotImplementedException();
    }
}