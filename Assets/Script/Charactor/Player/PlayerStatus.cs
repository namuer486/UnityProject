using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdelStatus:Status
{
    // Start is called before the first frame update
    private Player manager;
    private bool isMoving;
    public PlayerIdelStatus(Player manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        
    }
    public void OnUpdate()
    {
        if (manager.CanDie())
        {
            manager.ChangePlayerStatus(PlayerType.die);
        }
        isMoving = Input.GetAxisRaw("Horizontal") != 0
             || Input.GetAxisRaw("Vertical") != 0;
        if(isMoving)
        {
            manager.ChangePlayerStatus(PlayerType.move);
        }
        if(manager.CanRotation())
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
    private Player manager;
    public PlayerMoveStatus(Player manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        
    }
    public void OnUpdate()
    {
        if (manager.CanDie())
        {
            manager.ChangePlayerStatus(PlayerType.die);
        }
        if (manager.CanRotation())
        {
            manager.ChangePlayerStatus(PlayerType.rotation);
        }
        if (manager.CanMove())
        {
            EventCenter.Instance.OnTriggerEven("PlayerMove");
        }
    }
    public void OnExit()
    {

    }
}

public class PlayerRotationStatus: Status
{
    // Start is called before the first frame update
    private Player manager;
    private bool isLeft;
    private float timer;
    public PlayerRotationStatus(Player manager)
    {
        this.manager = manager;
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
        if(manager.CanDie())
        {
            manager.ChangePlayerStatus(PlayerType.die);
        }
        if (timer > 1.1)
        {
            manager.ChangePlayerStatus(PlayerType.idel);
        }
        if(isLeft)
            EventCenter.Instance.OnTriggerEven("PlayerLeftRotation");
        else
            EventCenter.Instance.OnTriggerEven("PlayerRightRotation");

    }
    public void OnExit()
    {
        timer = 0;
    }
}
public class PlayerDieStatus : Status
{
    // Start is called before the first frame update
    private Player manager;

    public PlayerDieStatus(Player manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.Die();
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
