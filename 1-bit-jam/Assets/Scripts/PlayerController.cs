using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameMenager GameMenager;
    public int movementIndex = 0;
    //private Game;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameMenager.posts.Length);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
    void PlayerMovement(){
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            if(movementIndex < GameMenager.posts.Length) {
                movementIndex ++;
                Debug.Log(GameMenager.posts[movementIndex]);
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(movementIndex > 0) {
                movementIndex --;
                Debug.Log("Left");
            }
        }
    }
}
