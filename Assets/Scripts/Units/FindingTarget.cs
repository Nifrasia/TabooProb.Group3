using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public static GameObject CheckForNearestForest(Vector3 origin, float range, string tag)
    {
        RaycastHit[] hits = Physics.SphereCastAll(origin,
                                                    range,
                                                    Vector3.up,
                                                    0f,
                                                    LayerMask.GetMask("Forest"));

        GameObject closest = null;
        float closestDist = 0f;

        for (int x = 0; x < hits.Length; x++)
        {
            // skip if this is not a target's tag
            if (hits[x].collider.tag != tag)
                continue;

            //Debug.Log("Test - " + hits[x].collider.gameObject.ToString());
            Forest target = hits[x].collider.GetComponent<Forest>();
            float dist = Vector3.Distance(origin, hits[x].transform.position);

            // skip if this is not a mine
            if (target == null)
                continue;

            // skip if it is any depleted mine
            //if (target.HP <= 0)
            //    continue;
            // if the closest is null or the distance is less than the closest distance it currently has
            else if ((closest == null) || (dist < closestDist))
            {
                closest = hits[x].collider.gameObject;
                closestDist = dist;
            }
        }

        if (closest != null)
            return closest;
        else
            return null;
    }
}
