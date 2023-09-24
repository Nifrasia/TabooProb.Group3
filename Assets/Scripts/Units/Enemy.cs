using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private LayerMask unitLayerMask;


    [SerializeField] float checkForEnemyRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckForAttack", 0f, checkForEnemyRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject != targetStructure)
    //        return;

    //    Structure s = other.gameObject.GetComponent<Structure>();
    //    if ((s != null) && (s.HP > 0))
    //        state = UnitState.AttackBuilding;
    //}


    private void CheckForAttack()
    {
        // get nearest enemy
        Unit enemyUnit = CheckForNearestEnemyUnit();

        //check is enemy null
        if(enemyUnit != null)
        {
            //if enemy is exist set target and set unit state
            targetUnit = enemyUnit.gameObject;
            SetUnitState(UnitState.MoveToAttack);
        }
        else
        {
            targetUnit=null;
            SetUnitState(UnitState.Idle);
        }

    }

    protected Unit CheckForNearestEnemyUnit()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,
                                                    detectRange,
                                                    Vector3.up,
                                                    unitLayerMask);

        GameObject closest = null;
        float closestDist = 0f;

        for (int x = 0; x < hits.Length; x++)
        {
            // skip if this is not a player's unit
            if (hits[x].collider.tag != "Unit")
                continue;

            Unit target = hits[x].collider.GetComponent<Unit>();
            float dist = Vector3.Distance(transform.position, hits[x].transform.position);

            // skip if this is not a unit
            if (target == null)
                continue;

            // skip if it is any dead unit
            if (target.HP <= 0)
                continue;
            // if the closest is null or the distance is less than the closest distance it currently has
            else if ((closest == null) || (dist < closestDist))
            {
                closest = hits[x].collider.gameObject;
                closestDist = dist;
            }
        }

        if (closest != null)
            return closest.GetComponent<Unit>();
        else
            return null;
    }
}
