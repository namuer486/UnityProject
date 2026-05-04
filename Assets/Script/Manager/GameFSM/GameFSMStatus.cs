using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuStatu : Status
{
    public void OnEnter()
    {

        Debug.Log("游戏主菜单");
    }
    public void OnUpdate()
    {
        //if (GameManage.instance.IsGameBegin())
        //{
        //    GameManage.instance.ChangeStatus(GameStatus.gameplay);
        //}
    }
    public void OnExit()
    {

    }
}
public class GamePlayStatu : Status
{
    private GameSubFSM subFSM = new GameSubFSM();
    public void OnEnter()
    {
        Debug.Log("游戏开始");
        EventCenter.Instance.OnTriggerEven("InputOpen");
        EventCenter.Instance.OnTriggerEven("MonsterBirth");
        EventCenter.Instance.OnTriggerEven("PlayerBirth");
        subFSM.Init();
        subFSM.ChangeSubState(GameSubStatus.normal);

    }
    public void OnUpdate()
    {
        //if (GameManage.instance.IsGameOver())
        //{
        //    GameManage.instance.ChangeStatus(GameStatus.gameover);
        //}
        subFSM.currentstatus.OnUpdate();
    }
    public void OnExit()
    {
        subFSM.ChangeSubState(GameSubStatus.normal);
        EventCenter.Instance.OnTriggerEven("InputClose");
        EventCenter.Instance.OnTriggerEven("PushClearAll");
        EventCenter.Instance.OnTriggerEven("MonsterAllDie");
    }
}
public class GameOverStatu : Status
{
    public void OnEnter()
    {
        Debug.Log("游戏结束");
        
    }
    public void OnUpdate()
    {
        //返回主菜单
        //if (GameManage.instance.IsBackMenu())
        //{
        //    GameManage.instance.ChangeStatus(GameStatus.menu);
        //}

        //if (GameManage.instance.IsGameBegin())
        //{
        //    GameManage.instance.ChangeStatus(GameStatus.gameplay);
        //}


    }
    public void OnExit()
    {

    }
}
