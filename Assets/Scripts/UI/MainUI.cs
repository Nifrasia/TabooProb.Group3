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

    public GameObject optionsPanel;
    public GameObject settingPanel;
    public GameObject buildPanel;
    public GameObject laborPanel;
    public GameObject campFirePanel;
    public GameObject commuCampPanel;
    public GameObject fishpondPanel;
    public GameObject loggerCampPanel;
    public GameObject minePanel;
    public GameObject cabinPanel;

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
        if (!commuCampPanel.activeInHierarchy)
        {
            commuCampPanel.SetActive(true);
        }
        else
        {
            commuCampPanel.SetActive(false);
        }
    }

    public void toggleLoggerCampPanel()
    {
        if (!loggerCampPanel.activeInHierarchy)
        {
            loggerCampPanel.SetActive(true);
        }
        else
        {
            loggerCampPanel.SetActive(false);
        }
    }
    public void toggleMinePanel()
    {
        if (!minePanel.activeInHierarchy)
        {
            minePanel.SetActive(true);
        }
        else
        {
            minePanel.SetActive(false);
        }
    }
    public void toggleCabinPanel()
    {
        if (!cabinPanel.activeInHierarchy)
        {
            cabinPanel.SetActive(true);
        }
        else
        {
            cabinPanel.SetActive(false);
        }
    }

    public void toggleCampFirePanel()
    {
        if (!campFirePanel.activeInHierarchy)
        {
            campFirePanel.SetActive(true);
        }
        else
        {
            campFirePanel.SetActive(false);
        }
    }
    public void UpdateDayText()
    {
        dayText.text = "Day " + GameManager.instance.Day.ToString();
    }
}