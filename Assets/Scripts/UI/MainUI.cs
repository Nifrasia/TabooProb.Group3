using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text fishText;
    [SerializeField] private TMP_Text ankenText;
    public GameObject buildPanel;

    public static MainUI instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        UpdateResourceUI();

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
    public void UpdateResourceUI()
    {
        woodText.text = Office.instance.Wood.ToString();
        stoneText.text = Office.instance.Stone.ToString();
        fishText.text = Office.instance.Fish.ToString();
        ankenText.text = Office.instance.Ankens.ToString();
    }
}