using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoggerCamp : Structure
{


    [SerializeField] private int maxStaffNum = 3;
    public int MaxStaffNum { get { return maxStaffNum; } set { maxStaffNum = value; } }

    [SerializeField] private float produceTimer = 0f;
    [SerializeField] private int produceTime; //time to finish produce
    [SerializeField] private int produceRate; //produce per anken

    public int ProduceRate { get { return produceRate; } set { produceRate = value; } }
    public int ProduceTime { get { return produceTime; } set { produceTime = value; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentWorkers.Count > 0)
        {
            cutting();
        }
    }


    public void cutting()
    {
        produceTimer += Time.deltaTime;
        if (produceTimer >= produceTime)
        {
            produceTimer = 0;
            Office.instance.Wood += produceRate * CurrentWorkers.Count;
            MainUI.instance.UpdateResourceUI();
        }
    }

}
