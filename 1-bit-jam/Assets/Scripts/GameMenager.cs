using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class GameMenager : MonoBehaviour
{
    public GameObject[] posts;
    public GameOverScreen GameOverScreen;
    public GameObject player;
    public int energy;
    public TMP_Text energyText;
    // Start is called before the first frame update
    void Start()
    {
        energy = 100;
        posts = GameObject.FindGameObjectsWithTag("Post").OrderBy(go => go.transform.position.x).ToArray();
        Debug.Log(posts[0]);
        InvokeRepeating("DecreaseEnergy", 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
         if (energy <= 0){GameOver();}
    }
    void GameOver()
    {
        GameOverScreen.Setup();
        player.SetActive(false);
        CancelInvoke();
   }
    void DecreaseEnergy() {
        energy --;
        UpdateEnergyCount();
    }
    public void ChangeEnergyAmount(int eAmount){
        energy += eAmount;
        UpdateEnergyCount();
    }
    void UpdateEnergyCount(){
        energyText.SetText("Energy: " + energy.ToString());
    }
}
