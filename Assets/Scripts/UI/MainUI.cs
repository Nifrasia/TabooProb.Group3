using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{

    public GameObject toolPanel;
    public GameObject laborMarketPanel;
    public static MainUI instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void toggleToolPanel()
    {
     if(!toolPanel.activeInHierarchy){
        toolPanel.SetActive(true);
        }
        else
        {
            toolPanel.SetActive(false); 
        }
    
    
    
    }


}