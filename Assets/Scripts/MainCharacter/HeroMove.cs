using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    public float speedHero = 20f;
   

    public Rigidbody2D rb;
    private Vector2 moveVelocity;
    private Vector2 mousePosition;
    public Camera cam;
    private AnimationController animCtrl;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animCtrl = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speedHero;
        if(moveVelocity != Vector2.zero) // включение и отключение анимации бега в зависимости от того, есть ли в данный момент скорость у персонажа
        {
            animCtrl.RunAnimationOn();
        }
        else
        {
            animCtrl.RunAnimationOff();
        }
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);//положение мыши из экранных в мировые координаты
    }
    

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //угол между вектором от объекта к мыше и осью х
        rb.rotation = angle;// привязка угла к развороту героя
    }
}
