using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void Setup(){
        gameObject.SetActive(true);
    }
    public void RestartButton(){
        SceneManager.LoadScene("Level1");
    }
    public void NextLevelButton(){
        SceneManager.LoadScene("Level2");
    }
    public void ExitButton(){
        SceneManager.LoadScene("MainMenuScene");
    }
}
