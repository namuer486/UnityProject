using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmetType
{
    none,
    head,
    body,
    foot,
    left,
    right
}
public class Equipment : MonoBehaviour
{
    public ItemConfig config {  get; set; }
    public float level {  get; set; }
}
