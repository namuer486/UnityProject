using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRepel : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> allEnemies;   // 敌人列表
    public float minDistance = 2f;      // 怪物之间最小距离
    public float repelStrength = 5f;    // 推开强度
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(allEnemies.Count <= 0)
        //    return;
        //// 计算互斥偏移
        //Vector3 repelOffset = CalculateRepulsion();

        //// 应用偏移
        //transform.position += repelOffset * Time.deltaTime;
    }
    public void Init()
    {
        allEnemies = new List<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("怪物加入互斥池");
            allEnemies.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("怪物离开互斥池");
            allEnemies.Remove(other.transform);
        }
    }
    public Vector3 CalculateRepulsion()
    {
        Vector3 offset = Vector3.zero;

        foreach (var enemy in allEnemies)
        {
            // 跳过自己
            if (enemy == null || enemy == transform)
                continue;

            // 只计算 XZ 平面（3D 标准）
            Vector3 selfPos = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 otherPos = new Vector3(enemy.position.x, 0, enemy.position.z);

            float distance = Vector3.Distance(selfPos, otherPos);

            // 小于最小距离 → 推开
            if (distance < minDistance && distance > 0.01f)
            {
                // 推开方向
                Vector3 dir = (selfPos - otherPos).normalized;

                // 距离越近，推力越大
                float force = (1 - (distance / minDistance)) * repelStrength;

                offset += dir * force;
            }
        }

        // 限制最大偏移，防止飞出去
        if (offset.magnitude > repelStrength)
            offset = offset.normalized * repelStrength;

        return offset;
    }
}
