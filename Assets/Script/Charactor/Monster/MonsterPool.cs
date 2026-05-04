using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public static MonsterPool instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public GameObject monster;
    private Queue<GameObject> birthqueue;

    public int MAX_MONSTER_NUM = 10;
    void Start()
    {
        birthqueue = new Queue<GameObject>();


        for (int i = 0; i < MAX_MONSTER_NUM; i++)
        {
            GameObject newbullet = Instantiate(monster);
            newbullet.SetActive(false);
            birthqueue.Enqueue(newbullet);
        }
        //EventCenter.Instance.Add(this, "MonsterBirth", MonsterBirth);
    }

    public GameObject GetMonster()
    {
        if (birthqueue.Count <= 0)
        {
            Debug.Log("¹ÖÎï³ØÒÑ¿Õ");
            return null;
        }
        return birthqueue.Dequeue();
    }
    public void ComeBack(GameObject newbullet)
    {
        //EventCenter.Instance.RemoveAll(newbullet);
        newbullet.SetActive(false);
        birthqueue.Enqueue(newbullet);
    }
    public int GetMonsterManNum() => MAX_MONSTER_NUM;

}
