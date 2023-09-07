using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : MonoBehaviour
{

    [SerializeField] private int money;
    public int Money { get { return money; } set { money = value; } }

    [SerializeField] private List<Anken> ankens = new List<Anken>();
    public List<Anken> Ankens { get { return ankens; } }

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    
}