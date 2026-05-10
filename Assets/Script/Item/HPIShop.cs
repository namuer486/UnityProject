using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIShop : MonoBehaviour
{
    public int ItemID = 1001;
    private ItemConfig itemConfig1;
    private ItemConfig itemConfig2;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster") || other.GetComponentInParent<MonsterTank>() != null)
        {

        }
        itemConfig1 = ItemTable.instance.GetConfig(1001);
        itemConfig2 = ItemTable.instance.GetConfig(1002);
        BagItem item = new BagItem(itemConfig1, 1);
        BagItem item2 = new BagItem(itemConfig2, 1);
        if (itemConfig1 != null && itemConfig2 != null && timer > 1)
        {
            Debug.Log("物品开始添加");
            EventCenter.Instance.OnTriggerEven("BagAdd", item);
            EventCenter.Instance.OnTriggerEven("BagAdd", item2);
            timer = 0;
        }

    }
}
