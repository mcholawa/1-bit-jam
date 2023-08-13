using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class GameMenager : MonoBehaviour
{
    public GameObject[] posts;
    public int energy;
    public TMP_Text energyText;
    // Start is called before the first frame update
    void Start()
    {
        energy = 100;
        posts = GameObject.FindGameObjectsWithTag("Post").OrderBy(go => go.transform.position.x).ToArray();
        Debug.Log(posts[0]);
        InvokeRepeating("decreaseEnergy", 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void decreaseEnergy() {
        energy --;
        updateEnergyCount();
    }
    public void changeEnergyAmount(int eAmount){
        energy += eAmount;
        updateEnergyCount();
    }
    void updateEnergyCount(){
        energyText.SetText("Energy: " + energy.ToString());
    }
}
