using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.3f;
    public string fullText = "rabarbar";  // Set your desired text here
    private string currentText = "";
    private TMP_Text textComponent;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        Debug.Log("Start method executed. Text component found: " + (textComponent != null));
        Debug.Log("Full text: " + fullText);
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            Debug.Log("Current text: " + currentText);
            yield return new WaitForSeconds(delay);
        }
    }
}