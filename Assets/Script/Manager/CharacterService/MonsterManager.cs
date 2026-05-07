using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            EventCenter.Instance.Add(this, "MonsterManagerInit", Init);
        }
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    private List<GameObject> monster;

    public Transform[] Birthpos;
    public int monsternum = 1;
    public int Level = 1;
    public float time = 10;
    private bool StartTime = false;
    private float timer = 0;
    private int monsterlive;

    private Transform playerpos;
    public void Update()
    {
        if (!StartTime)
            return;
        timer += Time.deltaTime;
        if (monsterlive == 0||timer>time)
        {
            timer = 0;
            EventCenter.Instance.OnTriggerEven("GameChose");
            Level++;
            monsternum = Level * monsternum;
            monsterlive = monsternum;
            MonsterAllDie();
            MonsterBirth();
        }
    }
    private void Init()
    {
        monster = new List<GameObject>();
        EventCenter.Instance.Add<GameObject>(this, "MonsterDie", MonsterDie);
        EventCenter.Instance.Add<MonsterFSM, int>(this, "MonsterOnHurt", MonsterOnHurt);
        EventCenter.Instance.Add(this, "MonsterAllDie", MonsterAllDie);
        EventCenter.Instance.Add(this, "MonsterBirth", MonsterBirth);
        EventCenter.Instance.Add<Transform>(this, "LoadPlayerPos", LoadPlayerPos);
        EventCenter.Instance.Add<bool>(this, "StartMonsterTime", StartMonsterTime);
        EventCenter.Instance.Add(this, "MonsterMangerDestry", Destry);
        EventCenter.Instance.Add(this, "MonsterMangerReset", OnReset);
    }
    private void Destry()
    {
        EventCenter.Instance.RemoveAll(this);
    }
    private void OnReset()
    {
        Level = 1;
        timer = 0;
        monsternum = 1;
        StartTime = false;
    }
    private void StartMonsterTime(bool flag)
    {
        StartTime = flag;
    }
    private void MonsterBirth()//TODO:波次生产器
    {
        if (Birthpos.Length <= 0)
        {
            Debug.Log("坐标未添加");
            return;
        }
        monsterlive = monsternum;
        StartCoroutine(LoadMonster());
    }
    IEnumerator LoadMonster()
    {
        for (int i = 0; i < monsternum; i++)
        {
            GameObject mymonster = MonsterPool.instance.GetMonster();
            mymonster.GetComponent<MonsterTank>().Init(playerpos);
            mymonster.transform.position = Birthpos[0].position + Vector3.left * 150;
            mymonster.SetActive(true);
            EventCenter.Instance.OnTriggerEven("UpLoadMonsterPos", transform.position);
            monster.Add(mymonster);
            yield return new WaitForSeconds(2);
        }
    }
    private void MonsterOnHurt(MonsterFSM monster, int AkT)
    {
        monster.prameter.HP -= AkT;
        if (monster.prameter.HP < 0)
        {
            monster.prameter.HP = 0;
            monster.IsDie = true;
        }

    }
    private void MonsterDie(GameObject gameObject)
    {
        monster.Remove(gameObject);
        MonsterPool.instance.ComeBack(gameObject);
        monsterlive--;
    }
    private void MonsterAllDie()
    {
        if (monster.Count <= 0)
            return;
        foreach (var mon in monster)
        {
            MonsterPool.instance.ComeBack(mon);
        }
        monster.Clear();
        monsterlive = 0;
    }
    public void LoadPlayerPos(Transform playerpos)
    {
        if (playerpos == null)
            return;
        this.playerpos = playerpos;

    }
}
