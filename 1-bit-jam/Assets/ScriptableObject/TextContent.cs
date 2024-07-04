using UnityEngine;

[CreateAssetMenu(fileName = "NewTextContent", menuName = "TextContent")]
public class TextContent : ScriptableObject
{
    [TextArea(10, 50)]
    public string content;
}
