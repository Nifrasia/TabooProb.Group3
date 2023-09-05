using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : MonoBehaviour
{
    public static Office instance;

    [SerializeField] private int wood;
    public int Wood { get { return wood; } set { wood = value; } }
    [SerializeField] private int stone;
    public int Stone { get { return stone; } set { stone = value; } }
    [SerializeField] private int fish;
    public int Fish { get { return fish; } set { fish = value; } }
    [SerializeField] private int anKen;
    public int Anken { get { return anKen; } set { anKen = value; } }

    [SerializeField] private int dailyCostWages;

    [SerializeField] private List<Structure> structures = new List<Structure>();
    public List<Structure> Structures { get { return structures; } }


    [SerializeField] private int availStaff;
    public int AvailStaff { get { return availStaff; } set { availStaff = value; } }

    [SerializeField] private GameObject staffParent;

    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private GameObject rallyPosition;

    

    private void Awake()
    {
        instance = this;
    }

    public void AddBuilding(Structure s)
    {
        structures.Add(s);
    }

    public void RemoveBuilding(Structure s)
    {
        structures.Remove(s);
        Destroy(s.gameObject);
    }

    public bool ToHireStaff(GameObject workerObj)
    {
        if (wood <= 0)
            return false;

        workerObj.transform.parent = staffParent.transform;

        Anken w = workerObj.GetComponent<Anken>();

        w.Hired = true; //Hire this worker
        

        wood -= w.DailyWage;
        

        //Update UI
        MainUI.instance.UpdateResourceUI();

        return true;
    }

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
