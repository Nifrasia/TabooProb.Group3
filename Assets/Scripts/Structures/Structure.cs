using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StructureType
{
    road,
    building,
    fishpond,
}

public abstract class Structure : MonoBehaviour
{
    [SerializeField] protected StructureType structureType;
    public StructureType StructureType { get { return structureType; } set { structureType = value; } }

    [SerializeField] protected bool functional;

    [SerializeField] private string structureName;
    public string StructureName { get { return structureName; } }

    [SerializeField] protected int hp;
    public int HP { get { return hp; } set { hp = value; } }

    [SerializeField] private int costToBuildWood;
    public int CostToBuildWood { get { return costToBuildWood; } }

    [SerializeField] private int costToBuildStone;
    public int CostToBuildStone { get { return costToBuildStone; } }

    [SerializeField] private int id;
    public int ID { get { return id; } set { id = value; } }


    [SerializeField] protected bool isHousing;
    public bool IsHousing { get { return isHousing; } set { isHousing = value; } }


    [SerializeField] protected int houseUnit;
    public int HouseUnit { get { return houseUnit; } }

    [SerializeField] private List<Anken> currentWorkers;
    public List<Anken> CurrentWorkers { get { return currentWorkers; } set { currentWorkers = value; } }

    // Start is called before the first frame update
    void Start()
    {
        functional = false;
        hp = 1;
    }


    public void AddStaff(Anken w)
    {
        currentWorkers.Add(w);
    }

    public void RemoveStaff(Anken w)
    {
        currentWorkers.Remove(w);
    }
}