using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum UnitState
{
    Idle,
    Walk,
    Death,
    Attack,
}

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float hp = 100;
    public float
        HP { get { return hp; } set { hp = value; } }

    [SerializeField] private UnitState state;
    public UnitState State { get { return state; } set { state = value; } }

    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get { return navAgent; } set { navAgent = value; } }

    private float distance;

    [SerializeField] private GameObject targetStructure;
    public GameObject TargetStructure { get { return targetStructure; } set { targetStructure = value; } }

    [SerializeField] protected float detectRange = 50f;
    public float DetectRange { get { return detectRange; } set { detectRange = value; } }

    [SerializeField] protected float attackRange = 2f;
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }

    [SerializeField] protected int attackPower = 5;
    public int AttackPower { get { return attackPower; } set { attackPower = value; } }

    [SerializeField] protected float CheckStateTimer = 0f;
    [SerializeField] protected float CheckStateTimeWait = 0.5f;

    [SerializeField] protected GameObject[] tools;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {

    }

    void Update()
    {
        CheckStaffState();
    }

    private void CheckStaffState()
    {
        CheckStateTimer += Time.deltaTime;

        if (CheckStateTimer >= CheckStateTimeWait)
        {
            CheckStateTimer = 0;
            SwitchStaffState();
        }
    }

    private void SwitchStaffState()
    {
        switch (state)
        {
            case UnitState.Walk:
                WalkUpdate();
                break;
        }
    }

    private void WalkUpdate()
    {
        distance = Vector3.Distance(navAgent.destination, transform.position);
        Debug.Log(distance);
        if (distance <= 3f)
        {
            Debug.Log("enter disctance <= 3f");
            navAgent.isStopped = true;
            state = UnitState.Idle;
        }
    }

    public void SetToWalk(Vector3 dest)
    {
        Debug.Log(dest);
        state = UnitState.Walk;
        navAgent.SetDestination(dest);
        navAgent.isStopped = false;
    }


}
