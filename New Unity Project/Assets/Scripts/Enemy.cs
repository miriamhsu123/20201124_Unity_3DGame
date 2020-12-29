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
