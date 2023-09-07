using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmStage
{
    throwing,
    waiting,
    catching,
}
public class Fisihingpod : Structure
{
    // Start is called before the first frame update
    [SerializeField] private FarmStage stage = FarmStage.throwing;

    [SerializeField] private int maxStaffNum = 3;
    public int MaxStaffNum { get { return maxStaffNum; } set { maxStaffNum = value; } }

    [SerializeField] private int dayRequired; //Day until harvest
    [SerializeField] private int dayPassed; //Day passed since last harvest

    [SerializeField] private float produceTimer = 0f;
    private int secondsPerDay = 10;

    [SerializeField] private GameObject FarmUI;

    [SerializeField] private List<Anken> currentWorkers;
    public List<Anken> CurrentWorkers { get { return currentWorkers; } set { currentWorkers = value; } }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkThrowing()
    {
        if ((hp >= 100) && (stage == FarmStage.throwing))
        {
            stage = FarmStage.waiting;
            hp = 1;
        }

    }
    public void checkWaiting()
    {
        if ((hp >= 100) && (stage == FarmStage.waiting))
        {
            produceTimer += Time.deltaTime;
            dayPassed = Mathf.CeilToInt(produceTimer / secondsPerDay);

            if ((functional == true) && (dayPassed >= dayRequired))
            {
                produceTimer = 0;
                stage = FarmStage.catching;
                hp = 1;
            }


        }
    }

        public void checkCatching()
    {
        if ((hp >= 100) && (stage == FarmStage.catching))
        {
            //harvest
            Debug.Log("Harvest +1000");

            hp = 1;
            stage = FarmStage.throwing;
        }

    }
}
