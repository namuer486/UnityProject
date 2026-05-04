using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIShop : MonoBehaviour
{
    public int ItemID = 1001;
    private ItemConfig itemConfig;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster") || other.GetComponentInParent<MonsterFSM>() != null)
        {

        }
        itemConfig = ItemTable.instance.GetConfig(ItemID);
        if (itemConfig != null && timer > 1)
        {
            Debug.Log("物品开始添加");
            EventCenter.Instance.OnTriggerEven("BagAdd", itemConfig, 1);
            timer = 0;
        }

    }
}
