using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    [SerializeField] private int money;
    public int Money { get { return money; } set { money = value; } }

    [SerializeField] private List<Worker> workers = new List<Worker>();
    public List<Worker> Workers { get { return workers; } }

    [SerializeField] private int wheat;
    public int Wheat { get { return wheat; } set { wheat = value; } }

    [SerializeField] private int melon;
    public int Melon { get { return melon; } set { melon = value; } }

    [SerializeField] private int corn;
    public int Corn { get { return corn; } set { corn = value; } }

    [SerializeField] private int milk;
    public int Milk { get { return milk; } set { milk = value; } }

    [SerializeField] private int apple;
    public int Apple { get { return apple; } set { apple = value; } }

    [SerializeField] private int stone;
    public int Stone { get { return stone; } set { stone = value; } }

    [SerializeField] private int dailyCostWages;

    [SerializeField] private List<Structure> structures = new List<Structure>();
    public List<Structure> Structures { get { return structures; } }


    [SerializeField] private int availStaff;
    public int AvailStaff { get { return availStaff; } set { availStaff = value; } }

    [SerializeField] private GameObject staffParent;

    [SerializeField] private GameObject spawnPosition;
    public GameObject SpawnPosition { get { return spawnPosition; } }
    [SerializeField] private GameObject rallyPosition;

    [Header("Building")]
    [SerializeField] private int unitLimit = 3; //Initial unit limit
    public int UnitLimit { get { return unitLimit; } }

    [SerializeField] private int housingUnitNum = 6; //number of units per each housing

    public static Office instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddBuilding(Structure s)
    {
        structures.Add(s);
        CheckHousing();
    }

    public void RemoveBuilding(Structure s)
    {
        structures.Remove(s);
        Destroy(s.gameObject);
        CheckHousing();
    }

    public bool ToHireStaff(GameObject workerObj)
    {
        if (money <= 0)
            return false;

        if (workers.Count >= unitLimit)
            return false;

        workerObj.transform.parent = staffParent.transform;
        Worker w = workerObj.GetComponent<Worker>();

        w.Hired = true; //Hire this worker
        w.SetToWalk(rallyPosition.transform.position);

        money -= w.DailyWage;
        AddStaff(w);

        //Update UI
        MainUI.instance.UpdateResourceUI();

        return true;
    }

    public void AddStaff(Worker w)
    {
        workers.Add(w);
        dailyCostWages += w.DailyWage;
    }
    public void UpdateAvailStaff()
    {
        availStaff = 0;

        foreach (Worker w in workers)
        {
            if (w.TargetStructure == null) //there is no job to do
                availStaff++;
        }
    }
    public void SendStaff(GameObject target)
    {
        Farm f = target.GetComponent<Farm>();

        int staffNeed = f.MaxStaffNum - f.CurrentWorkers.Count;
        if (staffNeed <= 0)
            return;

        UpdateAvailStaff();

        if (staffNeed > availStaff)
            staffNeed = availStaff;

        int n = 0; //number of Staff sent

        for (int i = 0; i < workers.Count; i++)
        {
            if (workers[i].TargetStructure == null)
            {
                Worker w = workers[i].GetComponent<Worker>();

                workers[i].TargetStructure = target;
                workers[i].SetToWalk(target.transform.position);
                f.AddStaffToFarm(w);
                n++;
            }

            if (n >= staffNeed)
                break;
        }

        UpdateAvailStaff();
    }
    public void FireStaff(Worker w)
    {
        workers.Remove(w);
        dailyCostWages -= w.DailyWage;
    }
    public bool ToFireStaff(GameObject staffObj)
    {
        staffObj.transform.parent = LaborMarket.instance.WorkerParent.transform;
        //move Staff obj back to Labor Market

        Worker w = staffObj.GetComponent<Worker>();

        w.Hired = false; //Fire this staff

        if (w.TargetStructure != null)
        {
            Farm f = w.TargetStructure.GetComponent<Farm>();
            if (f != null)
                f.CurrentWorkers.Remove(w); //Remove from this farm
        }

        w.TargetStructure = null; //Quit working
        w.SetToWalk(spawnPosition.transform.position);

        FireStaff(w);
        MainUI.instance.UpdateResourceUI();

        return true;
    }
    public void CheckHousing()
    {
        unitLimit = 3; //starting unit Limit

        foreach (Structure s in structures)
        {
            if (s.IsHousing && s.IsHousing)
                unitLimit += housingUnitNum;
        }

        if (unitLimit >= 100)
            unitLimit = 100;
        else if (unitLimit < 0)
            unitLimit = 0;
    }
    public void SendWorkerToMine(GameObject mine, GameObject warehouse)
    {
        UpdateAvailStaff();

        if (mine == null || availStaff <= 0)
            return;

        int n = 0; //number of Worker sent

        for (int i = 0; i < workers.Count; i++)
        {
            if (workers[i].TargetStructure == null)
            {
                Worker w = workers[i].GetComponent<Worker>();

                workers[i].TargetStructure = warehouse;
                workers[i].TargetMine = mine;
                w.StartMining(mine);
                n++;
            }

            if (n >= 1)
                break;
        }

        UpdateAvailStaff();
    }
}