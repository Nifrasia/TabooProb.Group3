using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        //DemolishOnClick();

        CheckLeftClick();
    }

    public void BeginNewBuildingPlacement(GameObject prefab)
    {
        if (CheckCostToBuild(prefab) == true)
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
        if (CheckCostToBuild(structureObj))
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

        if ((Office.instance.Wood - costWood) < 0 || (Office.instance.Stone - costStone) < 0) {
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
                case "CommuCamp": // if we click Object with tag 
                    MainUI.instance.toggleCommuCampPanel();
                    break;
                case "FishingPond":
                    MainUI.instance.toggleFishpondPanel();
                    break;
                case "Cabin":
                    MainUI.instance.toggleCabinPanel();
                    break;
                case "CampFire":
                    MainUI.instance.toggleCampFirePanel();
                    break;
                case "LoggerCamp":
                    MainUI.instance.toggleLoggerCampPanel();
                    break;
                case "Mine":
                    MainUI.instance.toggleMinePanel();
                    break;

            }
        }
    }

    /*public void DemolishOnClick()
    {
        Structure s = Office.instance.Structures[]; 

        Office.instance.RemoveBuilding(s);
        MainUI.instance.UpdateResourceUI();
    }*/

    public void CallStafftoFishingPound()
    {
        Office.instance.SendStaffToFishingPound(CurStructure);
        MainUI.instance.UpdateResourceUI();
    }

    public void CallStafftoMine()
    {
        Office.instance.SendStaffToMine(CurStructure);
        MainUI.instance.UpdateResourceUI();
    }

    public void CallStafftoCutTree()
    {
        Office.instance.SendStaffToCutTree(CurStructure);
        MainUI.instance.UpdateResourceUI();
    }

    public void CallStafftoForest() //Call Worker in Warehouse Panel
    {
        GameObject forest = FindingTarget.CheckForNearestForest(CurStructure.transform.position,
                                                                        10000f,
                                                                        "Forest");
        Debug.Log(forest);
        Office.instance.SendStaffToForest(forest, CurStructure);
        MainUI.instance.UpdateResourceUI();
    }
}