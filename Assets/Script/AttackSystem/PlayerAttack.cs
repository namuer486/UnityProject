using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public float attack_cd = .1f;
    public int AKT = 10;
    private float timer = 0;
    void Start()
    {
        
    }
    public void Init()
    {
        EventCenter.Instance.Add(this, "PlayerAttack", Attack);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
    
    private void OnDestroy()
    {
        EventCenter.Instance.RemoveAll(this);
    }
    private void Attack()
    {
        if (timer < attack_cd)
            return;
        GameObject mybullet = BulletPool.instance.GetBullet();
        if (mybullet == null)
        {
            Debug.Log("×Óµ¯Î´ÉèÖÃ");
            return;
        }
        mybullet.transform.position = transform.Find("fort").Find("posshoot").position;
        mybullet.transform.rotation = transform.rotation;
        mybullet.SetActive(true);
        timer = 0;
    }
}
