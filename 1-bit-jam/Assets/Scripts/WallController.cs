using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    // Start is called before the first frame update
    //rotation is commented in case it will be necessary
    //public canRotate = false;
    public float speed; 
    public GameObject startingPoint;
    public GameObject destinationPoint;
    private bool reverse = false;
    private bool isMoving = true;
    Transform target;
    void Start()
    {
        transform.position = startingPoint.transform.position;
        InvokeRepeating("changeDirection", 0, Random.Range(1.0f, 4.0f));
        target = destinationPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            // Debug.Log(target.position);
            // Debug.Log(transform.position);
            // Debug.Log(Vector3.Distance(transform.position, target.position));
             if(!reverse) 
            {
            target = destinationPoint.transform;
             }
            else{
            target = startingPoint.transform;
            } 
            if(transform.position == target.position) {
                    isMoving = false;
            }else{
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
     
    }
    void changeDirection(){
        reverse = !reverse;
        isMoving = true;
    }
}
