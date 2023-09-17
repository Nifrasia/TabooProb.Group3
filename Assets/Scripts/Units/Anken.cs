using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public enum Gender
{
    male,
    female
}

public class Anken : Unit
{

    private int id;
    public int ID { get { return id; } set { id = value; } }
    [SerializeField] private int charSkinID;
    public int CharSkinID { get { return charSkinID; } set { charSkinID = value; } }
    public GameObject[] charSkin;

    [SerializeField]private float sp = 200;
    public float SP { get { return sp; } set { sp = value; } }

    [SerializeField] private float hunger = 220;
    public float Hunger { get {  return hunger; } set {  hunger = value; } }


    [SerializeField] private int charFaceID;
    public int CharFaceID { get { return charFaceID; } set { charFaceID = value; } }
    public Sprite[] charFacePic;


    [SerializeField] private string staffName;
    public string StaffName { get { return staffName; } set { staffName = value; } }

    [SerializeField] private Gender staffGender = Gender.male;
    public Gender StaffGender { get { return staffGender; } set { staffGender = value; } }

    [SerializeField] private bool hired = false;
    public bool Hired { get { return hired; } set { hired = value; } }




    [SerializeField] private GameObject targetForest;
    public GameObject TargetForest { get { return targetForest; } set { targetForest = value; } }

    private int maxAmount = 30;

    [SerializeField] private int curAmount;
    public int CurAmount { get { return curAmount; } set { curAmount = value; } }

    //Miner State Timer
    [SerializeField] private float cuttingTimer = 0f;
    [SerializeField] private float cuttingTimeWait = 1f;

    //Miner Dig Timer
    [SerializeField] private float timeLastDig;
    [SerializeField] private float digRate = 3f;



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



    protected override void Update()
    {
        base.Update();

        cuttingTimer += Time.deltaTime;
        if (cuttingTimer >= cuttingTimeWait)
        {
            cuttingTimer = 0f;
            CheckWorkerState();
        }
    }


    private void CheckWorkerState()
    {
        switch (state)
        {
            case UnitState.MovetoCutTree:
                MoveToCutTree();
                break;
            case UnitState.CutTree:
                CuttinUpdate();
                break;
            case UnitState.MovetoDeliver:
                MoveToDeliverUpdate();
                break;
            case UnitState.Deliver:
                DeliverUpdate();
                break;
        }
    }


    #region Mining
    public void StartCutting(GameObject mine)
    {
        if (mine == null)
        {
            targetForest = null;
            SetUnitState(UnitState.MovetoDeliver);
            navAgent.SetDestination(TargetStructure.transform.position);
        }
        else
        {
            SetUnitState (UnitState.MovetoCutTree);
            navAgent.SetDestination(mine.transform.position);
        }
        navAgent.isStopped = false;
    }


    void MoveToCutTree()
    {
        if (targetForest == null)
        {
            GameObject newForest = FindingTarget.CheckForNearestForest(targetStructure.transform.position,
                                                                        10000f,
                                                                        "Forest");
            StartCutting(newForest);
        }

        //DisableAllTools();
        //Equip PickAxe

        if (Vector3.Distance(transform.position, navAgent.destination) <= 1f)
        {
            LookAt(navAgent.destination);
            SetUnitState(UnitState.CutTree);
        }
    }

    void CuttinUpdate()
    {
        Forest forest;
        if (targetForest != null)
            forest = targetForest.GetComponent<Forest>();
        else
        {
            GameObject newMine = FindingTarget.CheckForNearestForest(targetStructure.transform.position,
                                                                        10000f,
                                                                        "Forest");
            targetForest = newMine;
            StartCutting(newMine);
            return;
        }

        //DisableAllTools();
        //Equip PickAxe

        if (Time.time - timeLastDig > digRate)
        {
            timeLastDig = Time.time;

            if (curAmount < maxAmount)
            {
                curAmount += 3;

                if (curAmount > maxAmount)
                    curAmount = maxAmount;
            }
            else //Move to deliver at HQ
            {
                SetUnitState(UnitState.MovetoDeliver);
                navAgent.SetDestination(targetStructure.transform.position);
                navAgent.isStopped = false;
            }
        }
    }

    private void MoveToDeliverUpdate()
    {
        if (targetStructure == null)
        {
            SetUnitState(UnitState.Idle);
            return;
        }

        //DisableAllTools();
        //Equip Load

        if (Vector3.Distance(transform.position, targetStructure.transform.position) <= 5f)
        {
            SetUnitState(UnitState.Deliver);
            navAgent.isStopped = true;
        }
    }

    private void DeliverUpdate()
    {
        //This unit stops when there is no target resource to go back and he has nothing to deliver
        if (targetStructure == null)
        {
            SetUnitState(UnitState.Idle);
            return;
        }

        // Deliver the resource to player
        Office.instance.Stone += curAmount;
        curAmount = 0;

        // Go back to mining
        if (targetForest != null)
        {
            StartCutting(targetForest);
        }
        else
        {
            GameObject newForest = FindingTarget.CheckForNearestForest(targetStructure.transform.position,
                                                                    10000f,
                                                                    "Mine");
            if (newForest != null)
                StartCutting(newForest);
            else
            {
                targetStructure = null;
                SetUnitState(UnitState.Idle);
                navAgent.isStopped = true;
            }
        }
    }



    #endregion
}
