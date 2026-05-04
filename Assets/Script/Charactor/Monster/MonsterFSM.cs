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
public class MonsterFSM : MonoBehaviour
{
    public MonsterTank prameter;
    private Status currentstatus;
    private Dictionary<StatuType, Status> FsmPairs;

    private bool IsMoving;
    //private bool IsAttack;
    public bool IsDie { get; set; }

    private void Awake()
    {
        FsmPairs = new Dictionary<StatuType, Status>();
        prameter = GetComponent<MonsterTank>();

        FsmPairs.Add(StatuType.idel, new IdleStatu(this));
        FsmPairs.Add(StatuType.move, new MoveStatu(this));
        FsmPairs.Add(StatuType.rotation, new RotaionStatu(this));
        FsmPairs.Add(StatuType.charol, new CharolStatu(this));
        FsmPairs.Add(StatuType.attack, new AttackStatu(this));
        FsmPairs.Add(StatuType.die, new DieStatu(this));
    }
    void Start()
    {
        EventCenter.Instance.Add<int>(this, "MonsterHurt", OnHurt);
        Init();
    }
    public void Init()
    {
        IsMoving = true;
        //IsRotation = false;
        //IsAttack = false;
        IsDie = false;
        prameter.Init();
        ChangeStatu(StatuType.idel);
    }

    // Update is called once per frame
    void Update()
    {
        currentstatus.OnUpdate();
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
    private void OnHurt(int AkT)
    {
        prameter.HP -= AkT;
        if (prameter.HP < 0)
        {
            prameter.HP = 0;
            IsDie = true;
        }

    }
}
