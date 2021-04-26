using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Unit
{
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
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text armorText;
    [SerializeField]
    private Text scoreText;

    private Rigidbody2D rb;
    //private Vector2 moveVelocity;
    private Vector2 mousePosition;
    [SerializeField]
    private Camera cam;
    private AnimationController animCtrl;
    [SerializeField]
    private GameObject dieCanvas;
    private bool isInvulnerability;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animCtrl = GetComponent<AnimationController>();
        healthText.text = "" + health;
        armorText.text = "" + armor;
        scoreText.text = "" + score;
        isInvulnerability = false;
    }

    void Start()
    {
        pistolBullet = 34;
    }

    void Update()
    {
        healthText.text = "" + health;
        armorText.text = "" + armor;
        scoreText.text = "" + score;
        Move();
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
        dieCanvas.SetActive(true);
        base.Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Items item = collision.gameObject.GetComponent<Items>();
        if (item)
        {
            switch (item.itemType)
            {
                case Items.ItemType.MedKit:
                    if (health != 200)
                    {
                        health = item.healing(health);
                        item.RemoveItem();
                    }
                    break;
                case Items.ItemType.ShieldPack:
                    if (armor != 100)
                    {
                        armor = item.shielding(armor);
                        item.RemoveItem();
                    }
                    break;
                case Items.ItemType.PistolBulletPack:
                    if (pistolBullet != 272)
                    {
                        pistolBullet = item.getPistolBullet(pistolBullet);
                        item.RemoveItem();
                    }
                    break;
            }
        }
    }
    IEnumerator Invulnerability()
    {
        isInvulnerability = true;
        yield return new WaitForSeconds(3);
        isInvulnerability = false;
        yield return null;
    }
}
