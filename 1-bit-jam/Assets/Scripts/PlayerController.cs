using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    void OnCollisionEnter2D(Collision2D col){
        Debug.Log(col.gameObject.name + "");
        if(col.gameObject.name == "EndingGoal"){
            gameMenagerScript.NextLevel();
        }
        else if(col.gameObject.name == "EnergyBall"){
            gameMenagerScript.ChangeEnergyAmount(15);
            col.gameObject.SetActive(false);
        }
        
     }
    void PlayerMovement()
    {
    float distanceToDestination = Vector3.Distance(transform.position, gameMenagerScript.posts[movementIndex].transform.GetChild(0).position);
    
   
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("right");
            Debug.Log(distanceToDestination);
            if (movementIndex < gameMenagerScript.posts.Length - 1 && distanceToDestination < 1.5)
            {
                movementIndex++;
                Debug.Log("Moving");
                isMoving = true;
                gameMenagerScript.ChangeEnergyAmount(energyCost);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) )
        {
            Debug.Log("left");
            Debug.Log(distanceToDestination);
            if (movementIndex > 0 && distanceToDestination < 1.5)
            {
                Debug.Log("moving");
                movementIndex--;
                isMoving = true;
                gameMenagerScript.ChangeEnergyAmount(energyCost);
            }
        }
    
     if (distanceToDestination < 1.5){
        playerSprite.color = new Color(1, 1, 1, 0.5f);
        }
    else{
        playerSprite.color = new Color(1, 1, 1, 1f);
    }
    if (isMoving){
        textDistance.SetText(distanceToDestination.ToString());
            Transform newPosition = gameMenagerScript.posts[movementIndex].transform.GetChild(0);
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
