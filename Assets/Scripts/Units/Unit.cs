//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public enum UnitState
//{
//    Idle,
//    Walk,
//    Plow,
//    Sow,
//    Water,
//    Harvest,
//    MoveToAttackUnit,
//    AttackUnit,
//    MoveToAttackBuilding,
//    AttackBuilding
//}

//public abstract class Unit : MonoBehaviour
//{
//    [SerializeField] protected int hp = 100;
//    public int HP { get { return hp; } set { hp = value; } }

//    [SerializeField] protected UnitState state; 
//    public UnitState State { get { return state; } set { state = value; } }

//    protected NavMeshAgent navAgent;
//    public NavMeshAgent NavAgent { get { return navAgent; } set { navAgent = value; } }

//    protected float distance;

//    [SerializeField] protected GameObject targetStructure;
//    public GameObject TargetStructure { get { return targetStructure; } set { targetStructure = value; } }

//    [SerializeField] protected float detectRange = 50f;
//    public float DetectRange { get { return detectRange; } set { detectRange = value; } }

//    [SerializeField] protected float attackRange = 2f;
//    public float AttackRange { get { return attackRange; } set { attackRange = value; } }

//    [SerializeField] protected int attackPower = 5;
//    public int AttackPower { get { return attackPower; } set { attackPower = value; } }

//    //Timer
//    [SerializeField] protected float CheckStateTimer = 0f;
//    [SerializeField] protected float CheckStateTimeWait = 0.5f;

//    [SerializeField] protected GameObject[] tools;

//    void Awake()
//    {
//        navAgent = GetComponent<NavMeshAgent>();
//    }

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        CheckStaffState();
//    }

//    protected void CheckStaffState()
//    {
//        CheckStateTimer += Time.deltaTime;

//        if (CheckStateTimer >= CheckStateTimeWait)
//        {
//            CheckStateTimer = 0;
//            SwitchStaffState();
//        }
//    }

//    protected void SwitchStaffState()
//    {
//        switch (state)
//        {
//            case UnitState.Walk:
//                WalkUpdate();
//                break;
//            case UnitState.MoveToAttackBuilding:
//                MoveToAttackBuilding();
//                break;
//            //case UnitState.AttackBuilding:
//            //    AttackBuilding();
//            //    break;
//        }
//    }

//    protected void WalkUpdate()
//    {
//        distance = Vector3.Distance(navAgent.destination, transform.position);

//        if (distance <= 3f)
//        {
//            navAgent.isStopped = true;
//            state = UnitState.Idle;
//        }
//    }

//    public void SetToWalk(Vector3 dest)
//    {
//        state = UnitState.Walk;

//        navAgent.SetDestination(dest);
//        navAgent.isStopped = false;
//    }

//    protected void MoveToAttackBuilding()
//    {
//        if (targetStructure == null)
//        {
//            state = UnitState.Idle;
//            navAgent.isStopped = true;
//            return;
//        }
//        else
//        {
//            navAgent.SetDestination(targetStructure.transform.position);
//            navAgent.isStopped = false;
//        }

//        distance = Vector3.Distance(transform.position, targetStructure.transform.position);

//        if (distance <= attackRange)
//            state = UnitState.AttackBuilding;
//    }

//    //protected void AttackBuilding()
//    //{
//    //    navAgent.isStopped = true;

//    //    if (targetStructure != null)
//    //    {
//    //        Building b = targetStructure.GetComponent<Building>();

//    //        b.HP -= (int)attackPower;
//    //    }
//    //}
//}
