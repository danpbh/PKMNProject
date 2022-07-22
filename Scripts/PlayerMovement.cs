using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f; //Hardcoded for testing. This value could depend on the Pokémon's base speed.

    public Rigidbody2D rb; //Reference for rigid body.
    public Animator animator; //Reference for the animator.

    Vector2 movement; //A force to be applied to the player to move them.

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero) //If statement to catch the direction of idle when entering idle state (thanks Yamajirou!).
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        
        animator.SetFloat("Speed", movement.sqrMagnitude); //Using Sqr is a performance speedup trick to avoid having to calculate a squareroot in determining the vector.

    }

    void FixedUpdate() //We use fixed update to avoid wonky physics if there is a change in framerate.
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime); //Normalized to avoid faster diagonal movement (thanks ACETONY!).
    }
}
