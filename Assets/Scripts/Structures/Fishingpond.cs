using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmStage
{
    throwing,
    waiting,
    catching,
}
public class Fishingpond : Structure
{
    // Start is called before the first frame update
    [SerializeField] private FarmStage stage = FarmStage.throwing;

    [SerializeField] private int maxStaffNum = 3;
    public int MaxStaffNum { get { return maxStaffNum; } set { maxStaffNum = value; } }

    [SerializeField] private int product = 40;
    public int Product { get { return product; } }

    [SerializeField] private int ProduceTime; //Day until harvest
    //[SerializeField] private int ProduceTimePassed; //Day passed since last harvest

    [SerializeField] private float produceTimer = 0f;

    [SerializeField] private int throwingTime;
    [SerializeField] private int waitingTime;
    [SerializeField] private int catchingTime;

    //private float ProduceTimer = 0f;
    //private float ProduceTimeWait = 1f;



    [SerializeField] private GameObject FarmUI;

    [SerializeField] private List<Anken> currentWorkers;
    public List<Anken> CurrentWorkers { get { return currentWorkers; } set { currentWorkers = value; } }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkState();
    }

    public void checkState()
    {
        switch (stage)
        {
            case FarmStage.throwing:
                checkThrowing(); break;
            case FarmStage.waiting:
                checkWaiting(); break;
            case FarmStage.catching:
                checkCatching(); break;
        }
           
    }

    public void checkThrowing()
    {
        produceTimer += Time.deltaTime;
        if (produceTimer >= throwingTime)
        {
            produceTimer = 0;
            stage = FarmStage.waiting;
        }

    }
    public void checkWaiting()
    {
        produceTimer += Time.deltaTime;
        //dayPassed = Mathf.CeilToInt(produceTimer / secondsPerDay);

        if (produceTimer >= waitingTime)
        {
            produceTimer = 0;
            stage = FarmStage.catching;
        }
    }

        public void checkCatching()
    {
        produceTimer += Time.deltaTime;
        if (produceTimer >= catchingTime)
        {
            produceTimer = 0;
            Office.instance.Fish += Product;
            MainUI.instance.UpdateResourceUI();
            stage = FarmStage.throwing;
        }

    }

    public void AddStaffToFarm(Anken w)
    {
        currentWorkers.Add(w);
    }



}
