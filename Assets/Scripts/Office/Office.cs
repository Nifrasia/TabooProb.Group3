using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : MonoBehaviour
{
    [SerializeField] private int money;
    public int Money { get { return money; } set { money = value; } }

    [SerializeField] private List<Anken> ankens = new List<Anken>();
    public List<Anken> Ankens { get { return ankens; } }

    //[SerializeField] private int wheat;
    //public int Wheat { get { return wheat; } set { wheat = value; } }
    [SerializeField] private int fish;
    public int Fish { get { return fish; } set { fish = value; } }

    [SerializeField] private int wood;
    public int Wood { get { return wood; } set { wood = value; } }

    [SerializeField] private int stone;
    public int Stone { get { return stone; } set { stone = value; } }

    [SerializeField] private int availAnken;
    public int AvailStaff { get { return availAnken; } set { availAnken = value; } }

    [SerializeField] private List<Structure> structures = new List<Structure>();
    public List<Structure> Structures { get { return structures; } }

    [SerializeField] private int dailyCostFish;

    [SerializeField] private GameObject staffParent;
    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private GameObject rallyPosition;

    [Header("Building")]
    [SerializeField] private int unitLimit = 4; //Initial unit limit
    public int UnitLimit { get { return unitLimit; } }

    public static Office instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

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
        //if (money <= 0)
        //    return false;

        if (ankens.Count >= unitLimit)
        {
            return false;
        }

        workerObj.transform.parent = staffParent.transform;

        Anken w = workerObj.GetComponent<Anken>();

        w.Hired = true; //Hire this worker
        w.ChangeCharSkin(); //Show 3D model
        w.SetToWalk(rallyPosition.transform.position);

        //money -= w.DailyWage;
        AddStaff(w);

        //Update UI
        MainUI.instance.UpdateResourceUI();

        return true;
    }
    public void AddStaff(Anken w)
    {
        ankens.Add(w);
        dailyCostFish += w.DialyFish;
    }
    public void RemoveStaff(Anken w)
    {
        ankens.Remove(w);
        dailyCostFish -= w.DialyFish;
    }


    public void CheckHousing()
    {
        unitLimit = 4; //starting unit Limit

        foreach (Structure s in structures)
        {
            if (s.IsHousing && s.IsHousing)
                unitLimit += s.HouseUnit;
        }

        if (unitLimit >= 100)
            unitLimit = 100;
        else if (unitLimit < 0)
            unitLimit = 0;
    }
    public void UpdateAvailStaff()
    {
        availAnken = 0;

        foreach (Anken w in ankens)
        {
            if (w.TargetStructure == null) //there is no job to do
                availAnken++;
        }
    }
    public void SendStaffToFishingPound(GameObject target)
    {
        Fishingpond f = target.GetComponent<Fishingpond>();

        int staffNeed = f.MaxStaffNum - f.CurrentWorkers.Count;
        if (staffNeed <= 0)
            return;

        UpdateAvailStaff();

        if (staffNeed > availAnken)
            staffNeed = availAnken;

        int n = 0; //number of Staff sent

        for (int i = 0; i < ankens.Count; i++)
        {
            if (ankens[i].TargetStructure == null)
            {
                Anken w = ankens[i].GetComponent<Anken>();
                ankens[i].TargetStructure = target;
                Debug.Log(target);
                Debug.Log(target.transform.position);
                ankens[i].SetToWalk(target.transform.position);
                f.AddStaff(w);
                n++;

            }
           
            if (n >= staffNeed)
                break;
         }

        UpdateAvailStaff();
    }
    public void SendStaffToMine(GameObject target)
    {
        Mine f = target.GetComponent<Mine>();

        int staffNeed = f.MaxStaffNum - f.CurrentWorkers.Count;
        if (staffNeed <= 0)
            return;

        UpdateAvailStaff();

        if (staffNeed > availAnken)
            staffNeed = availAnken;

        int n = 0; //number of Staff sent

        for (int i = 0; i < ankens.Count; i++)
        {
            if (ankens[i].TargetStructure == null)
            {
                Anken w = ankens[i].GetComponent<Anken>();
                ankens[i].TargetStructure = target;
                Debug.Log(target);
                Debug.Log(target.transform.position);
                ankens[i].SetToWalk(target.transform.position);
                f.AddStaff(w);
                n++;

            }

            if (n >= staffNeed)
                break;
        }

        UpdateAvailStaff();
    }
    public void SendStaffToForest(GameObject forest, GameObject warehouse)
    {
        UpdateAvailStaff();

        if (forest == null || availAnken <= 0)
            return;

        int n = 0; //number of Worker sent

        for (int i = 0; i < ankens.Count; i++)
        {
            if (ankens[i].TargetStructure == null)
            {
                Anken w = ankens[i].GetComponent<Anken>();

                ankens[i].TargetStructure = warehouse;
                ankens[i].TargetForest = forest;
                w.StartCutting(forest);
                n++;
            }

            if (n >= 1)
                break;
        }

        UpdateAvailStaff();
    }
    public void SendStaffToCutTree(GameObject target)
    {
        LoggerCamp f = target.GetComponent<LoggerCamp>();

        int staffNeed = f.MaxStaffNum - f.CurrentWorkers.Count;
        if (staffNeed <= 0)
            return;

        UpdateAvailStaff();

        if (staffNeed > availAnken)
            staffNeed = availAnken;

        int n = 0; //number of Staff sent

        for (int i = 0; i < ankens.Count; i++)
        {
            if (ankens[i].TargetStructure == null)
            {
                Anken w = ankens[i].GetComponent<Anken>();
                ankens[i].TargetStructure = target;
                ankens[i].SetToWalk(target.transform.position);
                f.AddStaff(w);
                n++;

            }

            if (n >= staffNeed)
                break;
        }

        UpdateAvailStaff();
    }

    public void DialyCostFishUpdate()
    {
        if(fish > dailyCostFish) {
            fish -= dailyCostFish;
        }
        else
        {
            fish = 0;
        }
        
    }

    //public bool ToFireStaff(GameObject staffObj)
    //{
    //    staffObj.transform.parent = LaborMarket.instance.WorkerParent.transform;
    //    //move Staff obj back to Labor Market

        


    //    Anken w = staffObj.GetComponent<Anken>();
    //    w.Hired = false; //Fire this staff

    //    if (w.TargetStructure != null)
    //    {
    //        Farm f = w.TargetStructure.GetComponent<Farm>();
    //        if (f != null)
    //            f.CurrentWorkers.Remove(w); //Remove from this farm
    //    }

    //    w.TargetStructure = null; //Quit working
    //    //w.DisableAllTools();
    //    w.SetToWalk(spawnPosition.transform.position);

    //    RemoveStaff(w);
    //    MainUI.instance.UpdateResourceUI();

    //    return true;
    //}

}