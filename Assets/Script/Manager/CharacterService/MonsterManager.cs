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
    private float timer = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > time)
        {
            //Level++;
            //MonsterAllDie();
            //MonsterBirth();
            timer = 0;
        }
    }
    private void Init()
    {
        monster = new List<GameObject>();
        EventCenter.Instance.Add<GameObject>(this, "MonsterDie", MonsterDie);
        EventCenter.Instance.Add<MonsterFSM, int>(this, "MonsterOnHurt", MonsterOnHurt);
        EventCenter.Instance.Add(this, "MonsterAllDie", MonsterAllDie);
        EventCenter.Instance.Add(this, "MonsterBirth", MonsterBirth);
    }
    private void MonsterBirth()//TODO:波次生产器
    {
        monsternum = Level * monsternum;
        for (int i = 0; i < monsternum; i++)
        {
            if (Birthpos.Length <= 0)
            {
                Debug.Log("坐标未添加");
                return;
            }
            GameObject mymonster = MonsterPool.instance.GetMonster();

            //初始朝向设置
            //mymonster.transform.position = bossparolpos[bossparolpos_idx].transform.position;
            //mymonster.transform.LookAt(bossparolpos[bossparolpos_idx + 1].transform.position);
            //mymonster.transform.Find("fort").LookAt(bossparolpos[bossparolpos_idx + 1].transform.position);
            mymonster.transform.position = Birthpos[0].position + Vector3.left * 150;

            mymonster.SetActive(true);

            mymonster.GetComponent<MonsterFSM>().Init();
            //mymonster.GetComponent<MonsterCharol>().Init();
            //mymonster.GetComponent<MonsterRepel>().Init();
            EventCenter.Instance.OnTriggerEven("UpLoadMonsterPos", transform.position);
            monster.Add(mymonster);
        }
        //EventCenter.Instance.OnTriggerEven("MonsterDie");

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
    }
}
