using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Tank:Character
{
    public int Exp;

    public void Reset()
    {
        HP = 100;
        MP = 50;
        Level = 0;
        Exp = 0;
    }
}
public enum PlayerType
{
    idel,
    move,
    rotation,
    die
}
public class Player : MonoBehaviour
{
    public Tank tank;
    public Transform BirthPos;

    public Vector3 DiePos=new Vector3(110,1110,10);
    private Dictionary<PlayerType, Status> status=new Dictionary<PlayerType, Status>();
    private Status curruntstatu;

    private bool IsMoving=false;
    private bool IsRotation=false;
    private bool IsDie=false;

    private void Start()
    {
        tank=new Tank();
        
        status.Add(PlayerType.idel, new PlayerIdelStatus(this));
        status.Add(PlayerType.move, new PlayerMoveStatus(this));
        status.Add(PlayerType.rotation, new PlayerRotationStatus(this));
        status.Add(PlayerType.die, new PlayerDieStatus(this));
        Init();

        EventCenter.Instance.Add<int>(this, "PlayerHurt", OnHurt);
        EventCenter.Instance.Add<int>(this, "PlayerExpUp", ExpUp);

    }
    public void Init()
    {
        tank.Reset();
        IsDie = false;
        ChangePlayerStatus(PlayerType.idel);
        transform.position = BirthPos.position;

        EventCenter.Instance.OnTriggerEven("HPUpDate", tank.HP);
        EventCenter.Instance.OnTriggerEven("MPUpDate", tank.MP);
        EventCenter.Instance.OnTriggerEven("LevelUpDate", tank.Level);
    }
    private void Update()
    {
        if(tank.HP<=0)
        {
            IsDie = true;//playerdie
        }
        if (Input.GetMouseButtonUp(0)&&!IsDie)
        {
            EventCenter.Instance.OnTriggerEven("PlayerAttack");
        }
        IsMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
        IsRotation = Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E);
        curruntstatu.OnUpdate();
        
    }
    private void OnDestroy()
    {
        EventCenter.Instance.RemoveAll(this);
    }
    public void ChangePlayerStatus(PlayerType playerType)
    {
        Debug.Log(playerType);
        if (curruntstatu!=null)
        {
            curruntstatu.OnExit();
        }
        curruntstatu = status[playerType];
        curruntstatu.OnEnter();
    }
    public bool CanMove() => IsMoving;
    public bool CanRotation() => IsRotation;
    public bool CanDie() => IsDie;

    private void OnHurt(int AkT)
    {
        tank.HP-=AkT;
        if (tank.HP < 0) { tank.HP = 0; }
        EventCenter.Instance.OnTriggerEven("HPUpDate", tank.HP);
    }
    private void MPHurt(int value)
    {
        tank.MP-=value;
        if (tank.MP < 0) {tank.MP = 0; }
        EventCenter.Instance.OnTriggerEven("MPUpDate", tank.MP);
    }
    private void LevelUp()
    {
        tank.Level++;
        EventCenter.Instance.OnTriggerEven("LevelUpDate", tank.Level);
    }
    private void ExpUp(int exp)
    {
        tank.Exp += exp;
        if(tank.Exp > 100)
        {
            tank.Exp -= 100;
            LevelUp();
        }
        Debug.Log("EXP:" + tank.Exp);
        EventCenter.Instance.OnTriggerEven("ExpUpDate", tank.Exp);
    }
    public void Die()
    {
        transform.position = DiePos;
        
    }
}
