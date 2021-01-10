using UnityEngine;
using Invector.vCharacterController; //引用套件 Invector.(腳本名稱)

public class Player : MonoBehaviour
{
    private float hp = 100;  //將hp設定為100
    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    public void Damage()
    {
        hp -= 35;  //每一次攻擊損失35點hp
        ani.SetTrigger("受傷觸發");  //受傷時播放"受傷觸發"動畫

        //如果hp小於等於零，則執行Dead事件
        if (hp <= 0)
        {
            Dead();
        }
    }

    //創造名叫Dead的事件，內容為觸發"死亡觸發"動畫
    private void Dead()
    {
        ani.SetTrigger("死亡觸發");

        vThirdPersonController vt = GetComponent<vThirdPersonController>();
        vt.lockMovement = true;
        vt.lockRotation = true;
    }
}
