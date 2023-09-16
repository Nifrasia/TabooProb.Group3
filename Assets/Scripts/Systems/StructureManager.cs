using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructureManager : MonoBehaviour
{
    [SerializeField] private bool isConstructing;
    [SerializeField] private bool isDemolishing;

    [SerializeField] private GameObject curBuildingPrefab;
    [SerializeField] private GameObject buildingParent;

    [SerializeField] private Vector3 curCursorPos;

    public GameObject buildingCursor;
    public GameObject gridPlane;

    private GameObject ghostBuilding;

    [SerializeField] private GameObject _curStructure; //Currently selected structure
    public GameObject CurStructure { get { return _curStructure; } set { _curStructure = value; } }

    [SerializeField] private GameObject[] structurePrefab;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            CancelStructureMode();
        }

        curCursorPos = Formula.instance.GetCurTilePosition();

        if (isConstructing) //Mode Construct
        {
            buildingCursor.transform.position = curCursorPos;
            gridPlane.SetActive(true);
        }
        else //Mode Play
        {
            gridPlane.SetActive(false);
        }

        CheckLeftClick();
    }

    public void BeginNewBuildingPlacement(GameObject prefab)
    {
        if (CheckCostToBuild(prefab) == false)
        {
            return;
        }

        isDemolishing = false;
        isConstructing = true;

        curBuildingPrefab = prefab;

        //Instantiage Ghost Building
        ghostBuilding = Instantiate(curBuildingPrefab, curCursorPos, Quaternion.identity);
        ghostBuilding.GetComponent<FindBuildingSite>().Plane.SetActive(true);

        buildingCursor = ghostBuilding;
        buildingCursor.SetActive(true);
    }

    private void PlaceBuilding()
    {
        if (buildingCursor.GetComponent<FindBuildingSite>().CanBuild == false)
            return;

        GameObject structureObj = Instantiate(curBuildingPrefab,
                                               curCursorPos,
                                               Quaternion.identity,
                                               buildingParent.transform);

        Structure s = structureObj.GetComponent<Structure>();

        //Add building in Office
        Office.instance.AddBuilding(s);
        //Deduct Cost
        DeductCost(s.CostToBuildWood, s.CostToBuildStone);
        //Cancle if there is not enough resource
        if(CheckCostToBuild(structureObj))
        {
            CancelStructureMode();
        }

    }
    private void CheckLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isConstructing)
                PlaceBuilding(); //Real Construction
            else
                CheckOpenPanel();
        }
    }


    //cancle building
    private void CancelStructureMode()
    {
        isConstructing = false;

        if (buildingCursor != null)
            buildingCursor.SetActive(false);

        if (ghostBuilding != null)
            Destroy(ghostBuilding);
    }

    private bool CheckCostToBuild(GameObject obj)
    {
        int costWood = obj.GetComponent<Structure>().CostToBuildWood;
        int costStone = obj.GetComponent<Structure>().CostToBuildStone;

        if (costWood <= Office.instance.Wood && costStone <= Office.instance.Stone) {
            return true;
        }
        else {
            return false; 
        }
           
    }

    private void DeductCost(int costWood , int costStone)
    {
        Office.instance.Wood -= costWood;
        Office.instance.Stone -= costStone;
        MainUI.instance.UpdateResourceUI();
    }

    private void CheckOpenPanel()
    {
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //if we left click something
        if (Physics.Raycast(ray, out hit, 1000))
        {
            
            //Mouse over UI
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            CurStructure = hit.collider.gameObject;

            switch (hit.collider.tag) //hit.collider.tag
            {
                case "CommuCamp": // if we click Object with Farm tag 
                    OpenLaborMarket();
                    break;
                case "FishingPond":
                    OpenFishpondPanel();
                    break;   
            }
        }
    }
     
    public void OpenLaborMarket()
    {
        MainUI.instance.toggleLaborPanel();
    }
    public void OpenFishpondPanel()
    {
        MainUI.instance.toggleFishpondPanel();
    }



    public void CallStaff()
    {
        Office.instance.SendStaff(CurStructure);
        MainUI.instance.UpdateResourceUI();
    }
}