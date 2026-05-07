using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool: MonoBehaviour
{
    // Start is called before the first frame update
    public static BulletPool instance;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public GameObject bullet;
    public GameObject monsterbullet;

    private Vector3 pos_pool = new Vector3(100, -1000, 100);
    private Vector3 pos_monpool = new Vector3(200, -1000, 100);
    private int MAXNUMBULLET = 20;
    private int MAXNUMBULLET_MON = 100;

    private Queue<GameObject> queue;
    private Queue<GameObject> monsterqueue;
    public void Init() { 
        queue = new Queue<GameObject>();
        monsterqueue = new Queue<GameObject>();

        for (int i = 0; i < MAXNUMBULLET; i++)
        {
            GameObject newbullet = Instantiate(bullet, pos_pool, Quaternion.identity);
            newbullet.transform.GetComponent<Bullet>().AKT = PlayerManager.instance.player.AKT;
            newbullet.SetActive(false);
            queue.Enqueue(newbullet);
        }
        for (int i = 0; i < MAXNUMBULLET_MON; i++)
        {
            GameObject newmonbullet = Instantiate(monsterbullet, pos_monpool + Vector3.up, Quaternion.identity);
            newmonbullet.SetActive(false);
            monsterqueue.Enqueue(newmonbullet);
        }
    }

    public GameObject GetBullet()
    {
        if (queue.Count <= 0)
            return null;
        return queue.Dequeue();
    }
    public GameObject GetMonsterBullet()
    {
        if (monsterqueue.Count <= 0)
            return null;
        return monsterqueue.Dequeue();
    }
    public void ComeBackBullet(GameObject newbullet)
    {
        EventCenter.Instance.RemoveAll(newbullet);
        newbullet.SetActive(false);
        newbullet.transform.position = pos_pool;
        queue.Enqueue(newbullet);
    }
    public void ComeBackMonsterBullet(GameObject newmonsterbullet)
    {
        EventCenter.Instance.RemoveAll(newmonsterbullet);
        newmonsterbullet.SetActive(false);
        newmonsterbullet.transform.position = pos_monpool;
        monsterqueue.Enqueue(newmonsterbullet);
    }
}
