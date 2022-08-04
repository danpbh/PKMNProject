using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float moveSpeed = 5f; //Hardcoded for testing. This value could depend on the Pokémon's base speed.
    public Rigidbody2D rb; //Reference for rigid body.
    public Animator animator = new Animator(); //Reference for the animator.
    public Vector2 movement; //A force to be applied to the Pokémon to move them.


    void Start()
    {
        animator.SetFloat("Vertical", -1f);
    }


    // Update is called once per frame
    void Update()
    {
        // Input
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && movement != Vector2.zero) //If player is Running
        {
            StopCoroutine("delayAnim");
            moveSpeed = baseSpeed * 1.75f;
            animator.speed = 1.5f;
        }

        else if (movement != Vector2.zero) //If player is walking
        {
            StopCoroutine("delayAnim");
            moveSpeed = baseSpeed;
            animator.speed = 1f;
        }

        //If player is walking OR running, update the floats so the animator 
        //knows which direction to face. Otherwise, do NOT update the floats so the direction is saved when idling. (thanks Yamajirou!).
        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude); //Using Sqr is a performance speedup trick to avoid having to calculate a squareroot in determining the vector.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }



    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime); //Normalized to avoid faster diagonal movement (thanks ACETONY!).
    }


    void AnimAtStart()
    {
        StartCoroutine("delayAnim");
    }


    IEnumerator delayAnim()
    { 
        animator.speed = 0;
        yield return new WaitForSeconds(1f);
        animator.speed = 1;
    }


}
