using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdelStatus:Status
{
    // Start is called before the first frame update
    private PlayerFSM manager;
    private Player tank;
    public PlayerIdelStatus(PlayerFSM manager)
    {
        this.manager = manager;
        tank = manager.tank;
    }
    public void OnEnter()
    {
        
    }
    public void OnUpdate()
    {
        if (manager.IsDie)
        {
            manager.ChangePlayerStatus(PlayerType.die);
        }
        if(manager.IsMoving)
        {
            manager.ChangePlayerStatus(PlayerType.move);
        }
        if (manager.IsAttack)
        {
            manager.ChangePlayerStatus(PlayerType.attack);
        }
        if (manager.IsRotation != 0)
        {
            manager.ChangePlayerStatus(PlayerType.rotation);
        }
    }
    public void OnExit()
    {
        
    }
}

public class PlayerMoveStatus:Status
{
    // Start is called before the first frame update
    private PlayerFSM manager;
    private Player tank;
    public PlayerMoveStatus(PlayerFSM manager)
    {
        this.manager = manager;
        tank = manager.tank;
    }
    public void OnEnter()
    {
        
    }
    public void OnUpdate()
    {
        if (manager.IsDie)
        {
            manager.ChangePlayerStatus(PlayerType.die);
        }
        if (manager.IsRotation!=0)
        {
            manager.ChangePlayerStatus(PlayerType.rotation);
        }
        if (manager.IsAttack)
        {
            manager.ChangePlayerStatus(PlayerType.attack);
        }
        if (!manager.IsMoving)
        {
            manager.ChangePlayerStatus(PlayerType.idel);
        }
        tank.Move();
    }
    public void OnExit()
    {

    }
}

public class PlayerRotationStatus: Status
{
    // Start is called before the first frame update
    private PlayerFSM manager;
    private Player tank;
    private bool isLeft;
    private float timer;
    public PlayerRotationStatus(PlayerFSM manager)
    {
        this.manager = manager;
        tank = manager.tank;
    }
    public void OnEnter()
    {
        if(Input.GetKeyUp(KeyCode.Q))
            isLeft = true;
        else if(Input.GetKeyUp(KeyCode.E))
            isLeft = false;
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if(manager.IsDie)
        {
            manager.ChangePlayerStatus(PlayerType.die);
        }
        if (manager.IsRotation==0)
        {
            manager.ChangePlayerStatus(PlayerType.idel);
        }
        if (manager.IsRotation < 0)
        {
            manager.tank.LeftRotation();
        }
        else if(manager.IsRotation > 0)
        {
            manager.tank.RightRotation();
        }

    }
    public void OnExit()
    {
        timer = 0;
    }
}
public class PlayerAttackStatus : Status
{
    // Start is called before the first frame update
    private PlayerFSM manager;
    private Player tank;
    private float timer;
    public PlayerAttackStatus(PlayerFSM manager)
    {
        this.manager = manager;
        tank = manager.tank;
    }
    public void OnEnter()
    {
        timer = 0;
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if(manager.IsDie)
        {
            manager.ChangePlayerStatus(PlayerType.die);
        }
        if (manager.IsRotation != 0)
        {
            manager.ChangePlayerStatus(PlayerType.rotation);
        }
        if (manager.IsMoving)
        {
            manager.ChangePlayerStatus(PlayerType.move);
        }
        if (!manager.IsAttack)
        {
            manager.ChangePlayerStatus(PlayerType.idel);
        }
        if(timer > tank.attack_cd&&manager.IsAttack)
        {
            //tank.animator.SetBool("IsAttack", true);
            tank.Attack();
        }

    }
    public void OnExit()
    {
        tank.animator.SetBool("IsAttack", false);
    }
}
public class PlayerDieStatus : Status
{
    // Start is called before the first frame update
    private PlayerFSM manager;
    private Player tank;

    public PlayerDieStatus(PlayerFSM manager)
    {
        this.manager = manager;
        tank = manager.tank;
    }
    public void OnEnter()
    {
        tank.Die();
        EventCenter.Instance.OnTriggerEven("PlayerDie");
        EventCenter.Instance.OnTriggerEven("GameOver");

        
    }
    public void OnUpdate()
    {


    }
    public void OnExit()
    {
       
    }
}
