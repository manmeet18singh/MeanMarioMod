using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float jumpTakeOffSpeed = 10f;
    public float maxSpeed = 10f;
    
    private bool facingRight = true;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        anim = GetComponent<Animator> ();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        // Get input from player (ad keys or arrow keys)
        move.x = Input.GetAxis ("Horizontal");
        // Flip based on x pos
        Flip(move.x);
         // set run amimation
        anim.SetFloat ("Speed", Mathf.Abs(velocity.x) / maxSpeed);
        // speed to run
        targetVelocity = move * maxSpeed;


        // Check for Jump (space key)... no double jump 
        // play sound and animation
        if (Input.GetButtonDown ("Jump") && grounded){            
            FindObjectOfType<AudioManager>().Play("Jump");
            anim.SetBool("Jump", grounded);
            velocity.y = 2 * jumpTakeOffSpeed;
        }
        // Check if we're already in the air, if so cancel the jump
        else if (Input.GetButtonUp ("Jump")){
            if (velocity.y > 0){
                velocity.y = velocity.y * 0.1f;
            }
        }
    }

    // flip sprite when facing left
    private void Flip(float horizontal){
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight){
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
