using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC資料")]
    public NPCData data;
    [Header("對話框")]
    public GameObject dialogue;
    [Header("對話內容")]
    public Text textContent;
    [Header("對話者名稱")]
    public Text textName;
    [Header("對話間隔")]
    public float interval = 0.1f;

    public bool playerInArea;

    //自定義列舉
    public enum NPCState
    {
        FirstDialogue,Progressing,Finish //會有這三種狀態
    }

    //public 列舉之名稱 state = 列舉之名稱.列舉之狀態
    //預設列舉狀態為
    public NPCState state = NPCState.FirstDialogue;

    /* 協同程序
    private void Start()
    {
        //啟動協程
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        print("first_second");
        yield return new WaitForSeconds(1f);
        print("second_second");
        yield return new WaitForSeconds(2f);
        print("forth_second");
    }
    */

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "小明")
        {
            playerInArea = true;
            StartCoroutine (Dialogue());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "小明")
        {
            playerInArea = false;
            StopDialogue();
        }
    }

    /// <summary>
    /// 以下是"停止對話"
    /// </summary>
    private void StopDialogue()
    {
        dialogue.SetActive(false);  //關閉對話框
        StopAllCoroutines();  //停止所有協程
    }
    private IEnumerator Dialogue()  //對話協程
    {
        dialogue.SetActive(true); //出現對話框
        textContent.text = ""; //清空對話框

        textName.text = name;

        string dialogueString = data.dialogueB;

        switch (state)
        {
            case NPCState.FirstDialogue:
                dialogueString = data.dialogueA;
                break;
            case NPCState.Progressing:
                dialogueString = data.dialogueB;
                break;
            case NPCState.Finish:
                dialogueString = data.dialogueC;
                break;
            default:
                break;
        }

        for (int n = 0; n < dialogueString.Length; n++)
        {
            //print(data.dialogueA[n]);
            textContent.text += dialogueString[n] + "";
            yield return new WaitForSeconds(interval);

        }    
    }

}
