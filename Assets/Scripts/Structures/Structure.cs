using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StructureType
{
    road,
    building,
    wheat,
}

public abstract class Structure : MonoBehaviour
{
    [SerializeField] private StructureType structureType;
    public StructureType StructureType { get { return structureType; } set { structureType = value; } }

    [SerializeField] protected bool functional;

    [SerializeField] private string structureName;
    public string StructureName { get { return structureName; } }

    [SerializeField] protected int hp;
    public int HP { get { return hp; } set { hp = value; } }

    [SerializeField] private int costToBuild;
    public int CostToBuild { get { return costToBuild; } }

    [SerializeField] private int id;
    public int ID { get { return id; } set { id = value; } }

    // Start is called before the first frame update
    void Start()
    {
        functional = false;
        hp = 1;
    }
}