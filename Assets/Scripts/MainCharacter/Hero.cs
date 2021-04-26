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
    private int score= 0;
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animCtrl = GetComponent<AnimationController>();
        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("MCblink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
    }

    void Start()
    {
        healthText.text = "" + health;
        armorText.text = "" + armor;
        scoreText.text = "" + score;
    }

    void Update()
    {
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
            //if (moveVelocity != Vector2.zero) // включение и отключение анимации бега в зависимости от того, есть ли в данный момент скорость у персонажа

            animCtrl.RunAnimationOn();

            //else

            // animCtrl.RunAnimationOff();

            //transform.position = Vector2.MoveTowards(transform.position, transform.position +(Vector3)moveVelocity, speed * Time.deltaTime);
            //rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
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
        health -= damage;
        healthText.text = "" + health;
        if (health <= 0) 
        {
            Die();
        }
    }

    public override void Die()
    {
        dieCanvas.SetActive(true);
        base.Die();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy")) 
        {
            rb.AddForce(-collision.transform.position, ForceMode2D.Impulse);
        }
    }*/
}
