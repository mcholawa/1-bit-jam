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
    public int energyCost;
    public bool isUp = false;
    private GameObject spriteMask;
    private bool isExpanding = false;
    private Vector3 scaleChange = new Vector3(1f, 1f, 1f);
    public float scaleSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
        energyCost = -5;
        playerSprite = GetComponent<SpriteRenderer>();
        gameMenagerScript = gameMenagerObject.GetComponent<GameMenager>();
        //Debug.Log(gameMenagerScript.posts.Length);
        spriteMask = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        RotateSpriteMaks();
        //scaling the mask 
        if(isExpanding){
            spriteMask.transform.localScale += scaleChange * Time.deltaTime * scaleSpeed;
            if(Vector3.Distance(spriteMask.transform.localScale, new Vector3(16f, 16f, 16f)) < 1.0f ){
                scaleChange = -scaleChange;
            }
            else if(Vector3.Distance(spriteMask.transform.localScale, new Vector3(4f, 4f, 4f)) < 1.0f && scaleChange.x < 0){
                    isExpanding = false;
                    scaleChange = -scaleChange;
            }
        }
    }
    void RotateSpriteMaks(){
        float degreesPerSecond = 50;
        spriteMask.transform.Rotate( new Vector3(0, 0,degreesPerSecond ) * Time.deltaTime);
    }
    //collision manager
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
        else if (col.gameObject.name== "Wall"){
            gameMenagerScript.GameOver();
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
        else if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Space");
            spaceEvent();
        }
        if (isMoving)
        {
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
    void spaceEvent(){
        if (gameMenagerScript.energy > 10 && !isExpanding){
            isExpanding = true;
            gameMenagerScript.ChangeEnergyAmount(-10);
        }
    }
}
