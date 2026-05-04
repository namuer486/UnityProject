using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private Dictionary<string, ItemConfig> Table = new Dictionary<string, ItemConfig>();
    public void Save()
    {

    }
    public void Load()
    {

    }
}
