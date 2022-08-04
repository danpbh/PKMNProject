using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float baseSpeed = 5f;
    public float moveSpeed = 2f; //Hardcoded for testing. This value could depend on the Pokémon's base speed.
    public Rigidbody2D rb; //Reference for rigid body.
    public Animator animator; //Reference for the animator.
    public Vector2 movement; //A force to be applied to the Pokémon to move them.

    bool readyToWalk = false;
    int x = 0;
    int y = 0;
    float secondsMoving = 0;
    float secondsToMoveAgain = 0;


    IEnumerator Start()
    {
        animator.SetFloat("Vertical", -1f);
        secondsToMoveAgain = Random.Range(2f, 5f);
        yield return new WaitForSeconds(secondsToMoveAgain);
        readyToWalk = true;
    }


    void Update()
    {
        if (readyToWalk == true)
        {
            readyToWalk = false;
            StopCoroutine("delayAnim");
            StartCoroutine(Move());
        }

        if (movement != Vector2.zero) //If statement to catch the direction of idle when entering idle state (thanks Yamajirou!).
        {
            animator.speed = 1; //This line is necessary incase the Idle delay is still active when transitioning to the moving state.
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude); //Using Sqr is a performance speedup trick to avoid having to calculate a squareroot in determining the vector.
    }


    IEnumerator Move()
    {
        x = Random.Range(-1, 2);
        y = Random.Range(-1, 2);
        secondsMoving = Random.Range(0.5f, 1.6f);
        secondsToMoveAgain = Random.Range(2f,5f);

        movement.x = x;
        movement.y = y;
        yield return new WaitForSeconds(secondsMoving);
        movement.x = 0;
        movement.y = 0;

        yield return new WaitForSeconds(secondsToMoveAgain);
        readyToWalk = true;
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




