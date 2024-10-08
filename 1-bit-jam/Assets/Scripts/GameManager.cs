using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject[] posts;
     public GameObject[] upPosts;
    public GameOverScreen GameOverScreen;
    public NextLevelScreen NextLevelScreen;

    public GameObject narrationManager;
    public GameObject player;
    public int energy;
    public TMP_Text energyText;

    void Start()
    {
        
        energy = 100;
        posts = GameObject.FindGameObjectsWithTag("Post").OrderBy(go => go.transform.position.x).ToArray();
        upPosts = GameObject.FindGameObjectsWithTag("UpPost").OrderBy(go => go.transform.position.x).ToArray();
        InvokeRepeating("DecreaseEnergy", 0, 1.0f);
        TriggerNextNarration();
        player.SetActive(true);
    }

    void Update()
    {
         if (energy <= 0){GameOver();}
    }
    //game over event
    public void GameOver()
    {
        AudioManager.instance.PlayDeathSound();
        AudioManager.instance.StopBackgroundSounds();
        GameOverScreen.Setup();
        player.SetActive(false);
        CancelInvoke();
   }
   public void NextLevel()
    {
        NextLevelScreen.Setup();
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
    public void TriggerNextNarration(){
        narrationManager.GetComponent<NarrationManager>().ShowNextText();
    }
}
