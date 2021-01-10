using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 50)]
    public float speed = 3;
    [Header("停止距離"), Range(0, 50)]
    public float stopDistance = 2.5f;
    [Header("攻擊冷卻時間"), Range(0, 50)]
    public float CD = 2.3f;
    [Header("攻擊中心點")]
    public Transform ATKPoint;
    [Header("攻擊長度"),Range(0f,5f)]
    public float ATKLength;

    private Transform player;
    private NavMeshAgent nav;
    private Animator ani;
    private float timer;

    private void Awake()
    {
        //取得代理器(Agent)的資訊
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        //尋找物件(小明)的"位置"資訊
        player = GameObject.Find("小明").transform;
        nav.speed = speed;
        nav.stoppingDistance = stopDistance;
    }

    private void Update()
    {
        Track();
        Attack();
    }

    /// <summary>
    /// 繪製圖示事件:僅在Unity中顯示
    /// </summary>
    private void OnDrawGizmos()
    {
        //圖示.顏色=青色
        Gizmos.color = Color.cyan;
        //圖示.繪製射線(中心點，方向)
        //(攻擊中心點的座標.攻擊中心點的前方*攻擊長度)
        Gizmos.DrawRay(ATKPoint.position, ATKPoint.forward * ATKLength);
    }

    private RaycastHit hit;
    
    
    /// <summary>
    /// 怪物攻擊角色
    /// </summary>
    private void Attack()
    {
        //如果代理器跟角色的距離小於停止距離，則
        if(nav.remainingDistance < stopDistance)
        {
            //計時器 累加 一禎的時間
            timer += Time.deltaTime;

            //取得玩家座標_三軸座標的位置=角色位置
            Vector3 pos = player.position;
            //將玩家的座標Y軸指定為本物件的Y軸_本物件的y軸 = 角色y軸
            pos.y = transform.position.y;
            //本物件看向角色的座標
            transform.LookAt(pos);

            if(timer >= CD)
            {
                ani.SetTrigger("攻擊觸發");
                timer = 0;

                //物理.射線碰撞(攻擊中心點的座標.攻擊中心點的前方,射線擊中的物件,攻擊長度)
                //圖層:1<<圖層編號 = 只對8號圖層作用
                //放在hit前的藍色out = 將資訊儲存於"hit"物件
                if (Physics.Raycast(ATKPoint.position, ATKPoint.forward, out hit ,ATKLength, 1 << 8))
                {
                    hit.collider.GetComponent<Player>().Damage();
                }
            }
        }
    }

    /// <summary>
    /// 怪物追角色
    /// </summary>
    private void Track()
    {
        //為代理器設定目的地(player的位置)
        nav.SetDestination(player.position);
        ani.SetBool("走路開關", nav.remainingDistance > stopDistance);
    }
}
