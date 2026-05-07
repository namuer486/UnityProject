using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    idel,
    move,
    rotation,
    attack,
    die
}
public class PlayerFSM
{
    public Player tank { get; private set; }
    private Dictionary<PlayerType, Status> status = new Dictionary<PlayerType, Status>();
    public Status curruntstatu { get; private set; }

    public bool IsMoving {  get; set; }
    public int IsRotation {  get; set; }
    public bool IsAttack{  get; set; }
    public bool IsDie {  get; set; }
    public PlayerFSM(Player tank)
    {
        this.tank = tank;
        Init();
    }
    private void Init()
    {
        status.Add(PlayerType.idel, new PlayerIdelStatus(this));
        status.Add(PlayerType.move, new PlayerMoveStatus(this));
        status.Add(PlayerType.rotation, new PlayerRotationStatus(this));
        status.Add(PlayerType.attack, new PlayerAttackStatus(this));
        status.Add(PlayerType.die, new PlayerDieStatus(this));
        ChangePlayerStatus(PlayerType.idel);

        EventCenter.Instance.Add<bool>(this, "PlayerToMove", Move);
        EventCenter.Instance.Add<bool>(this, "PlayerToAttack", Attack);
        EventCenter.Instance.Add<int>(this, "PlayerToRotation", Rotation);
    }
    public void ChangePlayerStatus(PlayerType playerType)
    {
        Debug.Log("Íæ¼Òµ±Ç°×´Ì¬"+playerType);
        if (curruntstatu != null)
        {
            curruntstatu.OnExit();
        }
        curruntstatu = status[playerType];
        curruntstatu.OnEnter();
    }
    private void Move(bool flag)
    {
        IsMoving = flag;
    }
    private void Attack(bool flag)
    {
        IsAttack = flag;
    }
    private void Rotation(int flag)
    {
        IsRotation = flag;
    }
}
