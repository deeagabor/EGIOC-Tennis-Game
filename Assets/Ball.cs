using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    Vector3 initialPos; // ball's initial position
    public string hitter;

    int playerScore;
    int botScore;

    [SerializeField] Text playerScoreText;
    [SerializeField] Text botScoreText;

    private void Start()
    {
        initialPos = transform.position; // default it to where we first place it in the scene
        playerScore = 0;
        botScore = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall")) // if the ball hits a wall
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
            transform.position = initialPos; // reset it's position 

            if (hitter == "player")
            {
                playerScore++;
            }
            else if (hitter == "bot")
            {
                botScore++;
            }
            updateScores();
        }
        else
        {
            if (collision.transform.CompareTag("Out")) // if the ball hits a wall
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
                transform.position = initialPos; // reset it's position 

                if (hitter == "player")
                {
                    botScore++;
                }
                else if (hitter == "bot")
                {
                    playerScore++;
                }
                updateScores();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Out"))
        {
            if (hitter == "player")
            {
                botScore++;
            }
            else
            {
                if (hitter == "bot")
                {
                    playerScore++;
                }
            }
            updateScores();

            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
            transform.position = initialPos; // reset it's position 
        }
    }

    void updateScores()
    {
        playerScoreText.text = "Player: " + playerScore;
        botScoreText.text = "Bot: " + botScore;
    }

}