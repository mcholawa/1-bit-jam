using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public float delay;
    public string fullText = "";  // Set your desired text here
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
            // Debug.Log("Current text: " + currentText);
            yield return new WaitForSeconds(delay);
        }
     StartCoroutine(FadeOutText());
    }

    IEnumerator FadeOutText()
    {
        float startAlpha = textComponent.color.a;
        float rate = 1.0f / 1f;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            Color tmpColor = textComponent.color;
            tmpColor.a = Mathf.Lerp(startAlpha, 0, progress);
            textComponent.color = tmpColor;
            progress += rate * Time.deltaTime;

            yield return null;
        }

        // Ensure the text is fully transparent
        Color finalColor = textComponent.color;
        finalColor.a = 0;
        textComponent.color = finalColor;

         // Destroy the parent GameObject after fade-out
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}