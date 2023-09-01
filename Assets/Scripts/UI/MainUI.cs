using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    public GameObject buildPanel;

    public static MainUI instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
}