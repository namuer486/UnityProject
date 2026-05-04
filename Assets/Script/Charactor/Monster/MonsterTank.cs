using System.Collections;
using System.Collections.Generic;
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

    public void Init()
    {
        EventCenter.Instance.Add<Transform>(this, "LoadPlayerPos", LoadPlayerPos);

        HP = 100;
        MP = 50;
        exp = 10;
        type = null;
        attack_cd = 0.5f;
        item = ItemTable.instance.GetConfig(1001);
    }
    public bool CheckAttack()
    {
        float distance = Vector3.Distance(playerpos.position, transform.position);
        if (distance< attack_length)
        {
            return true;
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

    public void LoadPlayerPos(Transform playerpos)
    {
        if (playerpos == null)
            return;
        this.playerpos = playerpos;

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
}
