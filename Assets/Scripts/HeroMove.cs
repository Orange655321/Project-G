using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    public float speedHero = 20f;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveVelocity;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speedHero;

        //spriteRenderer.flipY = moveVelocity.y > 0.0f;
        //spriteRenderer.flipX = moveVelocity.x > 0.0f;
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + moveVelocity * Time.deltaTime);

        

    }
}
