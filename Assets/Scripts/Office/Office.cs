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
    public int Wood { get {  return wood; } set {  wood = value; } }

    [SerializeField] private int stone;
    public int Stone { get {  return stone; } set { stone = value; } }



    [SerializeField] private int availAnken;
    public int AvailStaff { get { return availAnken; } set { availAnken = value; } }

    [SerializeField] private List<Structure> structures = new List<Structure>();
    public List<Structure> Structures { get { return structures; } }


    [SerializeField] private int dailyCostWages;

    [SerializeField] private GameObject staffParent;
    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private GameObject rallyPosition;



    [Header("Building")]
    [SerializeField] private int unitLimit = 3; //Initial unit limit
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

        if(ankens.Count >= unitLimit)
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
        //dailyCostWages += w.DailyWage;
    }

    public void CheckHousing()
    {
        unitLimit = 3; //starting unit Limit

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


}