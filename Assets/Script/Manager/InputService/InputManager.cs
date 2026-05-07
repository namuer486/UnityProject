using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    public int PlyerID {  private get; set; }
    private bool enable=false;
    private bool ispause=false;

    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.Add(this, "InputOpen", Enable);
        EventCenter.Instance.Add(this, "InputClose", Disable);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("输入状态" + enable);
        if(!enable)
            return;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("游戏暂停");
            ispause = !ispause;
            if (ispause)
                EventCenter.Instance.OnTriggerEven("GamePause");
            else
                EventCenter.Instance.OnTriggerEven("GamePauseOver");
            EventCenter.Instance.OnTriggerEven("PushPuasePanel");

        }
        if(ispause)
            return;
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("打开背包");
            EventCenter.Instance.OnTriggerEven("PushBackPack");
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("打开面板");
            EventCenter.Instance.OnTriggerEven("PushPlayerPanel");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("打开技能选择");
            EventCenter.Instance.OnTriggerEven("PushCardsPanel");
            
        }
        CheckMove();
        if(!isMoving)
        {
            CheckAttack();
        }
        CheckRotation();
    }
    private void CheckMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        bool newMoveState = (h != 0 || v != 0);

        if (newMoveState != isMoving)
        {
            isMoving = newMoveState;
            EventCenter.Instance.OnTriggerEven("PlayerToMove", isMoving);
        }
    }
    private void CheckAttack()
    {
        if (Input.GetMouseButtonUp(0))
        {
            EventCenter.Instance.OnTriggerEven("PlayerToAttack", true);
        }
    }
    private void CheckRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            EventCenter.Instance.OnTriggerEven("PlayerToRotation",-1);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            EventCenter.Instance.OnTriggerEven("PlayerToRotation", 1);
        }
        else
        {
            EventCenter.Instance.OnTriggerEven("PlayerToRotation", 0);
        }
    }
    private void Enable()
    {
        StartCoroutine(LateEnable());
    }
    IEnumerator LateEnable()
    {
        yield return null;
        enable = true;
    }
    private void Disable() => enable = false;
}
