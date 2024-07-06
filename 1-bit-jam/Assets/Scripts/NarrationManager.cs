using UnityEngine;
using TMPro; // Make sure to use TMPro if you are using TextMesh Pro

public class NarrationManager : MonoBehaviour
{
    public TextContent[] textContents;
    public GameObject textBoxPrefab;
    public Transform canvasTransform;

    private int currentIndex = 0;

    void Start()
    {
        ShowNextText();
    }

    public void ShowNextText()
    {
        if (currentIndex < textContents.Length)
        {
            GameObject textBox = Instantiate(textBoxPrefab, canvasTransform);
            textBox.GetComponentInChildren<TypewriterEffect>().fullText = textContents[currentIndex].content;
            currentIndex++;
        }
    }
}