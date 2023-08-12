using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameMenager : MonoBehaviour
{
    public GameObject[] posts;
    // Start is called before the first frame update
    void Start()
    {
        posts = GameObject.FindGameObjectsWithTag("Post").OrderBy(go => go.transform.position.x).ToArray();
        Debug.Log(posts[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
