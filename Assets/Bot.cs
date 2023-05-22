using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    readonly float speed = 40; // moveSpeed
    Animator animator;
    public Transform ball;
    public Transform aimTarget; // aiming gameObject

    public Transform[] targets; // array of targets to aim at

    readonly float force = 11; // ball impact force
    Vector3 targetPosition; // position to where the bot will want to move


    void Start()
    {
        targetPosition = transform.position; // initialize the targetPosition to its initial position in the court
        animator = GetComponent<Animator>(); // reference to our animator for animations 
    }

    void Update()
    {
        Move(); // calling the move method
    }

    void Move()
    {
        targetPosition.x = ball.position.x; // update the target position to the ball's x position so the bot only moves on the x axis
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // lerp it's position
    }

    Vector3 PickTarget() // picks a random target from the targets array to be aimed at
    {
        int randomValue = Random.Range(0, targets.Length); // get a random value from 0 to length of our targets array-1
        return targets[randomValue].position; // return the chosen target
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // if it collides with the ball
        {
            Vector3 dir = PickTarget() - transform.position; // get the direction to where to send the ball
            
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 5, 0);

            Vector3 ballDir = ball.position - transform.position; // get the ball direction from the bot's position

            if (ballDir.x >= 0) // if it is on the right
            {
                animator.Play("forehand"); // play a forehand animation
            }
            else
            {
                animator.Play("backhand"); // otherwise play a backhand animation
            }

            ball.GetComponent<Ball>().hitter = "bot";
        }
    }
}