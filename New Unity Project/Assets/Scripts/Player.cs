using UnityEngine;
using Invector.vCharacterController; //引用套件 Invector.(腳本名稱)

public class Player : MonoBehaviour
{
    private float hp = 100;  //將hp設定為100
    private Animator ani;
   
    /// <summary>
    /// 攻擊次數
    /// </summary>
    private int atkCount;

    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;

    [Header("連擊間隔時間"), Range(0, 3)]
    public float interval = 1;
    [Header("攻擊中心點")]
    public Transform ATKPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float ATKLength;
    [Header("攻擊力"), Range(0, 500)]
    public float atk = 30;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    /// <summary>
    /// 繪製圖示事件:僅在Unity中顯示
    /// </summary>
    private void OnDrawGizmos()
    {
        //圖示.顏色=紅色
        Gizmos.color = Color.red;
        //圖示.繪製射線(中心點，方向)
        //(攻擊中心點的座標.攻擊中心點的前方*攻擊長度)
        Gizmos.DrawRay(ATKPoint.position, ATKPoint.forward * ATKLength);
    }

    private RaycastHit hit;

    private void Attack()
    {

        if (atkCount <3 )//如果連擊次數小於3才可以執行下列程式
        {
            if (timer < interval)//如果時間小於連擊間隔時間
            {
                timer += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Mouse0))//如果點一下左鍵
                {
                    atkCount++;//attack數字增加1
                    timer = 0;

                    if (Physics.Raycast(ATKPoint.position, ATKPoint.forward, out hit, ATKLength, 1 << 9))
                    {
                        hit.collider.GetComponent<Enemy>().Damage(atk);
                    }
                }
            }
            else
            {
                atkCount = 0;
                timer = 0;
            }
            
        }

        if (atkCount == 3) atkCount = 0;//如果連擊次數等於3就歸零
        ani.SetInteger("attack", atkCount);
        
    }

    public void Damage(float damage)
    {
        hp -= damage;  //每一次攻擊損失35點hp
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
