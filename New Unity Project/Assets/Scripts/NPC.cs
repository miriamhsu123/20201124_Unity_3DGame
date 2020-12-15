using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [Header("NPC資料")]
    public NPCData data;
    [Header("對話框")]
    public GameObject dialogue;
    [Header("對話內容")]
    public Text textContent;

    public bool playerInArea;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "小明")
        {
            playerInArea = true;
            Dialogue();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "小明")
        {
            playerInArea = false;
        }
    }
    private void Dialogue()
    {
        for (int n = 0; n < data.dialogueA.Length; n++)
        {
            print(data.dialogueA[n]);
        }    
    }

}
