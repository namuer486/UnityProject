using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public enum BulletType
    {

    }
    // Start is called before the first frame update
    public Transform headpos;
    public int speed = 1000;
    public int AKT = 10;
    public float LiveTime;
    private float timer = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<Player>() != null)
        {
            EventCenter.Instance.OnTriggerEven("PlayerHurt", AKT);
            Debug.Log(other+"±»»÷ÖÐ");
        }
        BulletPool.instance.ComeBackMonsterBullet(gameObject);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > LiveTime)
        {
            BulletPool.instance.ComeBackMonsterBullet(gameObject);
            timer = 0;
        }
        Vector3 dir = Quaternion.Inverse(transform.rotation) * transform.forward;
        transform.Translate(dir * Time.deltaTime * speed);

    }
}
