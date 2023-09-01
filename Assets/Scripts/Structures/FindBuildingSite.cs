using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FindBuildingSite : MonoBehaviour
{
    [SerializeField] private bool canBuild = false;
    public bool CanBuild { get { return canBuild; } set { canBuild = value; } }

    [SerializeField] private GameObject plane;
    public GameObject Plane { get { return plane; } }

    private Renderer pRenderer;

    void Awake()
    {
        pRenderer = plane.GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pRenderer.material.color = Color.green;
        canBuild = true;
    }

    private void ChangeColor(Collider other, bool flag, Color color)
    {
        if (other.tag == "Building" || other.tag == "House" || other.tag == "Farm" || other.tag == "Road")
        {
            pRenderer.material.color = color;
            canBuild = flag;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeColor(other, false, Color.red);
    }

    private void OnTriggerStay(Collider other)
    {
        ChangeColor(other, false, Color.red);
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeColor(other, true, Color.green);
    }
}