using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator anim;
    protected AudioSource explosion;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        explosion = GetComponent<AudioSource>();

    }
    public void JumpedOn()
    {
        anim.SetTrigger("Death");
        explosion.Play();


    }

    private void Death()
    {
        Destroy(this.gameObject);

    }
}
