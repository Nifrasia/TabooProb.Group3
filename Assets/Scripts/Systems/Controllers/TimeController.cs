using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    [SerializeField] private int day = 0;
    public int Day { get { return day; } set { day = value; } }


    [SerializeField] private DateTime CurrentTime;
    [SerializeField] public int dayLenght; // in second

    [SerializeField] private String Timer;



    [SerializeField] private float startHour;

    [SerializeField] private Light _sunLight;
    [SerializeField] private float sunRiseHour = 5;
    [SerializeField] private float sunSetHour = 5;

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    // Start is called before the first frame update
    void Start()
    {
        sunriseTime = TimeSpan.FromHours(sunRiseHour);
        sunsetTime = TimeSpan.FromHours(sunSetHour);
        
    }

    // Update is called once per frame
    void Update()
    {
     TimeUpdate();
        
    }

    public void TimeUpdate()
    {
        CurrentTime = CurrentTime.AddMinutes(Time.deltaTime);
        //((Time.deltaTime * (24 / minuteInOneDay)) / 60)
        //TimeOfDay += ((Time.deltaTime * (24 / dayLenght)) / 60);

        Timer = CurrentTime.ToString("HH : mm");
        //Timer = TimeOfDay.ToString();
    }

    public void DayUpdate()
    {
        day++;
    }


    private TimeSpan CalculateTimeDifferent(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
}
