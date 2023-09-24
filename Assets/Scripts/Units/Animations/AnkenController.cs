using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkenController : MonoBehaviour
{
    private Animator anim;
    private Anken anken;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anken = GetComponent<Anken>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        DisableAll();
        switch (anken.State)
        {
            case UnitState.Idle:
                anim.SetBool("isIdle", true);
                break;
            case UnitState.Walk:
            case UnitState.MoveToAttack:
                anim.SetBool("isWalk", true);
                break;
            case UnitState.Death:
                anim.SetBool("isDeath", true);
                break;
            case UnitState.Attack:
            case UnitState.AttackUnit:
                anim.SetBool("isAttack", true);
                break;
        }
    }

    private void DisableAll()
    {
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalk", false);
        anim.SetBool("isDeath", false);
        anim.SetBool("isAttack", false);

    }
}
