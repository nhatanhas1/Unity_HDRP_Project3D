using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
using System.Drawing;
using static EnemyData;
using Photon.Pun;

public class EnemyController : MonoBehaviour , IIDamageable
{
    public EnemyData enemyData;

    public Transform target;

    NavMeshAgent navMeshAgent;

    public enum State
    {
        Roaming,
        ChaseTarget,
        AttackTarget,
        Runaway,
    }

    public State enemyState;

    public float range = 10.0f;
    public bool isTargeted;
    Vector3 point;
    Vector3 runAwayDir;

    bool switchState;



    bool isDead;
    public int hitPoint;
    public int currentHitPoint;

    public int armor;
    public int attackDamage;
    int finalDamageTake;

    public float moveSpeed;
    public float attackSpeed;
    public float attackTime;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {        
        if(!PhotonNetwork.IsMasterClient) { return; }
        SetUpStats();
        StartCoroutine(UpdateEnemyState());
    }

    void SetUpStats()
    {
        //enemyAnimator = GetComponentInChildren<Animator>();
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //target = FindObjectOfType<playerController>().transform;

        

        isDead = false;
        hitPoint = enemyData.hitPoint;
        armor = enemyData.armor;
        attackDamage = enemyData.attackDamage;
        attackSpeed = enemyData.attackSpeed;
        moveSpeed = enemyData.moveSpeed;
        navMeshAgent.speed = enemyData.moveSpeed;

        enemyState = State.Roaming;
        //enemyAnimator.runtimeAnimatorController = enemyData.runtimeAnimatorController;

        currentHitPoint = hitPoint;
    }

    public void AttackState()
    {
        
    }

    IEnumerator UpdateEnemyState()
    {
        while (!isDead)
        {
            if (navMeshAgent.isOnNavMesh)
            {
                if (target != null)
                {
                    enemyState = State.ChaseTarget;
                }
                else
                {

                    enemyState = State.Roaming;
                }
                EnemyState();
                //Debug.Log("Run");
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    void EnemyState()
    {
        switch (enemyState)
        {
            case State.Roaming:
                //Debug.Log("Roaming");
                //point = transform.position;
                MoveRandomPosition();

                break;
            case State.ChaseTarget:
                //switchState = true;
                //Debug.Log("Chasing");
                navMeshAgent.stoppingDistance = target.GetComponent<CapsuleCollider>().radius / 2;
                navMeshAgent.destination = target.position;
                //if (navMeshAgent.hasPath)
                //{
                //    //Debug.Log("tim duoc duong");
                //}
                break;

            case State.Runaway:
                //switchState = true;
                //Debug.Log("RunAway");
                runAwayDir = (target.transform.position - transform.position).normalized;
                MoveAway(transform.position - runAwayDir * 3);

                break;

        }
    }

    void MoveAway(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
        navMeshAgent.isStopped = false;
    }

    public void MoveToPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized;
        navMeshAgent.destination = transform.position + (randomDir) * Random.Range(1f, 3f);


    }
   
    public void MoveRandomPosition()
    {
        if (navMeshAgent.remainingDistance < 0.5f || switchState == true)
        {
            //Debug.Log("Da den noi");


            if (RandomPoint(transform.position, range, out point))
            {
                //Debug.Log("aasd");
                navMeshAgent.destination = point;
                switchState = false;
                //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            }
        }
        else
        {
            //Debug.Log("Dang tren duong di");
        }

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                //Debug.Log("tim thay");
                return true;

            }
        }
        result = Vector3.zero;
        //Debug.Log("Khong tim thay");
        return false;
    }



    //IEnumerator UpdatePath()
    //{
    //    while (target != null)
    //    {
    //        Debug.Log("Enemy UpdatePath");
    //        NavMeshHit hit;
    //        if (NavMesh.SamplePosition(Vector3.zero, out hit, 50.0f, NavMesh.AllAreas))
    //        {
    //            Vector3 result = hit.position;
    //            navMeshAgent.enabled = true;
    //        }
    //        else
    //        {
    //            navMeshAgent.enabled = false;
    //        }
    //        if (navMeshAgent.isOnNavMesh)
    //        {


    //            //checkOnMesh = true;
    //            navMeshAgent.stoppingDistance = target.GetComponent<CapsuleCollider>().radius / 2;
    //            navMeshAgent.destination = target.position;
    //            if (navMeshAgent.hasPath)
    //            {
    //                //Debug.Log("tim duoc duong");
    //            }
    //            else
    //            {

    //                //Debug.Log("khong tim duoc duong");
    //            }
    //        }
    //        //Debug.Log("Enemy Tim duong");

    //        yield return new WaitForSeconds(.5f);
    //    }

    //}

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            Debug.Log("Enemy Take Damage");
            finalDamageTake = damage > armor ? (damage - armor) : 0;
            currentHitPoint -= finalDamageTake;
            //PopupDamage();
            if (currentHitPoint <= 0)
            {
                Dead();
            }
        }

    }

    void Dead()
    {
        if (isDead) { return; }
        isDead = true;
        if (!PhotonNetwork.IsMasterClient) { return; }
        PhotonNetwork.Destroy(gameObject);
        Debug.Log("Dead");
    }

    void Attack(IIDamageable target)
    {
        if (Time.time > attackTime)
        {
            attackTime = Time.time + attackSpeed;
            target.TakeDamage(attackDamage);
            //Debug.Log("Enemy Attack");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IIDamageable>() != null)
        {
            IIDamageable attackTarget = other.GetComponent<IIDamageable>();
            //GameObject attackTarget1 = other.gameObject;
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Attack(attackTarget);
            }

        }
    }
}
