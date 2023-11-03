using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
     public GameObject bulletObject;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnBullet", 1.0f, 100.0f);
    }

    // Update is called once per frame
    void spawnBullet()
    {
         Instantiate(bulletObject, transform.position, transform.rotation, transform);
    }
}
