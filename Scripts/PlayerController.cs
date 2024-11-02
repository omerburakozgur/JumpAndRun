using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Start() Variables
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;



    //Animations Variables
    private enum State { idle, running, jumping, takehit, death, falling }
    private State state = State.idle;
    private int jumpRight = 2;
    private int hp = 3;


    //Inspector Variables
    [SerializeField] LayerMask ground;
    [SerializeField] private float RunSpeed = 5f;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private int gems = 0;
    [SerializeField] private int hurtForce = 3;
    [SerializeField] private TextMeshProUGUI scoreNumber;
    [SerializeField] private TextMeshProUGUI hpNumber;
    [SerializeField] AudioSource gemCollect;
    [SerializeField] AudioSource footstep;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (state != State.takehit)
        {
            Movement();

        }
        AnimationState();
        anim.SetInteger("state", (int)state);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Collectible"))
        {
            gemCollect.Play();
            Destroy(collision.gameObject);
            gems++;
            scoreNumber.text = gems.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D enemyCollision)
    {
        if (enemyCollision.gameObject.tag == "Enemy")
        {

            Enemy enemy = enemyCollision.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                rb.velocity = new Vector2(rb.velocity.x, 6f);
                state = State.jumping;
                gems++;
                scoreNumber.text = gems.ToString();

            }
            else
            {
                state = State.takehit;

                if (enemyCollision.gameObject.transform.position.x > transform.position.x)
                {
                    hp--;
                    hpNumber.text = hp.ToString();
                    CheckifDead();
                    rb.velocity = new Vector2(-6, rb.velocity.y);

                }
                else
                {
                    hp--;
                    hpNumber.text = hp.ToString();
                    CheckifDead();
                    rb.velocity = new Vector2(6, rb.velocity.y);
                    
                }
            }

        }
    }

    private void Movement()
    {
        float direction = Input.GetAxis("Horizontal");
        if (direction < 0)
        {
            rb.velocity = new Vector2(-RunSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        else if (direction > 0)
        {
            rb.velocity = new Vector2(RunSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }



        if (Input.GetButtonDown("Jump"))
        {
            Jump();

        }
    }

    private void Jump()
    {
        if (coll.IsTouchingLayers(ground))
        {

            jumpRight = 2;

        }

         if (jumpRight > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            state = State.jumping;
            jumpRight--;
        }
    }

    private void AnimationState()
    {

        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }

        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.takehit)
        {
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                state = State.idle;
            }
        }


        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }

        else
        {
            state = State.idle;
        }
    }

    private void Footstep()
    {
        footstep.Play();
    }

    private void CheckifDead()
    {
        if (hp == 0)
        {
            SceneManager.LoadScene("Dead");

        }
    }
}
