using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum ItemType
{
    hp,
    attack
}

[CreateAssetMenu(menuName = "Item/ItemTable", fileName = "ItemTable")]//静态数据部分
public class ItemTable:ScriptableObject
{
    public static ItemTable instance;

    public static ItemTable Instance
    {
        get {
            if (instance != null)
                return instance;
            instance = Resources.Load<ItemTable>("ItemTable");
            if (instance == null)
            {
                Debug.LogError("Resources 里找不到 ItemTable！请检查文件是否放置正确");
            }
            return instance;
        }
    }

    public List<ItemConfig> items=new List<ItemConfig>();
    private Dictionary<int, ItemConfig> config=new Dictionary<int, ItemConfig>();

    public void SaveConfig()
    {
        foreach (var item in items)
        {
            config.Add(item.ItemID, item);
            Debug.Log("静态物品" + item.itemName + "加载完毕");
        }
    }
    public ItemConfig GetConfig(int key)
    {
        if (!config.ContainsKey(key))
        {
            Debug.Log("没有此物品");
            return null;
        }
        return config[key];
    }
}
[System.Serializable]
public class ItemConfig
{
    public int ItemID;
    public ItemType type;
    public int maxCount;
    public string itemName;
    public Sprite icon;
    public int effectNum;
}




