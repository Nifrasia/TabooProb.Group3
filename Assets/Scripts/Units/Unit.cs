using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public enum UnitState
{
    Idle,
    Walk,
    Death,
    Attack,
    MovetoDeliver,
    Deliver,
    MovetoCutTree,
    CutTree

}

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float hp = 100;
    public float
        HP { get { return hp; } set { hp = value; } }

    [SerializeField] protected UnitState state;
    public UnitState State { get { return state; } set { state = value; } }

    protected NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get { return navAgent; } set { navAgent = value; } }

    private float distance;

    [SerializeField] protected GameObject targetStructure;
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


    public UnityEvent<UnitState> onStateChange;


    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {

    }

    protected virtual void Update()
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

    //protected void DisableWeapon()
    //{
    //    weapon.SetActive(false);
    //}

    //protected void EquipWeapon()
    //{
    //    weapon.SetActive(true);
    //}


    public void SetUnitState(UnitState s)
    {
        if (onStateChange != null) //if there is an icon
            onStateChange.Invoke(s);

        state = s;
    }


    protected void LookAt(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle, 0);
    }


}
