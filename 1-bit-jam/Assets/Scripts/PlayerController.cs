using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public int movementIndex = 0;
    public GameObject gameMenagerObject;
    public int speed = 1;
    private GameMenager gameMenagerScript;
    private bool isMoving = false;
    private SpriteRenderer playerSprite;
    public TMP_Text textDistance;
    public int energyCost;
    public bool isUp = false;
    // Start is called before the first frame update
    void Start()
    {
        energyCost = -5;
        playerSprite = GetComponent<SpriteRenderer>();
        gameMenagerScript = gameMenagerObject.GetComponent<GameMenager>();
        Debug.Log(gameMenagerScript.posts.Length);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name + "");
        if (col.gameObject.name == "EndingGoal")
        {
            gameMenagerScript.NextLevel();
        }
        else if (col.gameObject.name == "EnergyBall")
        {
            gameMenagerScript.ChangeEnergyAmount(15);
            col.gameObject.SetActive(false);
        }

    }
    //Function used in Array.Find for checking UpArrow and DownArrow movement :)
    private bool findMatchingPostX(GameObject upPost){
        float playerPositionX = transform.position.x;
            return upPost.transform.position.x == playerPositionX;
        }
    void PlayerMovement()
    {
        float distanceToDestination = 0;
        int currentPostsLength;
        Transform nextTarget = null;
        //Key Input 
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("right");
            if(isUp){
            currentPostsLength = gameMenagerScript.upPosts.Length;
            distanceToDestination = Vector3.Distance(transform.position, gameMenagerScript.upPosts[movementIndex].transform.GetChild(0).position);
                if(movementIndex < currentPostsLength - 1) nextTarget = gameMenagerScript.upPosts[movementIndex + 1].transform.GetChild(0);
            }else{
            currentPostsLength = gameMenagerScript.posts.Length;
            distanceToDestination = Vector3.Distance(transform.position, gameMenagerScript.posts[movementIndex].transform.GetChild(0).position);
                if(movementIndex < currentPostsLength - 1) nextTarget = gameMenagerScript.posts[movementIndex +1].transform.GetChild(0);
            }
            if (movementIndex < currentPostsLength - 1 && distanceToDestination < 1.5 && distanceToDestination <= 6)
            {
                float distanceToNext = Vector3.Distance(transform.position, nextTarget.position);
                if (distanceToNext < 7)
                {
                    movementIndex++;
                    Debug.Log("Moving");
                    isMoving = true;
                    gameMenagerScript.ChangeEnergyAmount(energyCost);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("left");
            if(isUp && movementIndex > 0){
            distanceToDestination = Vector3.Distance(transform.position, gameMenagerScript.upPosts[movementIndex].transform.GetChild(0).position);
            nextTarget = gameMenagerScript.upPosts[movementIndex-1].transform.GetChild(0);
            }else if(!isUp && movementIndex > 0){
            distanceToDestination = Vector3.Distance(transform.position, gameMenagerScript.posts[movementIndex].transform.GetChild(0).position);
            nextTarget = gameMenagerScript.upPosts[movementIndex-1].transform.GetChild(0);
            }
            if (movementIndex > 0 && distanceToDestination < 1.5 && distanceToDestination < 1.5 && distanceToDestination <= 6)
            {
                float distanceToNext = Vector3.Distance(transform.position, nextTarget.position);
                if (distanceToNext < 7)
                {
                    movementIndex--;
                    Debug.Log("moving");
                    isMoving = true;
                    gameMenagerScript.ChangeEnergyAmount(energyCost);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !isUp){
            Debug.Log("up");
            GameObject matchingPostFound = Array.Find(gameMenagerScript.upPosts, findMatchingPostX);
             if(matchingPostFound != null){
                Debug.Log(matchingPostFound.name); 
                movementIndex = Array.IndexOf(gameMenagerScript.upPosts, matchingPostFound);
                Debug.Log("moving");
                isUp = true;
                isMoving = true;
                gameMenagerScript.ChangeEnergyAmount(energyCost);
             }
             else{
              Debug.Log("Nie ma tam nic :(");
             }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && isUp){
            Debug.Log("down");
             GameObject matchingPostFound = Array.Find(gameMenagerScript.posts, findMatchingPostX);
             if(matchingPostFound != null){
                Debug.Log(matchingPostFound.name); 
                movementIndex = Array.IndexOf(gameMenagerScript.posts, matchingPostFound);
                Debug.Log("moving");
                isUp = false;
                isMoving = true;
                gameMenagerScript.ChangeEnergyAmount(energyCost);
             }
             else{
               Debug.Log("Nie ma tam nic :(");
             }
        }
        if (distanceToDestination < 1.5)
        {
            playerSprite.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            playerSprite.color = new Color(1, 1, 1, 1f);
        }
        if (isMoving)
        {
            textDistance.SetText(distanceToDestination.ToString());
            Transform newPosition;
            if(isUp){
            newPosition = gameMenagerScript.upPosts[movementIndex].transform.GetChild(0);
            }else{
            newPosition = gameMenagerScript.posts[movementIndex].transform.GetChild(0);
            }
            if (transform.position == newPosition.position)
            {
                isMoving = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition.position, speed * Time.deltaTime);
            }
        }
    }

}
