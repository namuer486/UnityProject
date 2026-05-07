using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IdleStatu : Status
{
    // Start is called before the first frame update
    private MonsterTank prameter;
    private MonsterFSM mananger;
    private float tiemr;
    //private bool IsIdel;
    public IdleStatu(MonsterFSM manager)
    {
        this.mananger = manager;
        this.prameter = mananger.prameter;
    }
    public void OnEnter()
    {
        Debug.Log("怪兽沉默");
        tiemr = 0;
    }
    public void OnUpdate()
    {
        tiemr += Time.deltaTime;
        if (mananger.IsDie)
        {
            mananger.ChangeStatu(StatuType.die);
        }
        if(mananger.CanAttack())
        { 
            mananger.ChangeStatu(StatuType.attack);
        }
        if( tiemr > 1)
        {
            mananger.ChangeStatu(StatuType.charol);
        }
    }
    public void OnExit()
    {
        

    }
}
public class MoveStatu : Status
{
    // Start is called before the first frame update
    private MonsterTank prameter;
    private MonsterFSM mananger;
    public MoveStatu(MonsterFSM manager)
    {
        this.mananger = manager;
        this.prameter = mananger.prameter;
    }
    public void OnEnter()
    {

    }
    public void OnUpdate()
    {
        if (mananger.IsDie)
        {
            mananger.ChangeStatu(StatuType.die);
        }
        if (mananger.CanAttack())
        {
            mananger.ChangeStatu(StatuType.attack);
        }
        if (mananger.CanRotation())
        {
            mananger.ChangeStatu(StatuType.rotation);
        }
        EventCenter.Instance.OnTriggerEven("MonsterMove"); //TODO：传入巡逻的初始点和结束点

    }
    public void OnExit()
    {

    }
}
public class RotaionStatu : Status
{
    // Start is called before the first frame update
    private MonsterTank prameter;
    private MonsterFSM mananger;
    public RotaionStatu(MonsterFSM manager)
    {
        this.mananger = manager;
        this.prameter = mananger.prameter;
    }
    public void OnEnter()
    {
        EventCenter.Instance.OnTriggerEven("RotationEuler");
    }
    public void OnUpdate()
    {
        if (mananger.IsDie)
        {
            mananger.ChangeStatu(StatuType.die);
        }
        if (mananger.CanMove())
        {
            mananger.ChangeStatu(StatuType.move);
        }
        EventCenter.Instance.OnTriggerEven("MonsterRotation");//TODO：计算旋转角度传入

    }
    public void OnExit()
    {

    }
}
public class CharolStatu : Status
{
    // Start is called before the first frame update
    private MonsterTank prameter;
    private MonsterFSM mananger;
    public CharolStatu(MonsterFSM manager)
    {
        this.mananger = manager;
        this.prameter = mananger.prameter;
    }
    public void OnEnter()
    {
        Debug.Log("怪兽开始追击");
    }
    public void OnUpdate()
    {
        if (mananger.IsDie)
        {
            mananger.ChangeStatu(StatuType.die);
        }
        if (mananger.CanAttack())
        {
            mananger.ChangeStatu(StatuType.attack);
        }
        prameter.Charol();

    }
    public void OnExit()
    {
        EventCenter.Instance.OnTriggerEven("MonsterCharolOver");
    }
}
public class AttackStatu : Status
{
    // Start is called before the first frame update
    private MonsterTank prameter;
    private MonsterFSM mananger;
    private float timer = 0;
    private Transform fort;
    public AttackStatu(MonsterFSM manager)
    {
        this.mananger = manager;
        this.prameter = mananger.prameter;
        fort = prameter.transform.Find("fort");
    }
    public void OnEnter()
    {
    
        Debug.Log("enattack");
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if(mananger.IsDie)
        {
            mananger.ChangeStatu(StatuType.die);
        }
        if (mananger.CanIdel())
        {
            mananger.ChangeStatu(StatuType.idel);
        }
        if (timer>prameter.attack_cd)
        {
            prameter.Attack();
            timer = 0;
        }
        
    }
    public void OnExit()
    {

    }

}
public class DieStatu : Status
{
    // Start is called before the first frame update
    private MonsterTank prameter;
    private MonsterFSM mananger;

    public DieStatu(MonsterFSM manager)
    {
        this.mananger = manager;
        this.prameter = mananger.prameter;

    }
    public void OnEnter()
    {
        EventCenter.Instance.OnTriggerEven("MonsterDie", prameter.gameObject);

    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
