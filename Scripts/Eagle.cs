using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Eagle : Enemy
{
    private float leftCap;
    private float rightCap;
    [SerializeField] private float moveLength;
    [SerializeField] private float moveSpeed;

    private bool facingLeft = false;
    private Collider2D coll;
    private Rigidbody2D rb;


    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        leftCap = transform.position.x - moveSpeed / 4;
        rightCap = transform.position.x + moveSpeed / 4;


    }

    public void EagleMove()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.position.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                rb.velocity = new Vector2(-moveSpeed, 0);
            }
            else
            {
                facingLeft = false;
            }
        }

        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.position.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                    rb.velocity = new Vector2(moveSpeed, 0);
                
            }
            else
            {
                facingLeft = true;
            }
        }
    }
}
