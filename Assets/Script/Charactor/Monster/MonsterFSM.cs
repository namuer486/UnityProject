using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatuType
{
    idel,//¿ÕÏÐ
    move,//Ñ²Âß
    rotation,//Ðý×ª
    charol,//×·»÷
    attack,//¹¥»÷
    die//ËÀÍö
}
public class MonsterFSM
{
    public MonsterFSM(MonsterTank monster)
    {
        prameter = monster;
        Init();
    }
    public MonsterTank prameter;
    public Status currentstatus {  get; private set; }
    private Dictionary<StatuType, Status> FsmPairs;

    private bool IsMoving;
    //private bool IsAttack;
    public bool IsDie { get; set; }
    public void Init()
    {
        FsmPairs = new Dictionary<StatuType, Status>();

        FsmPairs.Add(StatuType.idel, new IdleStatu(this));
        FsmPairs.Add(StatuType.move, new MoveStatu(this));
        FsmPairs.Add(StatuType.rotation, new RotaionStatu(this));
        FsmPairs.Add(StatuType.charol, new CharolStatu(this));
        FsmPairs.Add(StatuType.attack, new AttackStatu(this));
        FsmPairs.Add(StatuType.die, new DieStatu(this));

        IsMoving = true;
        IsDie = false;
        ChangeStatu(StatuType.idel);
    }
    public void ChangeStatu(StatuType type)
    {
        if(currentstatus != null)
        {
            currentstatus.OnExit();
        }
        currentstatus = FsmPairs[type];
        currentstatus.OnEnter();
    }
    public bool CanMove() => IsMoving;
    public bool CanRotation() => !IsMoving;
    public bool CanAttack() => prameter.CheckAttack();
    public bool CanIdel() => !prameter.CheckAttack();
    //public bool CanDie() => IsDie;
}
