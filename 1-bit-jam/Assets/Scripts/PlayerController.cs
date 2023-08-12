using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int movementIndex = 0;
    public GameObject gameMenagerObject;
    public int speed = 1;
    private GameMenager gameMenagerScript;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        gameMenagerScript = gameMenagerObject.GetComponent<GameMenager>();
        Debug.Log(gameMenagerScript.posts.Length);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
    void PlayerMovement()
    {
    if (!isMoving){
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (movementIndex < gameMenagerScript.posts.Length - 1)
            {
                movementIndex++;
                Debug.Log(gameMenagerScript.posts[movementIndex]);
                isMoving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (movementIndex > 0)
            {
                movementIndex--;
                Debug.Log(gameMenagerScript.posts[movementIndex]);
                isMoving = true;
            }
        }
        }
        else{
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
