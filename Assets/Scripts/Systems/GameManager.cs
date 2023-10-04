using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int day = 0;
    public int Day { get { return day; } set { day = value; } }

    [SerializeField] private float dayTimer = 0f;
    [SerializeField] private float secondsPerDay = 3f;

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
        CheckTimeForDay();
    }

    public void CheckTimeForDay()
    {
        dayTimer += Time.deltaTime;

        if (dayTimer > secondsPerDay)
        {
            dayTimer = 0f;
            day++;
            Office.instance.DialyCostFishUpdate();
            MainUI.instance.UpdateResourceUI();
            MainUI.instance.UpdateDayText();
        }
    }
}
