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

public enum Gender
{
    male,
    female
}

public class Anken : MonoBehaviour
{
    private int id;
    public int ID { get { return id; } set { id = value; } }
    [SerializeField] private int charSkinID;
    public int CharSkinID { get { return charSkinID; } set { charSkinID = value; } }
    public GameObject[] charSkin;


    [SerializeField] private int charFaceID;
    public int CharFaceID { get { return charFaceID; } set { charFaceID = value; } }
    public Sprite[] charFacePic;


    [SerializeField] private string staffName;
    public string StaffName { get { return staffName; } set { staffName = value; } }

    //[SerializeField] private int dailyWage;
    //public int DailyWage { get { return dailyWage; } set { dailyWage = value; } }

    [SerializeField] private Gender staffGender = Gender.male;
    public Gender StaffGender { get { return staffGender; } set { staffGender = value; } }

    [SerializeField] private bool hired = false;
    public bool Hired { get { return hired; } set { hired = value; } }

    [SerializeField] private UnitState state;
    public UnitState State { get { return state; } set { state = value; } }

    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get { return navAgent; } set { navAgent = value; } }


    private float distance;

    [SerializeField] private GameObject targetStructure;
    public GameObject TargetStructure { get { return targetStructure; } set { targetStructure = value; } }

    //Timer
    private float CheckStateTimer = 0f;
    private float CheckStateTimeWait = 0.5f;


    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
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

        if (distance <= 3f)
        {
            navAgent.isStopped = true;
            state = UnitState.Idle;
        }
    }

    public void SetToWalk(Vector3 dest)
    {
        state = UnitState.Walk;

        navAgent.SetDestination(dest);
        navAgent.isStopped = false;
    }


    public void InitiateCharID(int i)
    {
        charSkinID = i;
        charFaceID = i;
    }

    public void SetGender()
    {
        if (charSkinID == 1 || charSkinID == 4)
        {
            staffGender = Gender.female;
        }
    }
    public void ChangeCharSkin()
    {
        for (int i = 0; i < charSkin.Length; i++)
        {
            if (i == charSkinID)
            {
                charSkin[i].SetActive(true);
            }
            else
            {
                charSkin[i].SetActive(false);
            }
        }

    }
}
