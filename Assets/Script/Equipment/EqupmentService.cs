using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqupmentService : MonoBehaviour
{
    public static EqupmentService Instance; 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    public EquipmentGrid head;
    public EquipmentGrid body;
    public EquipmentGrid foot;
    public EquipmentGrid left;
    public EquipmentGrid right;
    public void Init()
    {

    }
}
