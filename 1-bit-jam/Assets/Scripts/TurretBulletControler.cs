using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletControler : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject target;
    public Rigidbody2D rigidBody;
    public float angleChangingSpeed;
    public float movementSpeed;

    void Start(){
        target =  GameObject.FindWithTag("Player");
    }
    void FixedUpdate ()
    {
        Vector2 direction = (Vector2)target.transform.position - rigidBody.position;
        direction.Normalize ();
        float rotateAmount = Vector3.Cross (direction, transform.up).z;
        rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;
        rigidBody.velocity = transform.up * movementSpeed;
    }
}
