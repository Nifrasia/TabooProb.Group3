using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject laborMarketPanel;
    [SerializeField] private TMP_Text WoodText;
    [SerializeField] private TMP_Text StoneText;
    [SerializeField] private TMP_Text FishText;
    [SerializeField] private TMP_Text AnkenText;

    //public static MainUI instance;

    // Start is called before the first frame update
    void Start()
    {
        //instance = this;
        UpdateResourceUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleLaborPanel()
    {
        if (!laborMarketPanel.activeInHierarchy)
            laborMarketPanel.SetActive(true);
        else
            laborMarketPanel.SetActive(false);
    }
    public void UpdateResourceUI()
    {
        /*WoodText.text = Office.instance.Money.ToString();
        StoneText.text = Office.instance.Workers.Count.ToString();
        FishText.text = Office.instance.Workers.Count.ToString();
        AnkenText.text = Office.instance.Workers.Count.ToString();*/
    }

}
