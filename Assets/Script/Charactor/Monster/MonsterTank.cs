using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class MonsterTank:MonoBehaviour
{

    public int HP;
    public int MP;
    public int exp;
    public float attack_cd;
    public float attack_length;
    public Transform type;
    public ItemConfig item;

    //×·»÷
    private Transform playerpos;
    public float charolspeed;
    //×´Ì¬»ú
    private MonsterFSM fsm;
    public void Init(Transform playerpos)
    {
        fsm=new MonsterFSM(this);
        this.playerpos = playerpos;
        HP = 100;
        MP = 50;
        exp = 10;
        type = null;
        attack_cd = 0.5f;
        item = ItemTable.instance.GetConfig(1001);
    }
    private void Update()
    {
        fsm.currentstatus.OnUpdate();
    }
    public bool CheckAttack()
    {
        if (playerpos!=null)
        {
            float distance = Vector3.Distance(playerpos.position, transform.position);
            if (distance < attack_length)
            {
                return true;
            }
        }
        return false;
    }
    public void Attack()
    {
        Debug.Log("×Óµ¯·¢Éä");
        transform.Find("fort").LookAt(playerpos);
        GameObject mybullet = BulletPool.instance.GetMonsterBullet();
        if (mybullet != null)
        {
            mybullet.transform.position = transform.Find("fort").Find("posshoot").position;
            mybullet.transform.rotation = transform.Find("fort").rotation;
            mybullet.SetActive(true);
        }
    }
    public void Charol()
    {
        if (playerpos == null)
            return;
        Debug.Log("¿ªÊ¼×·»÷");
        transform.LookAt(playerpos);
        // ¼ÆËã»¥³âÆ«ÒÆ
        Vector3 repelOffset = transform.GetComponent<MonsterRepel>().CalculateRepulsion();

        // Ó¦ÓÃÆ«ÒÆ
        transform.position = Vector3.MoveTowards(transform.position, playerpos.position, charolspeed * Time.deltaTime) + repelOffset * Time.deltaTime; ;

    }
    public void OnHurt(int AkT)
    {
        HP -= AkT;
        if (HP < 0)
        {
            HP = 0;
            fsm.IsDie = true;
        }

    }
}
