using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    menu,
    gameplay,
    gameover
}
public enum GameSubStatus
{
    normal,
    pause,
    chose
}
public class GameManage : MonoBehaviour
{
    public static GameManage instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private bool is_game_over;
    private bool is_back_menu;

    private Status curruntstatus;//实例
    private GameStatus statusnow;
    private Dictionary<GameStatus, Status> gamestatus;
    

    void Start()
    {
        Init();
        EventCenter.Instance.OnTriggerEven("BackPackManagerInit");
        EventCenter.Instance.OnTriggerEven("UiManagerInit");
        EventCenter.Instance.OnTriggerEven("MonsterManagerInit");
        EventCenter.Instance.OnTriggerEven("PlayerManagerInit");
        Debug.Log("服务模块初始化完成");

        var table = ItemTable.Instance;
        table.SaveConfig();
        Debug.Log("物品表已加载：" + table);
        var ctable = CardsConfig.Instance;
        ctable.SaveCards();
        Debug.Log("卡片表已加载：" + ctable);
        BulletPool.instance.Init();
    }
    void Update()
    {
        Debug.Log(curruntstatus);
        curruntstatus.OnUpdate();
        
        
    }
    private void LateUpdate()
    {
        UiManager.instance.RefreshScreen();
        if (!is_game_over)
        {
           CameraManager.instance.LookAtPlayer();
        }
        
    }

    private void Init()
    {
        is_game_over = true;
        is_back_menu = false;
        gamestatus = new Dictionary<GameStatus, Status>();

        gamestatus.Add(GameStatus.menu, new MenuStatu());
        gamestatus.Add(GameStatus.gameplay, new GamePlayStatu());
        gamestatus.Add(GameStatus.gameover, new GameOverStatu());
        ChangeStatus(GameStatus.menu);

        EventCenter.Instance.Add(this, "GameOver", GameOver);
        EventCenter.Instance.Add(this, "GameBegin", GameBegin);
        EventCenter.Instance.Add(this, "ComeBackMenu", ComeBackMenu);
    }
    public void ChangeStatus(GameStatus status)
    {
        statusnow = status;
        if (curruntstatus != null)
        {
            curruntstatus.OnExit();
        }
        curruntstatus = gamestatus[statusnow];
        curruntstatus.OnEnter();
        
    }
    //public bool IsGameOver() => is_game_over;
    //public bool IsGameBegin() => !is_game_over;
    //public bool IsBackMenu() => is_back_menu;
    private void GameOver()
    {
        ChangeStatus(GameStatus.gameover);
        is_game_over = true;
    }
    private void GameBegin()
    {
        ChangeStatus(GameStatus.gameplay);
        is_game_over = false;
        is_back_menu = false;
    }
    private void ComeBackMenu()
    {
        ChangeStatus(GameStatus.menu);
        is_back_menu = true;
    }

    public GameStatus GetStatus() => statusnow;

    private void GamePause()
    {

    }
}
