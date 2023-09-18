using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text fishText;
    [SerializeField] private TMP_Text ankenText;

    [SerializeField] private TMP_Text dayText;

    public GameObject buildPanel;
    public GameObject laborPanel;
    public GameObject fishpondPanel;
    public GameObject optionsPanel;
    public GameObject settingPanel;
    public GameObject CommuCampPanel;
    public GameObject LoggerCampPanel;
    public GameObject CabinPanel;
    public GameObject MinePanel;
    public GameObject CampFirePanel;

    public static MainUI instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        UpdateResourceUI();
        UpdateDayText();

    }

    public void UpdateResourceUI()
    {
        woodText.text = Office.instance.Wood.ToString();
        stoneText.text = Office.instance.Stone.ToString();
        fishText.text = Office.instance.Fish.ToString();
        ankenText.text = Office.instance.Ankens.Count.ToString() + " / " + Office.instance.UnitLimit.ToString();
    }

    public void toggleBuildPanel()
    {
     if(!buildPanel.activeInHierarchy){
            buildPanel.SetActive(true);
        }
        else
        {
            buildPanel.SetActive(false); 
        }
    }

    public void toggleLaborPanel()
    {
        if (!laborPanel.activeInHierarchy)
        {
            laborPanel.SetActive(true);
        }
        else
        {
            laborPanel.SetActive(false);
        }
    }

    public void toggleFishpondPanel()
    {
        if(!fishpondPanel.activeInHierarchy)
        {
            fishpondPanel.SetActive(true);
        }
        else
        {
            fishpondPanel.SetActive(false);
        }
    }

    public void toggleOptionsPanel()
    {
        if (!optionsPanel.activeInHierarchy)
        {
            optionsPanel.SetActive(true);
        }
        else
        {
            optionsPanel.SetActive(false);
        }
    }

    public void toggleSettingPanel()
    {
        if (!settingPanel.activeInHierarchy)
        {
            settingPanel.SetActive(true);
        }
        else
        {
            settingPanel.SetActive(false);
        }
    }

    public void toggleCommuCampPanel()
    {
        if (!CommuCampPanel.activeInHierarchy)
        {
            CommuCampPanel.SetActive(true);
        }
        else
        {
            CommuCampPanel.SetActive(false);
        }
    }

    public void toggleLoggerCampPanel()
    {
        if (!LoggerCampPanel.activeInHierarchy)
        {
            LoggerCampPanel.SetActive(true);
        }
        else
        {
            LoggerCampPanel.SetActive(false);
        }
    }
    public void toggleMinePanel()
    {
        if (!MinePanel.activeInHierarchy)
        {
            MinePanel.SetActive(true);
        }
        else
        {
            MinePanel.SetActive(false);
        }
    }
    public void toggleCabinPanel()
    {
        if (!CabinPanel.activeInHierarchy)
        {
            CabinPanel.SetActive(true);
        }
        else
        {
            CabinPanel.SetActive(false);
        }
    }

    public void toggleCampFirePanel()
    {
        if (!CampFirePanel.activeInHierarchy)
        {
            CampFirePanel.SetActive(true);
        }
        else
        {
            CampFirePanel.SetActive(false);
        }
    }
    public void UpdateDayText()
    {
        dayText.text = "Day " + GameManager.instance.Day.ToString();
    }
}