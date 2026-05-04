using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {

    }
    // Start is called before the first frame update
    public int speed = 1000;
    public int AKT {  private get; set; }
    public float LiveTime;
    private float timer = 0;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Monster") || other.GetComponentInParent<MonsterFSM>() != null)
        {
            MonsterFSM monster = other.GetComponentInParent<MonsterFSM>();
            EventCenter.Instance.OnTriggerEven("MonsterOnHurt", monster, AKT);
            EventCenter.Instance.OnTriggerEven("PlayerExpUp", monster.prameter.exp);
            Debug.Log(other + "±»»÷ÖÐ");
        }
        BulletPool.instance.ComeBackBullet(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > LiveTime)
        {
            BulletPool.instance.ComeBackBullet(gameObject);
            timer = 0;
        }
        Vector3 dir = Quaternion.Inverse(transform.rotation)*transform.forward;
        transform.Translate(dir * Time.deltaTime * speed);

    }
}
