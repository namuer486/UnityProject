using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager:MonoBehaviour
{
    // Start is called before the first frame update
    public static UiManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            EventCenter.Instance.Add(this, "UiManagerInit", Init);
        }
        else
            Destroy(this);
    }

    public MenuUi MenuUi;
    public GameUiShow GameUi;
    public PaueseUi PaueseUi;
    public EndUi EndUi;
    public SkiilPanel SkiilPanel;
    public PlayerPanel PlayerPanel;
    public BackPack BackPack;

    public BasePanel PauseUi;
    private BasePanel UiMainNow;
    private Stack<BasePanel> PopupStack;
    private GameStatus statusnow;

    private bool IsPanelOpen;
    private bool IsBackOpen;
    private bool IsPauseOpen;

    private void Start()
    {

    }
    private void OnDestroy()
    {
        EventCenter.Instance.RemoveAll(this);
    }
    private void Init()
    {
        IsPanelOpen = false;
        IsBackOpen = false;
        PopupStack = new Stack<BasePanel>();
        EventCenter.Instance.Add(this, "RefreshScreen", RefreshScreen);
        BackPack.Init();
        PlayerPanel.Init();
        SkiilPanel.Init();
        EventCenter.Instance.Add(this, "PushPlayerPanel", PushPlayerPanel);
        EventCenter.Instance.Add(this, "PushBackPack", PushBackPack);
        EventCenter.Instance.Add(this, "PushPuasePanel", PushPuasePanel);
        EventCenter.Instance.Add(this, "PushClearAll", ClearAll);
        EventCenter.Instance.Add(this, "PushCardsPanel", PushCardsPanel);

    }
    //主界面处理
    public void RefreshScreen()
    {
        Debug.Log(statusnow);
        statusnow = GameManage.instance.GetStatus();
        switch (statusnow)
        {
            case GameStatus.menu: ChangeMainUi(MenuUi); break;
            case GameStatus.gameplay: ChangeMainUi(GameUi); break;
            case GameStatus.gameover: ChangeMainUi(EndUi); break;
        }
    }
    private void ChangeMainUi(BasePanel panel)
    {
        Debug.Log(panel);
        if (UiMainNow != null)
        {
            UiMainNow.Close();
        }
        UiMainNow=panel;
        UiMainNow.Open();
    }
    //弹窗处理
    private void PushPlayerPanel()
    {
        if(!IsPanelOpen)
        {
            Push(PlayerPanel);
            IsPanelOpen = true;
        }
        else
        {
            Pop();
            IsPanelOpen = false;
        }
        
    }
    private void PushBackPack()
    {
        if(!IsBackOpen)
        {
            Push(BackPack);
            IsBackOpen = true;
        }
        else
        {
            Pop();
            IsBackOpen=false;
        }
        
    }
    private void PushPuasePanel()
    {
        if(!IsPauseOpen)
        {
            Push(PaueseUi);
            IsPauseOpen = true;
        }
        else
        {
            Pop();
            IsPauseOpen = false;
        }
    }
    private void PushCardsPanel()
    {
        Push(SkiilPanel);
    }
    private void Push(BasePanel panel)
    {
        if (panel == null)
            return;
        PopupStack.Push(panel);
        panel.Open();
    }
    private void Pop()
    {
        if(PopupStack.Count > 0)
        {
            var panel = PopupStack.Pop();
            panel.Close();
        }
    }
    private void ClearAll()
    {
        while (PopupStack.Count > 0)
        {
            var panel = PopupStack.Pop();
            panel.Close();
        }
    }
    private void BackClose()
    {
        IsBackOpen=false;
    }
    private void PanelClose()
    {
        IsPanelOpen=false;
    }
}
