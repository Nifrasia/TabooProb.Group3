using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    [SerializeField] private int day = 0;
    //public int Day { get { return day; } set { day = value; } }


    [SerializeField] private DateTime currentTime;
    [SerializeField] public int dayLenght; // in second

    [SerializeField] private String Timer;
    [SerializeField] private TMP_Text dayText;


    [SerializeField] private float startHour;

    [SerializeField] private Light _sunLight;
    [SerializeField] private float sunRiseHour;
    [SerializeField] private float sunSetHour;

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
        RotateSun();
        
    }

    public void TimeUpdate()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * ((1440f * 60)/dayLenght));
        Timer = currentTime.ToString();
        DayUpdate();
    }

    public void DayUpdate()
    {
        if(day < currentTime.Day)
        {
            Office.instance.DialyCostFishUpdate();
            MainUI.instance.UpdateResourceUI();
        }
        day = currentTime.Day;
        dayText.text = "Day " + currentTime.Day.ToString();
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if(currentTime.TimeOfDay >  sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifferent(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifferent(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0,180,(float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifferent(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifferent(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }
        _sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);

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
