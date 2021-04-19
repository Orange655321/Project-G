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
    public Text healthText;
    public Text armorText;
    public Text scoreText;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private Vector2 mousePosition;
    [SerializeField]
    private Camera cam;
    private AnimationController animCtrl;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animCtrl = GetComponent<AnimationController>();
        
    }

    void Start()
    {
        
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
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        if (moveVelocity != Vector2.zero) // включение и отключение анимации бега в зависимости от того, есть ли в данный момент скорость у персонажа
        {
            animCtrl.RunAnimationOn();
        }
        else
        {
            animCtrl.RunAnimationOff();
        }

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //угол между вектором от объекта к мыше и осью х
        rb.rotation = angle;
    }
}
