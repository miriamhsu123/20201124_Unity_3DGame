using UnityEngine;

[CreateAssetMenu(fileName = "NPC 資料", menuName = "mo/NPC 資料")]
public class NPCData : ScriptableObject
{
    [Header("第一段對話"),TextArea(1,5)]
    public string dialogueA;
    [Header("第二段對話"), TextArea(1, 5)]
    public string dialogueB;
    [Header("第三段對話"), TextArea(1, 5)]
    public string dialogueC;
    [Header("任務項目需求數量")]
    public int count;
    [Header("第一段對話")]
    public int countCurrent;
}
