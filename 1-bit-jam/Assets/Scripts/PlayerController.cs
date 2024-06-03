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
    public bool IsAvailable = true;
    public float CooldownDuration;
    private GameObject[] currentPosts;

    // Start is called before the first frame update
    void Start()
    {
        energyCost = -5;
        playerSprite = GetComponent<SpriteRenderer>();
        gameMenagerScript = gameMenagerObject.GetComponent<GameMenager>();
        //Debug.Log(gameMenagerScript.posts.Length);
        spriteMask = gameObject.transform.GetChild(0).gameObject;
        currentPosts = gameMenagerScript.posts;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        RotateSpriteMaks();
        //scaling the mask 
        if (isExpanding)
        {
            spriteMask.transform.localScale += scaleChange * Time.deltaTime * scaleSpeed;
            if (Vector3.Distance(spriteMask.transform.localScale, new Vector3(16f, 16f, 16f)) < 1.0f)
            {
                scaleChange = -scaleChange;
            }
            else if (Vector3.Distance(spriteMask.transform.localScale, new Vector3(4f, 4f, 4f)) < 1.0f && scaleChange.x < 0)
            {
                isExpanding = false;
                scaleChange = -scaleChange;
            }
        }
    }
    void RotateSpriteMaks()
    {
        float degreesPerSecond = 50;
        spriteMask.transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
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
        else if (col.gameObject.name == "Wall")
        {
            gameMenagerScript.GameOver();
        }

    }
    //Function used in Array.Find for checking UpArrow and DownArrow movement :)
    private bool findMatchingPostX(GameObject upPost)
    {
        float playerPositionX = transform.position.x;
        return upPost.transform.position.x == playerPositionX;
    }
    void PlayerMovement()
    {

        if (isMoving)
        {
            Transform newPosition;
            newPosition = currentPosts[movementIndex].transform.GetChild(0);
            if (transform.position == newPosition.position)
            {
                isMoving = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition.position, speed * Time.deltaTime);
            }
        }
        if (IsAvailable)
        {
            //if right arrow is pressed AND it is not the last post AND there is no gap
            if (Input.GetKeyDown(KeyCode.RightArrow) && movementIndex < currentPosts.Length - 1 && 6 >= Vector2.Distance(currentPosts[movementIndex].transform.position,currentPosts[movementIndex+1].transform.position))
            {
                Debug.Log("right");
                movementIndex++;
                isMoving = true;
                StartCoroutine(StartCooldown());
                gameMenagerScript.ChangeEnergyAmount(energyCost);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && movementIndex != 0  && 6 >= Vector2.Distance(currentPosts[movementIndex].transform.position,currentPosts[movementIndex-1].transform.position))
            {
                Debug.Log("left");
                movementIndex--;
                isMoving = true;
                StartCoroutine(StartCooldown());
                gameMenagerScript.ChangeEnergyAmount(energyCost);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && !isUp)
            {
                GameObject matchingPostFound = Array.Find(gameMenagerScript.upPosts, findMatchingPostX);
                 if(matchingPostFound != null){
                    movementIndex = Array.IndexOf(gameMenagerScript.upPosts, matchingPostFound);
                    currentPosts = gameMenagerScript.upPosts;
                    isMoving = true;
                    isUp = true;
                    Debug.Log("up");
                    StartCoroutine(StartCooldown());
                }
               

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && isUp)
            {
                GameObject matchingPostFound = Array.Find(gameMenagerScript.posts, findMatchingPostX);
                if(matchingPostFound != null){
                movementIndex = Array.IndexOf(gameMenagerScript.posts, matchingPostFound);
                isMoving = true;
                 currentPosts = gameMenagerScript.posts;
                isUp = false;
                Debug.Log("down");
                StartCoroutine(StartCooldown());
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            //print all posts
            foreach (var item in currentPosts)
            {
                Debug.Log(item.ToString());
            }
            Debug.Log("movement index:");
            Debug.Log(movementIndex);
            spaceEvent();
        }
    }
    //Key Input 
    void spaceEvent()
    {
        if (gameMenagerScript.energy > 10 && !isExpanding)
        {
            isExpanding = true;
            gameMenagerScript.ChangeEnergyAmount(-10);
        }
    }

    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        Debug.Log(CooldownDuration);
        playerSprite.color = new Color (1f,1f,1f,0.3f);
        yield return new WaitForSeconds(CooldownDuration);

        IsAvailable = true;
        Debug.Log("not Cooldown");
        playerSprite.color = new Color (1f,1f,1f,1f);
    }
}
